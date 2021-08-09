using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Services.Run.Application.DTO;
using Services.Run.Application.Queries;
using Services.Run.Core.Entities;
using Services.Run.Infrastructure.Mongo.Documents;

namespace Services.Run.Infrastructure.Mongo.Queries.Handlers
{
    public class SearchRankingHandler: IQueryHandler<SearchRanking, PagedResult<RankingDto>>
    {
        private readonly IMongoRepository<RunDocument, Guid> _repository;

        public SearchRankingHandler(IMongoRepository<RunDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<RankingDto>> HandleAsync(SearchRanking query)
        {
            Expression<Func<RunDocument, bool>> predicate = r => r.RouteId == query.RouteId 
                && r.Status == Status.Completed;

            if (query.Date.HasValue)
            {
                var firstDayOfMonth = new DateTime(query.Date.Value.Year, query.Date.Value.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                
                predicate = predicate.And(r => r.EndTime >= firstDayOfMonth && r.EndTime <= lastDayOfMonth);
            }

            var pagedResult = await _repository.BrowseAsync(predicate, query);
            return pagedResult?.Map(d => new RankingDto(d.UserId, d.EndTime.Value, d.Time.Value));        
        }
    }
}