namespace Services.Run.Application.Exceptions
{
    public class UserTooFarException : AppException
    {
        public override string Code { get; } = "user_too_far";
        public double  Distance { get; }
        public int  MinDistance { get; }
        public int  MaxDistance { get; }

        public UserTooFarException(double distance, int minDistance, int maxDistance) 
            : base($"User is: {distance:0.##} meters from route starting point. " +
                   $"Distance must be between: {minDistance} and {maxDistance} meters")
        {
            Distance = distance;
            MinDistance = minDistance;
            MaxDistance = maxDistance;
        }
    }
}