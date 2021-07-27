using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Services.Run.Application.Services;
using Services.Run.Core.Entities;
using Services.Run.Core.Repositories;

namespace Services.Run.Application.Events.External.Handlers
{
    public class LocationAddedHandler: IEventHandler<LocationAdded>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRunRepository _runRepository;
        private readonly IDistanceMeasure _distanceMeasure;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker _messageBroker;

        public LocationAddedHandler(IUserRepository userRepository, IRunRepository runRepository, IDistanceMeasure distanceMeasure, IEventMapper eventMapper, IMessageBroker messageBroker)
        {
            _userRepository = userRepository;
            _runRepository = runRepository;
            _distanceMeasure = distanceMeasure;
            _eventMapper = eventMapper;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(LocationAdded @event)
        {
            var user = await _userRepository.GetAsync(@event.UserId);
            if (user is null)
                return;

            if (user.State != State.Valid)
                return;

            var run = await _runRepository.GetActiveRunByUserId(user.Id);
            if (run is null)
                return;
            
            if(run.IsAbleToUpdate() == false)
                return;

            var pointToComplete = run.PointToComplete;
            var distance = _distanceMeasure.GetDistanceBetween(pointToComplete.Latitude, 
                pointToComplete.Longitude,@event.Latitude, @event.Longitude);

            if ((int)distance > pointToComplete.Radius)
                return;

            run.UpdateLocation(new Location(@event.LocationId, @event.Latitude,
                @event.Longitude, @event.Accuracy, @event.CreatedAt));

            await _runRepository.UpdateAsync(run);
            
            var events = _eventMapper.MapAll(run.Events);
            await _messageBroker.PublishAsync(events);
        }
    }
}