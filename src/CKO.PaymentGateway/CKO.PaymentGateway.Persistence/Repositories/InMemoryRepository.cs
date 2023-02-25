using CKO.PaymentGateway.Application.Contracts.Persistence;
using CKO.PaymentGateway.Domain.Entities;

namespace CKO.PaymentGateway.Persistence.Repositories;

/// <summary>
/// This is in-memory store
/// Normally it should implement Data-Store specific data operations
/// Data-Stores like SQL Server or Mongo DB or Dynamo DB
/// Normally this class uses ORM framework like EF Core or Data-Store Specific SDK
/// </summary>
/// <typeparam name="T">Domain Entity</typeparam>
internal class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
{
    protected List<T> DataList;
    public InMemoryRepository()
    {
        DataList = new List<T>();
    }

    /// <summary>
    /// Internal Use for seeding & testing purposes
    /// </summary>
    /// <param name="dataList">Initial Data List - Data Seed</param>
    internal InMemoryRepository(List<T> dataList)
    {
        DataList = dataList?? new List<T>();
    }

    /// <summary>
    /// Add entity object to internal data store
    /// set its Id (if it is not initialized)
    /// </summary>
    /// <param name="entity">entity to be added</param>
    /// <returns>added entity</returns>
    public Task<T> AddAsync(T entity)
    {
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }

        DataList.Add(entity);
        return Task.FromResult(entity);
    }
    /// <summary>
    /// retrieves entity instance from data store
    /// </summary>
    /// <param name="id">entity PK</param>
    /// <returns>entity instance if found or NULL</returns>
    public Task<T> GetByIdAsync(Guid id)
    {
        return Task.FromResult(DataList.FirstOrDefault(ent => ent.Id == id));
    }
}