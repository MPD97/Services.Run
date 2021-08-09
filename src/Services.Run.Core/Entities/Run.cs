using System;
using System.Collections.Generic;
using System.Linq;
using Services.Run.Core.Events;
using Services.Run.Core.Exceptions;

namespace Services.Run.Core.Entities
{
    public class Run: AggregateRoot
    {
        private ISet<Point> _points = new HashSet<Point>();
        public Guid UserId { get; private set; }
        public Guid RouteId { get; private set; }
        public Status Status { get; private set; }
        public IEnumerable<Point> Points {   
            get => _points;
            private set => _points = new HashSet<Point>(value); 
        }
        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }

        public TimeSpan? Time => EndTime.HasValue ? EndTime.Value - StartTime : null;

        public Point PointToComplete { get; private set; }

        public Run(AggregateId id, Guid userId, Guid routeId, Status status, IEnumerable<Point> points, DateTime? startTime, DateTime? endTime)
        {
            Id = id;
            UserId = userId;
            RouteId = routeId;
            Status = status;
            Points = points;
            StartTime = startTime;
            EndTime = endTime;
            SetPointToComplete();
        }

        private void SetPointToComplete()
        {
            var notCompletedPoints = Points.Where(p => p.Completed == false).ToArray();
            if (notCompletedPoints.Length == 0)
            {
                PointToComplete = null;
                return;
            }

            var minOrder = notCompletedPoints.Min(p => p.Order);
            var point = Points.Where(p => p.Completed == false).
                SingleOrDefault(p => p.Order == minOrder);
            PointToComplete = point;
        }
        public bool IsAbleToUpdate()
        {
            if (Status != Status.Started)
                return false;

            if (PointToComplete is null)
                return false;

            return true;
        }
        public void UpdateLocation(Location location)
        {
            if (IsAbleToUpdate() == false)
                throw new RunNotChangeableException(Id);

            StartTime ??= location.CompletedAt;
            
            var point = PointToComplete;
            point.Complete(location);
            SetPointToComplete();
            if (PointToComplete is null)
            {
                Status = Status.Completed;
                EndTime = location.CompletedAt;
                AddEvent(new RunCompleted(this));
            }
            
            AddEvent(new PointCompleted(point));
        }
        public void CancelRun()
        {
            if (Status != Status.Started)
            {
                throw new InvalidRunStatusException(Status);
            }
            Status = Status.Cancelled;

            AddEvent(new RunCancelled(this));
        }
    }
}