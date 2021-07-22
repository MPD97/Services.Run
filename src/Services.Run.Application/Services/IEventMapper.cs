using System.Collections.Generic;
using Convey.CQRS.Events;
using Services.Run.Core;

namespace Services.Run.Application.Services
{
    public interface IEventMapper
    {
        IEvent Map(IDomainEvent @event);
        IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events);
    }
}