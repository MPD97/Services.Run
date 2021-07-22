using System;
using Convey.CQRS.Events;

namespace Services.Run.Application.Events
{
    [Contract]
    public class RunCancelled : IEvent
    {
        public Guid RunId { get; }

        public RunCancelled(Guid runId)
        {
            RunId = runId;
        }
    }
}