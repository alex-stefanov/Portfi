using System.Linq.Expressions;

namespace Portfi.Data.Repositories;

/// <summary>
/// Defines a generic repository interface for data access operations.
/// </summary>
/// <typeparam name="TType">The type of the entity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier.</typeparam>
public interface IRepository<TType, TId>
{
    /// <summary>
    /// Retrieves an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    TType? GetById(
        TId id);

    /// <summary>
    /// Asynchronously retrieves an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, null.</returns>
    Task<TType?> GetByIdAsync(
        TId id);

    /// <summary>
    /// Retrieves the first entity that matches the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to filter entities.</param>
    /// <returns>The first matching entity if found; otherwise, null.</returns>
    TType? FirstOrDefault(
        Func<TType, bool> predicate);

    /// <summary>
    /// Asynchronously retrieves the first entity that matches the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to filter entities.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the first matching entity if found; otherwise, null.</returns>
    Task<TType?> FirstOrDefaultAsync(
        Expression<Func<TType, bool>> predicate);

    /// <summary>
    /// Retrieves all entities.
    /// </summary>
    /// <returns>An enumerable collection of entities.</returns>
    IEnumerable<TType> GetAll();

    /// <summary>
    /// Asynchronously retrieves all entities.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of entities.</returns>
    Task<IEnumerable<TType>> GetAllAsync();

    /// <summary>
    /// Retrieves all entities as an <see cref="IQueryable{T}"/> that remains attached to the data source.
    /// </summary>
    /// <returns>An <see cref="IQueryable{T}"/> collection of entities.</returns>
    IQueryable<TType> GetAllAttached();

    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="item">The entity to add.</param>
    void Add(
        TType item);

    /// <summary>
    /// Asynchronously adds a new entity to the repository.
    /// </summary>
    /// <param name="item">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddAsync(
        TType item);

    /// <summary>
    /// Adds multiple entities to the repository.
    /// </summary>
    /// <param name="items">The entities to add.</param>
    void AddRange(
        TType[] items);

    /// <summary>
    /// Asynchronously adds multiple entities to the repository.
    /// </summary>
    /// <param name="items">The entities to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddRangeAsync(
        TType[] items);

    /// <summary>
    /// Deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns><c>true</c> if the entity was deleted successfully; otherwise, <c>false</c>.</returns>
    bool Delete(
        TType entity);

    /// <summary>
    /// Asynchronously deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the entity was deleted successfully.</returns>
    Task<bool> DeleteAsync(
        TType entity);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="item">The entity to update.</param>
    /// <returns><c>true</c> if the entity was updated successfully; otherwise, <c>false</c>.</returns>
    bool Update(
        TType item);

    /// <summary>
    /// Asynchronously updates an existing entity in the repository.
    /// </summary>
    /// <param name="item">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the entity was updated successfully.</returns>
    Task<bool> UpdateAsync(
        TType item);
}