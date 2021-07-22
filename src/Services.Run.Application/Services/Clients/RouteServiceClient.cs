using System;
using System.Threading.Tasks;
using Convey.HTTP;
using Convey.Secrets.Vault;
using Convey.WebApi.Security;
using Services.Run.Application.DTO;
using Services.Run.Application.Services.Route;

namespace Services.Run.Application.Services.Clients
{
    internal sealed class RouteServiceClient: IRouteServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;

        public RouteServiceClient(IHttpClient httpClient, HttpClientOptions options,
            ICertificatesService certificatesService, VaultOptions vaultOptions, SecurityOptions securityOptions)
        {
            _httpClient = httpClient;
            _url = options.Services["route"];
            if (!vaultOptions.Enabled || vaultOptions.Pki?.Enabled != true)
            {
                return;
            }

            var certificate = certificatesService.Get(vaultOptions.Pki.RoleName);
            if (certificate is null)
            {
                return;
            }

            var header = securityOptions.Certificate.GetHeaderName();
            var certificateData = certificate.GetRawCertDataString();
            _httpClient.SetHeaders(h => h.Add(header, certificateData));
        }

        public Task<RouteDto> GetAsync(Guid id)
            => _httpClient.GetAsync<RouteDto>($"{_url}/routes/{id}");
    }
}