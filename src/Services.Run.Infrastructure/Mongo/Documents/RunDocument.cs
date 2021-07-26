using System;
using System.Collections.Generic;
using Convey.Types;
using Services.Run.Core.Entities;

namespace Services.Run.Infrastructure.Mongo.Documents
{
    public sealed class RunDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RouteId { get; set; }
        public Status Status { get; set; }
        public IEnumerable<PointDocument> Points { get; set;}
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan? Time => EndTime.HasValue ? EndTime.Value - StartTime : null;
        public PointDocument PointToComplete { get; set; }

        public RunDocument()
        {
            
        }

        public RunDocument(Guid id, Guid userId, Guid routeId, Status status, IEnumerable<PointDocument> points, DateTime startTime, DateTime? endTime, PointDocument pointToComplete)
        {
            Id = id;
            UserId = userId;
            RouteId = routeId;
            Status = status;
            Points = points;
            StartTime = startTime;
            EndTime = endTime;
            PointToComplete = pointToComplete;
        }
    }
}