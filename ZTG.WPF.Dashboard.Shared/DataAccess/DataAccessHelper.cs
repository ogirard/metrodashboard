// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataAccessHelper.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.DataAccess
{
  public static class DataAccessHelper
  {
    /// <summary>
    /// Refreshs all entity collections of the given entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="mergeOption">The merge option to use for reloading</param>
    /// <param name="accessor">The data accessor.</param>
    public static void RefreshAllEntityCollections(EntityObject entity, MergeOption mergeOption, IDataAccess accessor)
    {
      RefreshAllEntityCollections(entity, mergeOption, accessor, new List<EntityObject>());
    }

    /// <summary>
    /// Refreshs all entity collections of the given entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="mergeOption">The merge option to use for reloading</param>
    /// <param name="accessor">The data accessor.</param>
    /// <param name="alreadyRefreshedObjects">A list with objects that don't need to be reloaded.</param>
    public static void RefreshAllEntityCollections(EntityObject entity, MergeOption mergeOption, IDataAccess accessor, IList<EntityObject> alreadyRefreshedObjects)
    {
      entity.ArgumentNotNull("entity");
      accessor.ArgumentNotNull("accessor");
      alreadyRefreshedObjects.ArgumentNotNull("alreadyRefreshedObjects");

      if (!accessor.CanRefreshEntity(entity))
      {
        return;
      }

      foreach (var end in ((IEntityWithRelationships)entity).RelationshipManager.GetAllRelatedEnds())
      {
        end.Load(mergeOption);
        var elements = new List<EntityObject>();
        foreach (var element in end)
        {
          var entityObject = (EntityObject)element;
          if (!alreadyRefreshedObjects.Contains(entityObject))
          {
            elements.Add(entityObject);
          }
        }

        foreach (var element in elements)
        {
          if (element.EntityState != EntityState.Added)
          {
            accessor.Refresh(element, RefreshMode.StoreWins);
          }
        }
      }
    }
  }
}
