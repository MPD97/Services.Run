namespace Services.Run.Core.Exceptions
{
    public class InvalidAccuracyException : DomainException
    {
        public override string Code { get; } = "invalid_accuracy";
        
        public decimal Accuracy { get; }

        public InvalidAccuracyException(decimal accuracy) 
            : base($"Point has invalid accuracy: {accuracy}.")
        {
            Accuracy = accuracy;
        }
    }
}