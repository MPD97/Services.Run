﻿using System;
using System.Collections.Generic;
using System.Linq;
using Services.Run.Core.Events;
using Services.Run.Core.Exceptions;
using Services.Run.Core.ValueObjects;

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
        public DateTime StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }

        public TimeSpan? Time => EndTime.HasValue ? EndTime.Value - StartTime : null;

        public Point? PointToComplete
        {
            get
            { 
                var notCompletedPoints = Points.Where(p => p.Completed == false).ToArray();
                if (notCompletedPoints.Length == 0)
                    return null;
                var minOrder = notCompletedPoints.Min(p => p.Order);
                var point = Points.Where(p => p.Completed == false).
                   SingleOrDefault(p => p.Order == minOrder);
                return point;
            }
        }

        public Run(Guid id, Guid userId, Guid routeId, Status status, IEnumerable<Point> points, DateTime startTime, DateTime? endTime)
        {
            Id = id;
            UserId = userId;
            RouteId = routeId;
            Status = status;
            Points = points;
            StartTime = startTime;
            EndTime = endTime;
        }

        public void UpdateLocation(Location location)
        {
            if (Status != Status.Started)
            {
                throw new InvalidRunStatusException(Status);
            }

            if (PointToComplete.HasValue == false)
            {
                throw new InvalidNextPointToCompleteException();
            }

            var point = PointToComplete.Value;
            point.Complete(location);
            if (PointToComplete.HasValue == false)
            {
                Status = Status.Completed;
                EndTime = location.CompletedAt;
                AddEvent(new RunCompleted(this));
            }
            
            AddEvent(new PointCompleted(point));
        }
    }
}