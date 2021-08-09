using System.Collections.Generic;
using System.Linq;
using Convey.CQRS.Events;
using Services.Run.Application.Services;
using Services.Run.Core;
using Services.Run.Core.Entities;
using Services.Run.Core.Events;

namespace Services.Run.Infrastructure.Services
{
    public class EventMapper : IEventMapper
    {
        public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events)
            => events.Select(Map);

        public IEvent Map(IDomainEvent @event)
        {
            switch (@event)
            {
                case PointCompleted e: return new Application.Events.RunPointCompleted(e.Point.Id);
                case RunCancelled e: return new Application.Events.RunCancelled(e.Run.Id);
                case RunCompleted e: return new Application.Events.RunCompleted(e.Run.Id, e.Run.UserId, e.Run.RouteId);
            }

            return null;
        }
    }
}