// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataAccess.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;

using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.DataAccess
{
  /// <summary>
  /// Entity Framework 4.0 implementation of IDataAccess.
  /// </summary>
  public class DataAccess : IDataAccess
  {
    /// <summary>
    /// Entity framework object context (actual database context).
    /// </summary>
    private readonly ObjectContext _context;

    /// <summary>
    /// Cache that mapping from Entitytype to its name
    /// </summary>
    private readonly IDictionary<Type, string> _objectSetNameMap = new Dictionary<Type, string>();

    /// <summary>
    /// Chache commited versions of entity values
    /// </summary>
    private readonly IDictionary<object, IList<object>> _commitedValues = new Dictionary<object, IList<object>>();

    /// <summary>
    /// Initializes a new instance of the <see cref="DataAccess"/> class.
    /// </summary>
    /// <param name="ctx">The ObjectContext.</param>
    public DataAccess(ObjectContext ctx)
    {
      if (ctx == null)
      {
        throw new ArgumentNullException("ctx");
      }

      _context = ctx;
    }

    /// <summary>
    /// Gets all entities of the given type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns>List with all entities of the given Type.</returns>
    public IList<TEntity> GetAll<TEntity>()
    {
      return GetAll<TEntity>(false);
    }

    /// <summary>
    /// Gets all entities of the given type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="forceReload">if set to <c>true</c> the local entiteis will be overriden with the values from the database.</param>
    /// <returns>List with all entities of the given Type.</returns>
    public IList<TEntity> GetAll<TEntity>(bool forceReload)
    {
      var entityType = GetEntitySetName(typeof(TEntity));
      var query = _context.CreateQuery<TEntity>(entityType);
      if (forceReload)
      {
        query.MergeOption = MergeOption.OverwriteChanges;
      }

      return query.OfType<TEntity>().ToList();
    }

    /// <summary>
    /// Gets all entities with the given state that are loaded in the context.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="states">The states.</param>
    /// <returns>A list of entities loaded in the context</returns>
    public IEnumerable<TEntity> GetAllInContext<TEntity>(EntityState states)
    {
      return _context.ObjectStateManager.GetObjectStateEntries(states).Select(e => e.Entity).OfType<TEntity>();
    }

    /// <summary>
    /// Gets the linq interface to submit optimized linq queries.
    /// </summary>
    /// <typeparam name="TEntity">The type of the Entity.</typeparam>
    /// <param name="forceReload">Flag, indicating whether to force reloading from database.</param>
    /// <returns>The linq query.</returns>
    public IQueryable<TEntity> Query<TEntity>(bool forceReload = false) where TEntity : class
    {
      var objectSet = _context.CreateObjectSet<TEntity>();

      if (forceReload)
      {
        objectSet.MergeOption = MergeOption.OverwriteChanges;
      }

      return objectSet;
    }

    /// <summary>
    /// Creates a new default object of type TEntity and sets all properties defined as not null.
    /// </summary>
    /// <typeparam name="TEntity">Type of object to create</typeparam>
    /// <returns>Object of type TEntity</returns>
    public TEntity CreateObject<TEntity>() where TEntity : class
    {
      return _context.CreateObject<TEntity>();
    }

    /// <summary>
    /// Add the entity to the database context. Commit transaction or call Save() to persist the entities.
    /// Inserts or updates the given entity to the database.
    /// </summary>
    /// <param name="entity">The entity to insert or update.</param>
    public void Add(object entity)
    {
      entity.ArgumentNotNull("entity");
      string entitySetName = GetEntitySetName(entity.GetType());
      _context.AddObject(entitySetName, entity);
    }

    /// <summary>
    /// Gets the name of the entity set for the given object type from metadata or the cache.
    /// </summary>
    /// <param name="entityType">Type of the entity.</param>
    /// <returns>The name of the entity set for the given object type</returns>
    private string GetEntitySetName(Type entityType)
    {
      if (!_objectSetNameMap.ContainsKey(entityType))
      {
        _objectSetNameMap[entityType] = EntityHelper.GetEntitySetName(entityType, _context);
      }

      return _objectSetNameMap[entityType];
    }

    /// <summary>
    /// Saves the database context (inserts added entities, deletes removed entities, updates, etc.)
    /// </summary>
    public void Save()
    {
      _context.SaveChanges(SaveOptions.AcceptAllChangesAfterSave | SaveOptions.DetectChangesBeforeSave);

      // clean last versions
      _commitedValues.Clear();
    }

    /// <summary>
    /// Deletes the specified entity from the database.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    public void Delete(object entity)
    {
      ObjectStateEntry stateEntry;
      if (_context.ObjectStateManager.TryGetObjectStateEntry(entity, out stateEntry))
      {
        _context.DeleteObject(entity);
      }

      // clean last versions
      if (_commitedValues.ContainsKey(entity))
      {
        _commitedValues.Remove(entity);
      }
    }

    /// <summary>
    /// Begins a new transaction.
    /// </summary>
    /// <returns>
    /// Returns a general transaction scope. Can be used in "using() { .Commit(); }" semantic.
    /// </returns>
    public ITransactionScope BeginTransaction()
    {
      return new TransactionScope();
    }

    /// <summary>
    /// Partially commits an entity by setting the current values as original values (Relations are NOT considered!)
    /// </summary>
    /// <remarks>
    /// This only works for entities of the current context which have been modified.
    /// The changes are NOT SAVED to the db, the entity's state will remain <see cref="EntityState.Modified"/>.
    /// </remarks>
    /// <param name="entity">The entity.</param>
    public void CommitVersionWithoutSaving(object entity)
    {
      ObjectStateEntry stateEntry;
      if (_context.ObjectStateManager.TryGetObjectStateEntry(entity, out stateEntry) == false || (stateEntry.State != EntityState.Modified && stateEntry.State != EntityState.Added))
      {
        return;
      }

      var version = Copy(stateEntry.CurrentValues);

      if (_commitedValues.ContainsKey(entity))
      {
        _commitedValues[entity] = version;
      }
      else
      {
        _commitedValues.Add(entity, version);
      }
    }

    /// <summary>
    /// Reverts the uncommited changes on the given entity (Relations are NOT considered!)
    /// </summary>
    /// <remarks>
    /// This only works for entities of the current context which have been modified (i.e. State == <see cref="EntityState.Modified"/>).
    /// The entity's state will remain <see cref="EntityState.Modified"/>, only the values will be set back to the original or last committed
    /// values (see <see cref="CommitVersionWithoutSaving"/>).
    /// </remarks>
    /// <param name="entity">The entity.</param>
    public void RestoreLastVersionOrOriginal(object entity)
    {
      ObjectStateEntry stateEntry;
      if (!_context.ObjectStateManager.TryGetObjectStateEntry(entity, out stateEntry) || (stateEntry.State != EntityState.Modified && stateEntry.State != EntityState.Added))
      {
        return;
      }

      // if the values are not on the committed values stack, then return if the entity was added.
      if (!_commitedValues.ContainsKey(entity) && stateEntry.State == EntityState.Added)
      {
        return;
      }

      // if committed version exists, take these values, otherwise fall back to original values
      var originalValues = _commitedValues.ContainsKey(entity) ? _commitedValues[entity] : Copy(stateEntry.OriginalValues);

      // reset the modified fields to the last commited version
      for (int i = 0; i < originalValues.Count; i++)
      {
        stateEntry.CurrentValues.SetValue(i, originalValues[i]);
      }
    }

    /// <summary>
    /// Refreshes the specified element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="refreshMode">The refresh mode.</param>
    public void Refresh(EntityObject element, RefreshMode refreshMode)
    {
      _context.Refresh(refreshMode, element);
    }

    /// <summary>
    /// Determines whether this entity can be refreshed.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns><c>true</c> if this entity can be refreshed; otherwise, <c>false</c>.</returns>
    public bool CanRefreshEntity(EntityObject entity)
    {
      ObjectStateEntry stateEntry;
      if (!_context.ObjectStateManager.TryGetObjectStateEntry(entity, out stateEntry))
      {
        return false;
      }

      return stateEntry.State != EntityState.Added && stateEntry.State != EntityState.Deleted;
    }

    /// <summary>
    /// Makes a copy of the given DB record.
    /// </summary>
    /// <param name="record">The record.</param>
    /// <returns></returns>
    private static IList<object> Copy(DbDataRecord record)
    {
      var list = new List<object>();
      for (int i = 0; i < record.FieldCount; i++)
      {
        list.Add(record.GetValue(i));
      }

      return list;
    }

    /// <summary>
    /// Detaches the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public void Detach(object entity)
    {
      ObjectStateEntry stateEntry;
      if (_context.ObjectStateManager.TryGetObjectStateEntry(entity, out stateEntry))
      {
        _context.Detach(entity);
      }

      // clean last versions
      if (_commitedValues.ContainsKey(entity))
      {
        _commitedValues.Remove(entity);
      }
    }
  }
}
