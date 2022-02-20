using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddServices(config);
        
        services.AddApiVersioning(confi =>
        {
            confi.DefaultApiVersion = new ApiVersion(1, 0);
            confi.AssumeDefaultVersionWhenUnspecified = true;
            confi.AssumeDefaultVersionWhenUnspecified = true;
          
        });
        return services;
    }
}