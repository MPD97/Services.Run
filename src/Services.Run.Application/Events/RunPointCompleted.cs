using System;
using Convey.CQRS.Events;

namespace Services.Run.Application.Events
{
    [Contract]
    public class RunPointCompleted: IEvent
    {
        public Guid PointId { get; }
        
        public RunPointCompleted( Guid pointId)
        {
            PointId = pointId;
        }
    }
}