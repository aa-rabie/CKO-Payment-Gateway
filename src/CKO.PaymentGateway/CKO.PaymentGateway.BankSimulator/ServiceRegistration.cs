using CKO.PaymentGateway.Application.Contracts.BankSimulator;
using CKO.PaymentGateway.BankSimulator.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace CKO.PaymentGateway.BankSimulator;

public static class ServiceRegistration
{
    /// <summary>
    /// Registering services' implementations in DI Service collection
    /// This method might take additional IConfiguration parameter in case we need to init services from Config
    /// </summary>
    /// <param name="services">DI container</param>
    /// <returns>DI container</returns>
    public static IServiceCollection AddBankServices(this IServiceCollection services)
    {
        services.AddSingleton<IPaymentProcessingService>(sp => FakePaymentProcessingServiceFactory.CreateWithTestData(sp));
        return services;
    }
}