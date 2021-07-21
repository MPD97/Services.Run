namespace Services.Run.Core.Exceptions
{
    public class InvalidLocationIdException : DomainException
    {
        public override string Code { get; } = "invalid_location_id";

        public InvalidLocationIdException() : base($"Invalid location id.")
        {
        }
    }
}