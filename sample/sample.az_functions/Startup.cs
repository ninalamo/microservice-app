using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using sample.application;
using sample.az_functions;

[assembly: FunctionsStartup(typeof(Startup))]

namespace sample.az_functions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddApplication();
    }
}