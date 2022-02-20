using Application.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services.AddValidatorsFromAssemblyContaining<IRequestValidator>();
}