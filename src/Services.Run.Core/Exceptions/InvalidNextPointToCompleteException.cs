namespace Services.Run.Core.Exceptions
{
    public class InvalidNextPointToCompleteException : DomainException
    {
        public override string Code { get; } = "invalid_next_point_to_complete_id";

        public InvalidNextPointToCompleteException() : base($"Invalid next point to complete.")
        {
        }
    }
}