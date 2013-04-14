// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WpfHelper.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Media3D;

using ZTG.WPF.Dashboard.Shared.Native;
using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.WPF
{
  /// <summary>
  /// WPF Helper functions
  /// </summary>
  public static class WpfHelper
  {
    /// <summary>
    /// Searchs the visual tree upward for a specific type 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "OK in this case")]
    public static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
    {
      while ((source != null) && (source is Visual || source is Visual3D) && (source.GetType() != typeof(T)))
      {
        source = VisualTreeHelper.GetParent(source);
      }

      return source;
    }

    /// <summary>
    /// Finds a child control in the Visual Subtree of the given source element.
    /// </summary>
    /// <param name="source">The source (root).</param>
    /// <param name="condition">The condition for the child control to match.</param>
    /// <returns></returns>
    public static DependencyObject FindChild(DependencyObject source, Func<DependencyObject, bool> condition)
    {
      if (source == null || condition == null)
      {
        return source;
      }

      var searchQueue = new Queue<DependencyObject>();
      searchQueue.Enqueue(source);

      while (searchQueue.Count > 0)
      {
        var root = searchQueue.Dequeue();
        if (condition(root))
        {
          return root;
        }

        var childrenCount = VisualTreeHelper.GetChildrenCount(root);
        for (var i = 0; i < childrenCount; i++)
        {
          searchQueue.Enqueue(VisualTreeHelper.GetChild(root, i));
        }
      }

      return source;
    }

    /// <summary>
    /// Applies the given action to all visual children and to the given root.
    /// </summary>
    /// <param name="root">The root.</param>
    /// <param name="action">The action.</param>
    public static void ApplyToAllChildren(this DependencyObject root, Action<DependencyObject> action)
    {
      action.ArgumentNotNull("action");

      if (root == null)
      {
        return;
      }

      action(root);

      for (int i = 0; i < VisualTreeHelper.GetChildrenCount(root); i++)
      {
        ApplyToAllChildren(VisualTreeHelper.GetChild(root, i), action);
      }
    }

    /// <summary>
    /// Gets the cummulated top margin and padding between child and parent top border
    /// </summary>
    /// <param name="child">The child.</param>
    /// <param name="parent">The parent.</param>
    /// <returns></returns>
    public static double GetTopMarginAndPadding(FrameworkElement child, FrameworkElement parent)
    {
      if (child == null)
      {
        return 0;
      }

      var topMarginAndPadding = child.Margin.Top;
      var childControl = child as Control;
      if (childControl != null)
      {
        topMarginAndPadding += childControl.Padding.Top;
      }

      if (Equals(child, parent))
      {
        return topMarginAndPadding;
      }

      var currentParent = (child.Parent ?? child.TemplatedParent) as FrameworkElement;

      while (currentParent != null && !Equals(currentParent, parent))
      {
        topMarginAndPadding += currentParent.Margin.Top;
      }

      var parentControl = parent as Control;
      if (parentControl != null)
      {
        topMarginAndPadding += parentControl.Padding.Top;
      }

      return topMarginAndPadding;
    }

    /// <summary>
    /// Adds a value changed handler for the <see cref="property"/> on the given dependency object (the property has to be valid for its type)
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <param name="property">The property.</param>
    /// <param name="handler">The handler.</param>
    public static void AddValueChangedHandler(this DependencyObject obj, DependencyProperty property, EventHandler handler)
    {
      obj.ArgumentNotNull("obj");
      property.ArgumentNotNull("property");
      handler.ArgumentNotNull("handler");

      var dependencyPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(property, obj.GetType());
      dependencyPropertyDescriptor.AddValueChanged(obj, handler);
    }

    /// <summary>
    /// Removes a value changed handler for the <see cref="property"/> on the given dependency object (the property has to be valid for its type)
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <param name="property">The property.</param>
    /// <param name="handler">The handler.</param>
    public static void RemoveValueChangedHandler(this DependencyObject obj, DependencyProperty property, EventHandler handler)
    {
      obj.ArgumentNotNull("obj");
      property.ArgumentNotNull("property");
      handler.ArgumentNotNull("handler");

      var dependencyPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(property, obj.GetType());
      dependencyPropertyDescriptor.RemoveValueChanged(obj, handler);
    }

    /// <summary>
    /// Gets the tool icon path according to the string <paramref name="path"/>.
    /// </summary>
    /// <returns>The icon as <see cref="Geometry" /> or <tt>null</tt></returns>
    public static Geometry AsGeometry(string path)
    {
      if (string.IsNullOrEmpty(path))
      {
        return null;
      }

      try
      {
        return Geometry.Parse(path);
      }
      catch (FormatException)
      {
        // don't care, return empty path
        return null;
      }
    }

    /// <summary>
    /// Starts a drag resize at current mouse position for the given window
    /// </summary>
    /// <param name="window">The window.</param>
    /// <param name="direction">The direction.</param>
    public static void DragResize(Window window, WindowResizeDirection direction)
    {
      var windowInterop = new WindowInteropHelper(window);
      windowInterop.EnsureHandle();

      NativeMethods.SendMessage(windowInterop.Handle, NativeMethods.WM_SYSCOMMAND, (IntPtr)(61440 + direction), IntPtr.Zero);
    }
  }
}
