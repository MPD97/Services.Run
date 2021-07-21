using System;
using System.Threading.Tasks;

namespace Services.Run.Core.Repositories
{
    public interface IRunRepository
    {
        Task<Entities.Run> GetAsync(Guid id);
        Task<Entities.Run> GetActiveRunByUserId(Guid id);
        Task AddAsync(Entities.Run run);
        Task UpdateAsync(Entities.Run run);
    }
}