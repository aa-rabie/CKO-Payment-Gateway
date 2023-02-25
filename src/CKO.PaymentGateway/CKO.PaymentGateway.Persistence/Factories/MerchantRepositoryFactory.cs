using CKO.PaymentGateway.Domain.Entities;
using CKO.PaymentGateway.Persistence.Repositories;

namespace CKO.PaymentGateway.Persistence.Factories;

/// <summary>
/// Internal class to Init MerchantRepository with valid list of merchants as testing data
/// </summary>
internal class MerchantRepositoryFactory
{
    public static MerchantRepository CreateWithTestData()
    {
        var testData = new List<Merchant>()
        {
            new Merchant()
            {
                Id = Guid.Parse("B90CF485-7CA4-40C5-A989-BCEC92110BD0"),
                Name = "First Merchant"
            },
            new Merchant()
            {
                Id = Guid.Parse("FB88D428-5EC7-4D4C-9D8D-084AC5D1492D"),
                Name = "Second Merchant"
            }
        };
        return new MerchantRepository(testData);
    }
}