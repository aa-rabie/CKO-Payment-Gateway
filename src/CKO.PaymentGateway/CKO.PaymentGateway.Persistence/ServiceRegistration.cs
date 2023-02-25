using CKO.PaymentGateway.Application.Contracts.Persistence;
using CKO.PaymentGateway.Persistence.Factories;
using CKO.PaymentGateway.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CKO.PaymentGateway.Persistence;

public static class ServiceRegistration
{
    /// <summary>
    /// Registering services' implementations in DI Service collection
    /// This method might take additional IConfiguration parameter in case we need to init services from Config
    /// </summary>
    /// <param name="services">DI container</param>
    /// <returns>DI container</returns>
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        // AddSingleton is used because in this app we are using InMemory repository 
        // In Normal Web Application => AddScoped would be used
        services.AddSingleton(typeof(IRepository<>), typeof(InMemoryRepository<>));
        services.AddSingleton<IMerchantRepository, MerchantRepository>(cp => MerchantRepositoryFactory.CreateWithTestData());
        return services;
    }
}