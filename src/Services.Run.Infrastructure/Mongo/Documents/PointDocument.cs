using System;
using System.Drawing;
using System.Linq;
using Services.Run.Application.DTO;
using Services.Run.Core.Entities;
using Services.Run.Core.ValueObjects;

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
     public static class Extensions
     {
         public static Core.Entities.Run AsEntity(this RunDocument document)
             => new Core.Entities.Run(document.Id, document.UserId, document.RouteId, document.Status,
                 document.Points.Select(p => new Core.ValueObjects.Point(p.Id, p.Order, p.Latitude, p.Longitude, p.Radius, p.Completed, 
                     new Location(p.Location.Id, p.Location.Latitude, p.Location.Longitude, p.Location.Accuracy, p.Location.CompletedAt),
                     p.Next)), document.StartTime, document.EndTime);

        public static RunDocument AsDocument(this Core.Entities.Run entity)
            => new RunDocument
            {
                Id = entity.Id,
                RouteId = entity.RouteId, 
                UserId = entity.UserId,
                Status =  entity.Status,
                Points = entity.Points.Select(p => new PointDocument()
                {
                    Id = p.Id,
                    Order = p.Order,
                    Completed = p.Completed,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    Radius = p.Radius,
                    Next = p.Next,
                    Location = p.Location.HasValue ?
                        new LocationDocument()
                        {
                            Id = p.Location.Value.Id,
                            Accuracy = p.Location.Value.Accuracy,
                            Latitude = p.Location.Value.Latitude,
                            Longitude = p.Location.Value.Longitude,
                            CompletedAt = p.Location.Value.CompletedAt
                        }
                        : null
                }),
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                PointToComplete = entity.PointToComplete.HasValue ? 
                new PointDocument()
                {
                    Id = entity.PointToComplete.Value.Id,
                    Order = entity.PointToComplete.Value.Order,
                    Latitude = entity.PointToComplete.Value.Latitude,
                    Longitude = entity.PointToComplete.Value.Longitude,
                    Completed = entity.PointToComplete.Value.Completed,
                    Radius = entity.PointToComplete.Value.Radius,
                    Location = entity.PointToComplete.Value.Location.HasValue ?
                        new LocationDocument()
                        {
                            Id = entity.PointToComplete.Value.Location.Value.Id,
                            Latitude = entity.PointToComplete.Value.Location.Value.Latitude,
                            Longitude = entity.PointToComplete.Value.Location.Value.Longitude,
                            Accuracy = entity.PointToComplete.Value.Location.Value.Accuracy,
                            CompletedAt = entity.PointToComplete.Value.Location.Value.CompletedAt
                        }
                        : null,
                    Next = entity.PointToComplete.Value.Next
                } 
                : null
            };

        public static RunDto AsDto(this RunDocument document)
            => new RunDto
            {
                Id = document.Id,
                UserId = document.UserId,
                RouteId = document.RouteId,
                Status = document.Status.ToString(),
                StartTime = document.StartTime,
                EndTime = document.EndTime,
                Time = document.Time,
                Points = document.Points.Select(p => new RunDto.PointDto()
                {
                    Id = p.Id,
                    Order = p.Order,
                    Completed = p.Completed,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    Next = p.Next,
                    Radius = p.Radius,
                    Location = p.Location is null ? null : new LocationDto(p.Location.Id, p.Location.Latitude,
                        p.Location.Longitude, p.Location.Accuracy, p.Location.CompletedAt)
                })
            };
        
        public static User AsEntity(this UserDocument document)
            => new User(document.Id, document.State);

        public static UserDocument AsDocument(this User entity)
            => new UserDocument
            {
                Id = entity.Id,
                State = entity.State
            };
    }
}