using System;
using Services.Run.Core.Exceptions;

namespace Services.Run.Core.ValueObjects
{
    public struct Location : IEquatable<Location>
    {
        public Guid Id { get; }
        public decimal Latitude { get; }
        public decimal Longitude { get; }
        public int Accuracy { get;  }
        public DateTime CompletedAt { get; }

        public Location(Guid id, decimal latitude, decimal longitude, int accuracy, DateTime completedAt)
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

        public bool Equals(Location other)
        {
            return Id.Equals(other.Id) && Latitude == other.Latitude && Longitude == other.Longitude && Accuracy == other.Accuracy && CompletedAt.Equals(other.CompletedAt);
        }

        public override bool Equals(object obj)
        {
            return obj is Location other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Latitude, Longitude, Accuracy, CompletedAt);
        }

        public static bool operator ==(Location left, Location right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Location left, Location right)
        {
            return !left.Equals(right);
        }
    }
}