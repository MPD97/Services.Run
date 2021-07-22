using System;
using Convey.CQRS.Events;

namespace Services.Run.Application.Events
{
    [Contract]
    public class RunCreated : IEvent
    {
        public Guid RunId { get; }
        public string Status { get; }

        public RunCreated(Guid runId, string status)
        {
            RunId = runId;
            Status = status;
        }
    }
}