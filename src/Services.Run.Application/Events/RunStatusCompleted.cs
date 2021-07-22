using System;
using Convey.CQRS.Events;

namespace Services.Run.Application.Events
{
    [Contract]
    public class RunStatusCompleted: IEvent
    {
        public Guid RunId { get; }
        public Guid UserId { get; }
        
        public RunStatusCompleted(Guid runId, Guid userId)
        {
            RunId = runId;
            UserId = userId;
        }
    }
}