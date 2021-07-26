using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Services.Run.Application.DTO;
using Services.Run.Application.Queries;
using Services.Run.Infrastructure.Mongo.Documents;

namespace Services.Run.Infrastructure.Mongo.Queries.Handlers
{
    public class GetRunsHandler: IQueryHandler<GetRuns, IEnumerable<RunDto>>
    {
        private readonly IMongoRepository<RunDocument, Guid> _runDocument;

        public GetRunsHandler(IMongoRepository<RunDocument, Guid> runDocument)
        {
            _runDocument = runDocument;
        }


        public async Task<IEnumerable<RunDto>> HandleAsync(GetRuns query)
        {
            var routes = await _runDocument.FindAsync(_ => true);

            return routes.Select(r => Documents.Extensions.AsDto(r));
        }
    }
}