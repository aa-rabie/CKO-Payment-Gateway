using CKO.PaymentGateway.Application.Contracts;
using CKO.PaymentGateway.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CKO.PaymentGateway.Application;

public static class ServiceRegistration
{
    /// <summary>
    /// Registering services' implementations in DI Service collection
    /// This method might take additional IConfiguration parameter in case we need to init services from Config
    /// </summary>
    /// <param name="services">DI container</param>
    /// <returns>DI container</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IDataMaskingService>(new DataMaskingService());
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
}