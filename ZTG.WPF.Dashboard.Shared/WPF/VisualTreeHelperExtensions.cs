//----------------------------------------------------------------------------------------------------
// <copyright file="VisualTreeHelperExtensions.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;

namespace ZTG.WPF.Dashboard.Shared.WPF
{
  /// <summary>
  ///   Helper for operations on the visual tree
  /// </summary>
  public static class VisualTreeHelperExtensions
  {
    /// <summary>
    /// Traverses up the visual tree until it finds an object of the specified type or returns null
    /// </summary>
    /// <param name="dependencyObject">
    /// The dependency Object.
    /// </param>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily",
      Justification = "I need to check first and then cast")]
    public static T FindAncestor<T>(this DependencyObject dependencyObject) where T : class
    {
      DependencyObject target = dependencyObject;
      do
      {
        target = VisualTreeHelper.GetParent(target);
      }
      while (target != null && !(target is T));

      return target as T;
    }

    /// <summary>
    /// Traverses down the visual tree until it finds all objects of the specified type or returns null
    /// </summary>
    /// <param name="dependencyObject">
    /// The dependency Object.
    /// </param>
    /// <returns>
    /// The enumeration of child items
    /// </returns>
    public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject dependencyObject)
      where T : DependencyObject
    {
      if (dependencyObject != null)
      {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
        {
          DependencyObject child = VisualTreeHelper.GetChild(dependencyObject, i);
          if (child is T)
          {
            yield return (T)child;
          }

          foreach (T childOfChild in FindVisualChildren<T>(child))
          {
            yield return childOfChild;
          }
        }
      }
    }
  }
}