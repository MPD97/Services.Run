namespace Services.Run.Core.Events
{
    public class RunCompleted : IDomainEvent
    {
        public Entities.Run Run { get; }
        
        public RunCompleted(Entities.Run run)
        {
            Run = run;
        }
    }
}