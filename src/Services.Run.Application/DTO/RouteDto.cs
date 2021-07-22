using System;
using System.Collections.Generic;

namespace Services.Run.Application.DTO
{
    public class RouteDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? AcceptedBy { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Difficulty { get; set; }
        public int Length { get; set; }
        public string Status { get; set; }
        public IEnumerable<PointDto> Points { get; set; }
        
        public class PointDto
        {
            public Guid Id { get; set; }
            public int Order { get; set; }
            public decimal Latitude { get; set; }
            public decimal Longitude { get; set; }
            public int Radius { get; set; }

            public PointDto()
            {
            
            }

            public PointDto(Guid id, int order, decimal latitude, decimal longitude, int radius)
            {
                Id = id;
                Order = order;
                Latitude = latitude;
                Longitude = longitude;
                Radius = radius;
            }
        }
    }
}