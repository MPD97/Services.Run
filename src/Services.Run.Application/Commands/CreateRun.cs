using System;
using Convey.CQRS.Commands;

namespace Services.Run.Application.Commands
{
    [Contract]
    public class CreateRun : ICommand
    {
        public Guid RunId { get; }
        public Guid RouteId { get; }
        public Guid UserId { get; }
        public decimal Latitude { get; }
        public decimal Longitude { get; }
        public decimal Accuracy { get; }

        public CreateRun(Guid runId, Guid routeId, Guid userId, decimal latitude, decimal longitude, decimal accuracy)
        {
            RunId = runId;
            RouteId = routeId;
            UserId = userId;
            Latitude = latitude;
            Longitude = longitude;
            Accuracy = accuracy;
        }
    }
}