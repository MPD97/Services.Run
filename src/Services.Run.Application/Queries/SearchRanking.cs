using System;
using Convey.CQRS.Queries;
using Services.Run.Application.DTO;

namespace Services.Run.Application.Queries
{
    public class SearchRanking: PagedQueryBase, IQuery<PagedResult<RunRankingDto>>
    {
        public Guid RouteId { get; set; }
        public DateTime? Date { get; set; }
    }
}