using System;
using Convey.CQRS.Events;

namespace Services.Run.Application.Events
{
    [Contract]
    public class RunPointStatusCompleted: IEvent
    {
        public Guid RunId { get; }
        public Guid PointId { get; }
        
        public RunPointStatusCompleted(Guid runId, Guid pointId)
        {
            RunId = runId;
            PointId = pointId;
        }
    }
}