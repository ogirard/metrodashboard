//----------------------------------------------------------------------------------------------------
// <copyright file="ResourceDictionaryExtensions.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System.Windows;

using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Extensions
{
  /// <summary>
  ///   Provides extensions for a <see cref="ResourceDictionary" />
  /// </summary>
  public static class ResourceDictionaryExtensions
  {
    /// <summary>
    /// Gets the resource with the given resourceKey and of the given type
    /// </summary>
    /// <typeparam name="T">
    /// The expected type of the resource
    /// </typeparam>
    /// <param name="resourceDictionary">
    /// The resource dictionary.
    /// </param>
    /// <param name="resourceKey">
    /// The resourceKey.
    /// </param>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    /// <exception cref="ResourceReferenceKeyNotFoundException">
    /// Thrown if a resource with the given resourceKey and type could not be found
    /// </exception>
    public static T GetResource<T>(this ResourceDictionary resourceDictionary, string resourceKey)
    {
      resourceDictionary.ArgumentNotNull("resourceDictionary");

      if (resourceDictionary.Contains(resourceKey) && resourceDictionary[resourceKey] is T)
      {
        return (T)resourceDictionary[resourceKey];
      }

      throw new ResourceReferenceKeyNotFoundException("Resource not found", resourceKey);
    }

    /// <summary>
    /// Gets the resource with the given resourceKey and of the given type
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <param name="frameworkElement">
    /// The framework element.
    /// </param>
    /// <param name="resourceKey">
    /// The resource key.
    /// </param>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    /// <exception cref="ResourceReferenceKeyNotFoundException">
    /// Thrown if a resource with the given resourceKey and type could not be found
    /// </exception>
    public static T GetResource<T>(this FrameworkElement frameworkElement, string resourceKey)
    {
      frameworkElement.ArgumentNotNull("frameworkElement");
      object resource = frameworkElement.TryFindResource(resourceKey);

      if (resource is T)
      {
        return (T)resource;
      }

      Window window = Window.GetWindow(frameworkElement);
      if (window != null)
      {
        return window.Resources.GetResource<T>(resourceKey);
      }

      throw new ResourceReferenceKeyNotFoundException("Resource not found", resourceKey);
    }
  }
}