using System;

namespace Services.Run.Core.Events
{
    public class RunCancelled : IDomainEvent
    {
        public Entities.Run Run { get; }

        public RunCancelled(Entities.Run run)
        {
            Run = run;
        }
    }
}