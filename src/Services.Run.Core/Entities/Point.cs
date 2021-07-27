using System;
using Services.Run.Core.Exceptions;

namespace Services.Run.Core.Entities
{
    public class Point
    {
        public AggregateId Id { get; }
        public int Order { get; }
        public decimal Latitude { get; }
        public decimal Longitude { get; }
        public int Radius { get; }
        public bool Completed { get; private set; }
        public Location Location { get; private set; }
        public Guid? Next { get; private set; }

        public Point(AggregateId id, int order, decimal latitude, decimal longitude, int radius,
            bool completed, Location location, Guid? next)
        {
            Id = id;
            Order = order < 0 ? throw new InvalidPointOrderException(order) : order;
            Latitude = IsValidLatitude(latitude) ? latitude : throw new InvalidLatitudeException(latitude);
            Longitude = IsValidLongitude(longitude) ? longitude : throw new InvalidLongitudeException(longitude);
            Radius = IsValidRadius(radius) ? radius : throw new InvalidRadiusException(radius);
            Completed = completed;
            Location = location;
            Next = next;
        }
        public static bool IsValidLatitude(decimal latitude)
            => latitude is >= -90m and <= 90m;
        public static bool IsValidLongitude(decimal longitude)
            => longitude is >= -180m and <= 180m;
        public static bool IsValidRadius(int radius)
            => radius is >= 0 and <= 100;

        public void Complete(Location location)
        {
            Completed = true;
            Location = location;
        }

        public void SetNext(Guid id) => Next = id;
    }
}