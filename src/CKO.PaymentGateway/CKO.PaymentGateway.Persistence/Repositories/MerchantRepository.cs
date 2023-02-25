using CKO.PaymentGateway.Application.Contracts.Persistence;
using CKO.PaymentGateway.Domain.Entities;

namespace CKO.PaymentGateway.Persistence.Repositories;

/// <summary>
/// These class is only defined as an example implementation if
///     1. there are Merchant specific data operations that we need to implement
/// otherwise
///     1. this class and related interface can be removed. 
/// </summary>
internal class MerchantRepository : InMemoryRepository<Merchant>, IMerchantRepository
{
    /// <summary>
    /// This constructor id defined only to seed the app with Merchant testing data
    /// </summary>
    /// <param name="dataList">Seed Data</param>
    internal MerchantRepository(List<Merchant> dataList) : base(dataList)
    {
    }
}