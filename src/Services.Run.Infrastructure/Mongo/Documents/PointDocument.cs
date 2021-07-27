using System;
using System.Drawing;

namespace Services.Run.Infrastructure.Mongo.Documents
{
    public sealed class PointDocument
    {
        public Guid Id { get; set; }
        public int Order { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Radius { get; set; }
        public bool Completed { get; set; }
        public LocationDocument Location { get; set; }
        public Guid? Next { get; set; }

        public PointDocument()
        {
            
        }

        public PointDocument(Guid id, int order, decimal latitude, decimal longitude, int radius, bool completed, LocationDocument location, Guid? next)
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