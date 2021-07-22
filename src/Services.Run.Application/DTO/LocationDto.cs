using System;

namespace Services.Run.Application.DTO
{
    public class LocationDto
    {
        public Guid Id { get; }
        public decimal Latitude { get; }
        public decimal Longitude { get; }
        public int Accuracy { get;  }
        public DateTime CompletedAt { get; }

        public LocationDto()
        {
        }

        public LocationDto(Guid id, decimal latitude, decimal longitude, int accuracy, DateTime completedAt)
        {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
            Accuracy = accuracy;
            CompletedAt = completedAt;
        }
    }
}