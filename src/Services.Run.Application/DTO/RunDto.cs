using System;
using System.Collections.Generic;

namespace Services.Run.Application.DTO
{
    public class RunDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RouteId { get; set; }
        public string Status { get; set; }
        public IEnumerable<PointDto> Points { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan? Time { get; set; }

        public RunDto()
        {
        }

        public RunDto(Guid Id, Guid userId, Guid routeId, string status, IEnumerable<PointDto> points, DateTime? startTime, DateTime? endTime, TimeSpan? time)
        {
            Id = Id;
            UserId = userId;
            RouteId = routeId;
            Status = status;
            Points = points;
            StartTime = startTime;
            EndTime = endTime;
            Time = time;
        }
        public class PointDto
        {
            public Guid Id { get; set; }
            public int Order { get; set; }
            public decimal Latitude { get; set; }
            public decimal Longitude { get; set; }
            public int Radius { get; set; }
            public bool Completed { get; set; }
            public LocationDto Location { get; set; }
            public Guid? Next { get; set; }

            public PointDto()
            {
            }

            public PointDto(Guid id, int order, decimal latitude, decimal longitude, int radius, bool completed, LocationDto location, Guid? next)
            {
                Id = id;
                Order = order;
                Latitude = latitude;
                Longitude = longitude;
                Radius = radius;
                Completed = completed;
                Location = location;
                Next = next;
            }
        }
    }
}