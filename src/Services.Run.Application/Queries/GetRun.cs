using System;
using Convey.CQRS.Queries;
using Services.Run.Application.DTO;

namespace Services.Run.Application.Queries
{
    public class GetRun: IQuery<RunDto>
    {
        public Guid RunId { get; set; }
    }
}