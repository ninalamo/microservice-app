using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace sample.application;

public static class RegisterService
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return services;
    }
}