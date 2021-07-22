using Services.Run.Core.Entities;

namespace Services.Run.Application.Exceptions
{
    public class InvalidRouteStatusException : AppException
    {
        public override string Code { get; } = "invalid_route_status";
        public RouteStatus  Status { get; }

        public InvalidRouteStatusException(RouteStatus status) 
            : base($"Route status: {status} is not valid.")
        {
            Status = status;
        }
    }
}