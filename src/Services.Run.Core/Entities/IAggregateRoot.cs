using System.Collections.Generic;

namespace Services.Run.Core.Entities
{
    public interface IAggregateRoot
    {
        IEnumerable<IDomainEvent> Events { get; }
        AggregateId Id { get;  }
        int Version { get; }
        void IncrementVersion();
    }
}