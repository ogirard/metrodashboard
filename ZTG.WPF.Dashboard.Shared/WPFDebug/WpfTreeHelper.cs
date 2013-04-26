// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WpfTreeHelper.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Media;

namespace ZTG.WPF.Dashboard.Shared.WPFDebug
{
  /// <summary>
  /// Helper methods to print out WPF tree informations
  /// </summary>
  public static class WpfTreeHelper
  {
    /// <summary>
    /// Prints the logical tree. Print in constructor: this.PrintLogicalTree();
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="depth">The depth.</param>
    public static void PrintLogicalTree(this object element, int depth = 0)
    {
      var dependencyObject = element as DependencyObject;
      if (dependencyObject == null)
      {
        return;
      }

      System.Diagnostics.Debug.WriteLine(new string(' ', depth) + dependencyObject);

      foreach (var child in LogicalTreeHelper.GetChildren(dependencyObject))
      {
        child.PrintLogicalTree(depth + 1);
      }
    }

    /// <summary>
    /// Prints the visual tree. Print in OnContentRendered(): this.PrintVisualTree()
    /// </summary>
    /// <param name="dependencyObject">The dependency object.</param>
    /// <param name="depth">The depth.</param>
    public static void PrintVisualTree(this DependencyObject dependencyObject, int depth = 0)
    {
      if (dependencyObject == null)
      {
        return;
      }

      System.Diagnostics.Debug.WriteLine(new string(' ', depth) + dependencyObject);

      for (var i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
      {
        var child = VisualTreeHelper.GetChild(dependencyObject, i);
        child.PrintVisualTree(depth + 1);
      }
    }
  }
}