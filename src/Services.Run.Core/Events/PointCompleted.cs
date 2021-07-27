using Services.Run.Core.Entities;

namespace Services.Run.Core.Events
{
    public class PointCompleted : IDomainEvent
    {
        public Point Point { get; }
        
        public PointCompleted(Point point)
        {
            Point = point;
        }
    }
}