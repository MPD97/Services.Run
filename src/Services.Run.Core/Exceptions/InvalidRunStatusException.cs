using Services.Run.Core.Entities;

namespace Services.Run.Core.Exceptions
{
    public class InvalidRunStatusException : DomainException
    {
        public override string Code { get; } = "invalid_run_status";
        
        public Status Status { get; }

        public InvalidRunStatusException(Status status)
            : base($"Run has invalid status: {status}.")
        {
        }
    }
}