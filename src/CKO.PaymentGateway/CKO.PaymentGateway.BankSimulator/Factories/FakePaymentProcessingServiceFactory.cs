using CKO.PaymentGateway.BankSimulator.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CKO.PaymentGateway.BankSimulator.Factories;

/// <summary>
/// Internal class used to Initialize 'FakePaymentProcessingService' instance
/// with InMemory List of valid/accepted Card Entries
/// </summary>
internal class FakePaymentProcessingServiceFactory
{
    public static FakePaymentProcessingService CreateWithTestData(IServiceProvider serviceProvider)
    {
        var acceptedCardEntries = new List<CardEntry>
        {
            new CardEntry("6771-8994-9024-5742", "739", 1, 2025, "VISA"),
            new CardEntry("3528-5073-5315-5667", "153", 2, 2027, "MASTERCARD"),
            new CardEntry("6767-0749-5751-1756", "581", 2 , 2027, "VISA")
        };
        return ActivatorUtilities.CreateInstance<FakePaymentProcessingService>(serviceProvider,acceptedCardEntries);
    }
}