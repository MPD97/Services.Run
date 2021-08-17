using System.Collections.Generic;
using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Queries;
using Convey.Logging;
using Convey.Secrets.Vault;
using Convey.Types;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Services.Run.Application;
using Services.Run.Application.Commands;
using Services.Run.Application.DTO;
using Services.Run.Application.Queries;
using Services.Run.Infrastructure;

namespace Services.Run.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await CreateWebHostBuilder(args)
                .Build()
                .RunAsync();
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
            => WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services
                    .AddConvey()
                    .AddWebApi()
                    .AddApplication()
                    .AddInfrastructure()
                    .Build())
                .Configure(app => app
                    .UseInfrastructure()
                    .UseDispatcherEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync(ctx.RequestServices.GetService<AppOptions>().Name))
                        .Get<GetRun, RunDto>("runs/{runId}")
                        .Get<SearchRanking, PagedResult<RunRankingDto>>("runs")
                        .Post<CreateRun>("runs",
                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"runs/{cmd.RunId}"))))
                .UseLogging()
                .UseVault();
    }
}