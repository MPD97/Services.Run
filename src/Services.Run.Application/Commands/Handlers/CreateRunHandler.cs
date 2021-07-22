using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Services.Run.Application.Events;
using Services.Run.Application.Exceptions;
using Services.Run.Application.Services;
using Services.Run.Application.Services.Route;
using Services.Run.Core.Entities;
using Services.Run.Core.Repositories;
using Services.Run.Core.ValueObjects;

namespace Services.Run.Application.Commands.Handlers
{
    public class CreateRunHandler: ICommandHandler<CreateRun>
    {
        private readonly IRunRepository _runRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly IRouteServiceClient _routeServiceClient;
        private readonly IDistanceMeasure _distanceMeasure;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IEventMapper _eventMapper;
        
        public CreateRunHandler(IRunRepository runRepository, IUserRepository userRepository,
            IMessageBroker messageBroker, IRouteServiceClient routeServiceClient,
            IDistanceMeasure distanceMeasure, IDateTimeProvider dateTimeProvider, IEventMapper eventMapper)
        {
            _runRepository = runRepository;
            _userRepository = userRepository;
            _messageBroker = messageBroker;
            _routeServiceClient = routeServiceClient;
            _distanceMeasure = distanceMeasure;
            _dateTimeProvider = dateTimeProvider;
            _eventMapper = eventMapper;
        }

        public async Task HandleAsync(CreateRun command)
        {
            var user = await _userRepository.GetAsync(command.UserId);
            if (user is null)
                throw new UserNotFoundException(command.UserId);

            if (user.State != State.Valid)
                throw new InvalidUserStateException(command.UserId, user.State);

            var route = await _routeServiceClient.GetAsync(command.RouteId);
            if (route is null)
                throw new RouteNotFoundException(command.RouteId);
            
            if (!Enum.TryParse<RouteStatus>(route.Status, true, out var routeStatus))
                throw new InvalidRouteStatusException(RouteStatus.Unknown);
            
            if (routeStatus != RouteStatus.Accepted)
                throw new RouteStatusIsNotAcceptedException(routeStatus);

            var sortedPoints = new SortedSet<Point>(new ByOrderExtension());
            foreach (var point in route.Points)
            {
                sortedPoints.Add(new Point(point.Id, point.Order, point.Latitude, point.Longitude, point.Radius, false, null, null));
            }

            for (int i = 0; i < sortedPoints.Count - 1; i++)
            {
                var current = sortedPoints.ElementAt(i);
                var next = sortedPoints.ElementAt(i + 1);

                current.SetNext(next.Id);
            }
            
            const int minDistance = 0;
            const int maxDistance = 250;
            var routeFirstPoint = sortedPoints.ElementAt(0);
            var distance = _distanceMeasure.GetDistanceBetween(command.Latitude, command.Longitude,
                routeFirstPoint.Latitude, routeFirstPoint.Longitude);
            
            if (distance is > maxDistance or < minDistance)
                throw new UserTooFarException(distance, minDistance, maxDistance);

            var activeRun = await _runRepository.GetActiveRunByUserId(user.Id);
            if (activeRun is {})
            {
                activeRun.CancelRun();
                
                await _runRepository.UpdateAsync(activeRun);
                var events = _eventMapper.MapAll(activeRun.Events);
                await _messageBroker.PublishAsync(events.ToArray());
            }
            
            var run = new Core.Entities.Run(command.RunId, user.Id, route.Id, Status.Started, sortedPoints,
                _dateTimeProvider.Now, null);

            await _runRepository.AddAsync(run);
            await _messageBroker.PublishAsync(new RunCreated(run.Id, run.Status.ToString()));
        }
    }
}