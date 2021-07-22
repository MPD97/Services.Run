using System.Collections.Generic;
using Convey.CQRS.Queries;
using Services.Run.Application.DTO;

namespace Services.Run.Application.Queries
{
    public class GetRuns : IQuery<IEnumerable<RunDto>>
    {
    }
}