using System;
using Services.Run.Core.Exceptions;

namespace Services.Run.Core.Entities
{
    public class Location
    {
        public AggregateId Id { get; }
        public decimal Latitude { get; }
        public decimal Longitude { get; }
        public int Accuracy { get;  }
        public DateTime CompletedAt { get; }

        public Location(AggregateId id, decimal latitude, decimal longitude, int accuracy, DateTime completedAt)
        {
            Id = id == Guid.Empty ? throw new InvalidLocationIdException() : id;
            Latitude = IsValidLatitude(latitude) ? latitude : throw new InvalidLatitudeException(latitude);
            Longitude = IsValidLongitude(longitude) ? longitude : throw new InvalidLongitudeException(longitude);
            Accuracy = IsValidAccuracy(accuracy) ? accuracy : throw new InvalidAccuracyException(accuracy);
            CompletedAt = completedAt;
        }

        public static bool IsValidLatitude(decimal latitude)
            => latitude is >= -90m and <= 90m;
        public static bool IsValidLongitude(decimal longitude)
            => longitude is >= -180m and <= 180m;
        public static bool IsValidAccuracy(int accuracy)
            => accuracy is >= 0 and <= 100;
        
    }
}