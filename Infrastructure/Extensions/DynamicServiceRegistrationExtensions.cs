using Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class DynamicServiceRegistrationExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        var transcientServiceType = typeof(ITranscientService);

        var transcientServices = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => transcientServiceType.IsAssignableFrom(p))
            .Where(t => t.IsClass && !t.IsAbstract)
            .Select(t =>new
            {
                Service = t.GetInterfaces().FirstOrDefault(),
                Implementation = t
            }).Where(t => t.Service != null);

        foreach (var service in transcientServices)
        {
            if (transcientServiceType.IsAssignableFrom(service.Service))
            {
                services.AddTransient(service.Service, service.Implementation);
            }
        }

        return services;
    }
}