// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetroSimpleDialogView.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Window
{
  [ContentProperty("DialogContent")]
  public class MetroSimpleDialogView : Control
  {
    [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "That's the way to do it for dependency properties (reviewed: ys)")]
    static MetroSimpleDialogView()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroSimpleDialogView), new FrameworkPropertyMetadata(typeof(MetroSimpleDialogView)));
    }

    /// <summary>
    /// The DialogContent dependency property
    /// </summary>
    public static readonly DependencyProperty DialogContentProperty = DependencyProperty.Register("DialogContent", typeof(object), typeof(MetroSimpleDialogView), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the content of the dialog.
    /// </summary>
    /// <value>
    /// The content of the dialog.
    /// </value>
    public object DialogContent
    {
      get
      {
        return GetValue(DialogContentProperty);
      }

      set
      {
        SetValue(DialogContentProperty, value);
      }
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      var window = System.Windows.Window.GetWindow(this);
      if (window != null)
      {
        window.Closing += WindowOnClosing;
        window.Closed += WindowOnClosed;
        Loaded += OnLoaded;
      }
    }

    private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
    {
      // move the keyboard focus to first focusable 
      FrameworkElement frameworkElement = null; 
      var window = System.Windows.Window.GetWindow(this);
      if (window != null)
      {
        frameworkElement = window.Content as FrameworkElement;
      }

      if (frameworkElement != null)
      {
        frameworkElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
      }
      else
      {
        MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
      }
    }

    private void WindowOnClosing(object sender, CancelEventArgs cancelEventArgs)
    {
      var viewModel = DataContext as MetroSimpleDialogViewModelBase;
      if (viewModel != null)
      {
        viewModel.HandleClosing(cancelEventArgs);
      }
    }

    private void WindowOnClosed(object sender, EventArgs eventArgs)
    {
      var window = sender as System.Windows.Window;
      if (window != null)
      {
        window.Closing -= WindowOnClosing;
        window.Closed -= WindowOnClosed;
      }
    }
  }
}
