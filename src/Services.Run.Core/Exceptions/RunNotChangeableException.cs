using System;

namespace Services.Run.Core.Exceptions
{
    public class RunNotChangeableException : DomainException
    {
        public override string Code { get; } = "run_not_changeable";

        public Guid RunId { get; }

        public RunNotChangeableException(Guid runId) 
            : base($"Run: {runId} is not changeable.")
        {
            RunId = runId;
        }
    }
}