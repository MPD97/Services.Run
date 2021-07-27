using System;
using System.Threading.Tasks;
using Services.Run.Application.DTO;

namespace Services.Run.Application.Services.Route
{
    public interface IRouteServiceClient
    {
        Task<RouteDto> GetAsync(Guid id);
    }
}