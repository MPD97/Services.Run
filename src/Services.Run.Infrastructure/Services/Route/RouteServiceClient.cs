using System;
using System.Threading.Tasks;
using Convey.HTTP;
using Convey.Secrets.Vault;
using Convey.WebApi.Security;
using Services.Run.Application.DTO;
using Services.Run.Application.Services.Route;

namespace Services.Run.Infrastructure.Services.Route
{
    internal sealed class RouteServiceClient: IRouteServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;

        public RouteServiceClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _httpClient = httpClient;
            _url = options.Services["route"];
          
        }

        public Task<RouteDto> GetAsync(Guid id)
            => _httpClient.GetAsync<RouteDto>($"{_url}/routes/{id}");
    }
}