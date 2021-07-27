using System.Linq;
using Services.Run.Application.DTO;
using Services.Run.Core.Entities;

namespace Services.Run.Infrastructure.Mongo.Documents
{
    public static class Extensions
    {
        public static Core.Entities.Run AsEntity(this RunDocument document)
            => new Core.Entities.Run(document.Id, document.UserId, document.RouteId, document.Status,
                document.Points?.Select(p => new Point(p.Id, p.Order, p.Latitude, p.Longitude, p.Radius, p.Completed, 
                    p.Location is null ? null : new Location(p.Location.Id, p.Location.Latitude, p.Location.Longitude, p.Location.Accuracy, p.Location.CompletedAt),
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
                    Location = p.Location is {} ?
                        new LocationDocument()
                        {
                            Id = p.Location.Id,
                            Accuracy = p.Location.Accuracy,
                            Latitude = p.Location.Latitude,
                            Longitude = p.Location.Longitude,
                            CompletedAt = p.Location.CompletedAt
                        }
                        : null
                }),
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                Time = entity.Time,
                PointToComplete = entity.PointToComplete is {} ? 
                    new PointDocument()
                    {
                        Id = entity.PointToComplete.Id,
                        Order = entity.PointToComplete.Order,
                        Latitude = entity.PointToComplete.Latitude,
                        Longitude = entity.PointToComplete.Longitude,
                        Completed = entity.PointToComplete.Completed,
                        Radius = entity.PointToComplete.Radius,
                        Location = entity.PointToComplete.Location is {} ?
                            new LocationDocument()
                            {
                                Id = entity.PointToComplete.Location.Id,
                                Latitude = entity.PointToComplete.Location.Latitude,
                                Longitude = entity.PointToComplete.Location.Longitude,
                                Accuracy = entity.PointToComplete.Location.Accuracy,
                                CompletedAt = entity.PointToComplete.Location.CompletedAt
                            }
                            : null,
                        Next = entity.PointToComplete.Next
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