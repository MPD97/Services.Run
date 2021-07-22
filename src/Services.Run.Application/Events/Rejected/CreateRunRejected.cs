using Convey.CQRS.Events;

namespace Services.Run.Application.Events.Rejected
{
    [Contract]
    public class CreateRunRejected : IRejectedEvent
    {
        public string Reason { get; }
        public string Code { get; }

        public CreateRunRejected(string reason, string code)
        {
            Reason = reason;
            Code = code;
        }
    }
}