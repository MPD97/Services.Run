using System;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using Services.Run.Core.Entities;
using Services.Run.Core.Repositories;
using Services.Run.Infrastructure.Mongo.Documents;

namespace Services.Run.Infrastructure.Mongo.Repositories
{
    public class RunMongoRepository : IRunRepository
    {
        private readonly IMongoRepository<RunDocument, Guid> _repository;

        public RunMongoRepository(IMongoRepository<RunDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<Core.Entities.Run> GetAsync(Guid id)
        {
            var run = await _repository.GetAsync(r => r.Id == id);
    
            return run?.AsEntity();
        }

        public async Task<Core.Entities.Run> GetActiveRunByUserId(Guid id)
        {
            var run = await _repository.GetAsync(r => r.Id == id && r.Status == Status.Started);
    
            return run?.AsEntity();        }

        public async Task AddAsync(Core.Entities.Run run)
            => await _repository.AddAsync(run.AsDocument());

        public async Task UpdateAsync(Core.Entities.Run run)
            => await _repository.UpdateAsync(run.AsDocument());
    }
}