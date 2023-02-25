using CKO.PaymentGateway.Domain.Entities;

namespace CKO.PaymentGateway.Application.Contracts.Persistence;

/// <summary>
/// defines common data operations 
/// </summary>
/// <typeparam name="T">Domain entity</typeparam>
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Get Single Entity using Id
    /// </summary>
    /// <param name="id">Entity Identifier</param>
    /// <returns>Entity Instance or NULL</returns>
    Task<T> GetByIdAsync(Guid id);
    /// <summary>
    /// Add new entity to data store
    /// </summary>
    /// <param name="entity">instance to be added</param>
    /// <returns>Added entity</returns>
    Task<T> AddAsync(T entity);
}