using System;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Services.Run.Application.DTO;
using Services.Run.Application.Queries;
using Services.Run.Infrastructure.Mongo.Documents;

namespace Services.Run.Infrastructure.Mongo.Queries.Handlers
{
    public class GetRunHandler: IQueryHandler<GetRun, RunDto>
    {
        private readonly IMongoRepository<RunDocument, Guid> _runDocument;

        public GetRunHandler(IMongoRepository<RunDocument, Guid> runDocument)
        {
            _runDocument = runDocument;
        }

        public async Task<RunDto> HandleAsync(GetRun query)
        {
            var document = await _runDocument.GetAsync(p => p.Id == query.RunId);

            return document?.AsDto();
        }
    }
}