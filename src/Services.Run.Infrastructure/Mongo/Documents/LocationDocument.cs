using System;

namespace Services.Run.Infrastructure.Mongo.Documents
{
    public sealed class LocationDocument
    {
        public Guid Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Accuracy { get; set; }
        public DateTime CompletedAt { get; set; }

        public LocationDocument()
        {
            
        }

        public LocationDocument(Guid id, decimal latitude, decimal longitude, int accuracy, DateTime completedAt)
        {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
            Accuracy = accuracy;
            CompletedAt = completedAt;
        }
    }
}