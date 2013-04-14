// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataAccess.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;

namespace ZTG.WPF.Dashboard.Shared.DataAccess
{
  /// <summary>
  /// Simple, general purpose, data access layer (DAL) interface.
  /// </summary>
  public interface IDataAccess
  {
    /// <summary>
    /// Gets all entities of given type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns>The list of entities.</returns>
    IList<TEntity> GetAll<TEntity>();

    /// <summary>
    /// Gets all entities of the given type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="forceReload">if set to <c>true</c> the local entiteis will be overriden with the values from the database.</param>
    /// <returns>List with all entities of the given Type.</returns>
    IList<TEntity> GetAll<TEntity>(bool forceReload);

    /// <summary>
    /// Gets all entities with the given state that are loaded in the context.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="states">The states.</param>
    /// <returns>A list of entities loaded in the context</returns>
    IEnumerable<TEntity> GetAllInContext<TEntity>(EntityState states);

    /// <summary>
    /// Gets the linq interface to submit optimized linq queries
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity</typeparam>
    /// <param name="forceReload">Flag, indicating whether to force reloading from database.</param>
    /// <returns>A ObjectQuery that can be used to create a linq query</returns>
    IQueryable<TEntity> Query<TEntity>(bool forceReload = false) where TEntity : class;

    /// <summary>
    /// Creates a new default object of type TEntity and sets all properties defined as not null.
    /// </summary>
    /// <typeparam name="TEntity">Type of object to create</typeparam>
    /// <returns>Object of type TEntity</returns>
    TEntity CreateObject<TEntity>() where TEntity : class;

    /// <summary>
    /// Add the entity to the database context. Commit transaction or call Save() to persist the entities.
    /// Inserts or updates the given entity to the database.
    /// </summary>
    /// <param name="entity">The entity to insert or update.</param>
    void Add(object entity);

    /// <summary>
    /// Saves the database context (inserts added entities, deletes removed entities, updates, etc.)
    /// </summary>
    void Save();

    /// <summary>
    /// Deletes the specified entity from the database.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    void Delete(object entity);

    /// <summary>
    /// Begins a new transaction.
    /// </summary>
    /// <returns>Returns a general transaction scope. Can be used in "using() { .Commit(); }" semantic. </returns>
    ITransactionScope BeginTransaction();

    /// <summary>
    /// Partially commits an entity by setting the current values as original values
    /// </summary>
    /// <remarks>
    /// This only works for entities of the current context which have been modified.
    /// Note: The enitity changes are NOT saved to the db
    /// </remarks>
    /// <param name="entity">The entity.</param>
    void CommitVersionWithoutSaving(object entity);

    /// <summary>
    /// Reverts the uncommited changes on the given entity
    /// </summary>
    /// <param name="entity">The entity.</param>
    void RestoreLastVersionOrOriginal(object entity);

    /// <summary>
    /// Detaches the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    void Detach(object entity);

    /// <summary>
    /// Refreshes the specified element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="refreshMode">The refresh mode.</param>
    void Refresh(EntityObject element, RefreshMode refreshMode);

    /// <summary>
    /// Determines whether this entity can be refreshed.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns><c>true</c> if this entity can be refreshed; otherwise, <c>false</c>.</returns>
    bool CanRefreshEntity(EntityObject entity);
  }
}
