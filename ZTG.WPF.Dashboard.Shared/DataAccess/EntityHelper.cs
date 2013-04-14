// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityHelper.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ZTG.WPF.Dashboard.Shared.DataAccess
{
  /// <summary>
  /// Helperclass for handling Entitynames and EntitySets.
  /// </summary>
  public static class EntityHelper
  {
    /// <summary>
    /// Loads assembly into workspace.
    /// </summary>
    /// <param name="workspace">The workspace.</param>
    /// <param name="assembly">The assembly.</param>
    private static void LoadAssemblyIntoWorkspace(MetadataWorkspace workspace, Assembly assembly)
    {
      workspace.LoadFromAssembly(assembly);
    }

    #region GetEntitySetName

    /// <summary>
    /// Gets the name of the entity set.
    /// </summary>
    /// <param name="entityType">Type of the entity.</param>
    /// <param name="context">The security context.</param>
    /// <returns>The name of the entity set.</returns>
    public static string GetEntitySetName(Type entityType, ObjectContext context)
    {
      context.ArgumentNotNull("context");
      Type entityToFind = GetEntityTypeDirectlyDerivedFromEntityObject(entityType);
      EntityType edmEntityType = GetEntityType(context, entityToFind);
      EntityContainer container = context.MetadataWorkspace.GetItems<EntityContainer>(DataSpace.CSpace).Single<EntityContainer>();
      EntitySet set = (EntitySet)container.BaseEntitySets.Single(p => p.ElementType == edmEntityType);

      return container.Name + "." + set.Name;
    }

    /// <summary>
    /// Gets entity type directly derived from EntityObject.
    /// </summary>
    /// <param name="entityType">Type of the entity.</param>
    /// <returns>The entity type directly derived from EntityObject</returns>
    private static Type GetEntityTypeDirectlyDerivedFromEntityObject(Type entityType)
    {
      return entityType.BaseType == typeof(EntityObject) ? entityType : GetEntityTypeDirectlyDerivedFromEntityObject(entityType.BaseType);
    }

    #endregion

    #region GetEntityType

    /// <summary>
    /// Gets the type of the entity.
    /// </summary>
    /// <param name="context">The ObjectContext.</param>
    /// <param name="clrType">Type of the CLR.</param>
    /// <returns>Type of the entity</returns>
    [DebuggerStepThrough]
    public static EntityType GetEntityType(ObjectContext context, Type clrType)
    {
      if (context == null)
      {
        throw new ArgumentNullException("context");
      }

      if (clrType == null)
      {
        throw new ArgumentNullException("clrType");
      }

      EdmType type;

      try
      {
        type = context.MetadataWorkspace.GetType(clrType.Name, clrType.Namespace, DataSpace.OSpace);
      }
      catch (ArgumentException)
      {
        LoadAssemblyIntoWorkspace(context.MetadataWorkspace, clrType.Assembly);
        type = context.MetadataWorkspace.GetType(clrType.Name, clrType.Namespace, DataSpace.OSpace);
      }

      return (EntityType)context.MetadataWorkspace.GetEdmSpaceType((StructuralType)type);
    }

    /// <summary>
    /// Tries to get the type of the entity.
    /// </summary>
    /// <param name="context">The ObjectContext.</param>
    /// <param name="clrType">The CLR - Type.</param>
    /// <param name="entityType">Type of the entity.</param>
    /// <returns>Type of the entity</returns>
    public static bool TryGetEntityType(ObjectContext context, Type clrType, out EntityType entityType)
    {
      entityType = null;
      if (context == null)
      {
        throw new ArgumentNullException("context");
      }

      if (clrType == null)
      {
        throw new ArgumentNullException("clrType");
      }

      EdmType type;

      bool flag = context.MetadataWorkspace.TryGetType(clrType.Name, clrType.Namespace, DataSpace.OSpace, out type);
      if (!flag)
      {
        LoadAssemblyIntoWorkspace(context.MetadataWorkspace, clrType.Assembly);
        flag = context.MetadataWorkspace.TryGetType(clrType.Name, clrType.Namespace, DataSpace.OSpace, out type);
      }

      if (flag)
      {
        entityType = (EntityType)context.MetadataWorkspace.GetEdmSpaceType((StructuralType)type);
        return true;
      }

      return false;
    }
    #endregion
  }
}
