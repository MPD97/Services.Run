using System;
using Convey.CQRS.Events;

namespace Services.Run.Application.Events
{
    [Contract]
    public class RunCompleted: IEvent
    {
        public Guid RunId { get; }
        public Guid UserId { get; }
        
        public RunCompleted(Guid runId, Guid userId)
        {
            RunId = runId;
            UserId = userId;
        }
    }
}