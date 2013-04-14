// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetroWindowCaption.xaml.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

using ZTG.WPF.Dashboard.Shared.Extensions;
using ZTG.WPF.Dashboard.Shared.Localization;
using ZTG.WPF.Dashboard.Shared.Native;
using ZTG.WPF.Dashboard.Shared.WPF;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Window
{
  /// <summary>
  /// Interaction logic for MetroWindowCaption.xaml
  /// </summary>
  internal partial class MetroWindowCaption : INotifyPropertyChanged
  {
    private DelegateCommand _closeCommand;

    /// <summary>
    /// Gets the CloseCommand.
    /// </summary>
    public DelegateCommand CloseCommand
    {
      get
      {
        return _closeCommand;
      }

      private set
      {
        ChangeAndNotify(ref _closeCommand, value, "CloseCommand");
      }
    }

    private DelegateCommand _maximizeOrRestoreCommand;

    /// <summary>
    /// Gets the MaximizeOrRestoreCommand.
    /// </summary>
    public DelegateCommand MaximizeOrRestoreCommand
    {
      get
      {
        return _maximizeOrRestoreCommand;
      }

      private set
      {
        ChangeAndNotify(ref _maximizeOrRestoreCommand, value, "MaximizeOrRestoreCommand");
      }
    }

    private DelegateCommand _minimizeCommand;

    /// <summary>
    /// Gets the MinimizeCommand.
    /// </summary>
    public DelegateCommand MinimizeCommand
    {
      get
      {
        return _minimizeCommand;
      }

      private set
      {
        ChangeAndNotify(ref _minimizeCommand, value, "MinimizeCommand");
      }
    }

    private MetroWindowBase _parentWindow;

    public MetroWindowBase ParentWindow
    {
      get
      {
        return _parentWindow;
      }

      private set
      {
        ChangeAndNotify(ref _parentWindow, value, "ParentWindow");
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="MetroWindowCaption"/> class.
    /// </summary>
    public MetroWindowCaption()
    {
      InitializeComponent();

      Loaded += OnLoadedHandler;
      Unloaded += OnUnloadedHandler;
    }

    private void OnLoadedHandler(object sender, RoutedEventArgs e)
    {
      ParentWindow = System.Windows.Window.GetWindow(this) as MetroWindowBase;

      // init default commands
      CloseCommand = new DelegateCommand(Close, CanClose);
      MinimizeCommand = new DelegateCommand(Minimize, CanMinimize);
      MaximizeOrRestoreCommand = new DelegateCommand(MaximizeOrRestore, CanMaximizeOrRestore);

      ParentWindow.AddValueChangedHandler(System.Windows.Window.WindowStateProperty, WindowStateChangedHandler);
    }

    private void OnUnloadedHandler(object sender, RoutedEventArgs e)
    {
      ParentWindow.RemoveValueChangedHandler(System.Windows.Window.WindowStateProperty, WindowStateChangedHandler);
      Loaded -= OnLoadedHandler;
      Unloaded -= OnUnloadedHandler;
    }

    private void WindowStateChangedHandler(object sender, EventArgs e)
    {
      _maximizeOrRestoreButton.ToolTip = _parentWindow.WindowState == WindowState.Normal ?
                                           "TCG.MS.UserInterface.Controls.Windows.MetroWindowCaption.Menu.Maximize.Text".TranslateText() :
                                           "TCG.MS.UserInterface.Controls.Windows.MetroWindowCaption.Menu.Restore.Text".TranslateText();
    }

    private bool _windowMaximizedByMouseDoubleClick;

    protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
    {
      base.OnMouseDoubleClick(e);

      if (MaximizeOrRestoreCommand != null && CanMaximizeOrRestore())
      {
        MaximizeOrRestoreCommand.Execute(null);

        // Workaround: Set flag when window was maximized by double click. So you can ignore the subsequent MouseMove event.
        _windowMaximizedByMouseDoubleClick = _parentWindow.WindowState == WindowState.Maximized;
      }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      try
      {
        base.OnMouseMove(e);

        if ((_parentWindow == null) || (e.LeftButton != MouseButtonState.Pressed) || _windowMaximizedByMouseDoubleClick)
        {
          // Workaround: Always reset the following flag.
          _windowMaximizedByMouseDoubleClick = false;
          return;
        }

        if (_parentWindow.WindowState == WindowState.Maximized)
        {
          // Set normal window position
          var restoreBounds = _parentWindow.RestoreBounds;
          var mouseScreenPosition = PointToScreen(e.GetPosition(null));
          NativeMethods.RECT maximizedScreenRect;
          NativeMethods.GetWindowRect(_parentWindow.GetHandle(), out maximizedScreenRect);

          double x;
          if ((mouseScreenPosition.X - (restoreBounds.Width / 2)) < maximizedScreenRect.Left)
          {
            x = maximizedScreenRect.Left;
          }
          else if ((mouseScreenPosition.X + (restoreBounds.Width / 2)) > maximizedScreenRect.Right)
          {
            x = maximizedScreenRect.Right - restoreBounds.Width;
          }
          else
          {
            x = mouseScreenPosition.X - (restoreBounds.Width / 2);
          }

          _parentWindow.Top = 0;
          _parentWindow.Left = x;

          // Restore the normal window state
          _parentWindow.WindowState = WindowState.Normal;
        }

        _parentWindow.DragMove();
      }
      catch (InvalidOperationException)
      {
        // DragMove rarely throws an exception: "Can only call DragMove when primary mouse button is down."
        // Workaround: Catch the exception and do nothing...
      }
    }

    private void Close()
    {
      if (!CanClose())
      {
        return;
      }

      _parentWindow.Close();
    }

    private bool CanClose()
    {
      return _parentWindow != null && _parentWindow.ShowCloseButton;
    }

    private void MaximizeOrRestore()
    {
      if (!CanMaximizeOrRestore())
      {
        return;
      }

      _parentWindow.WindowState = _parentWindow.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
    }

    private bool CanMaximizeOrRestore()
    {
      return _parentWindow != null && _parentWindow.ShowMaximizeOrRestoreButton;
    }

    private void Minimize()
    {
      if (!CanMinimize())
      {
        return;
      }

      // minimize complete window stack of Management Suite
      NativeMethods.ShowWindow(Process.GetCurrentProcess().MainWindowHandle, NativeMethods.ShowWindowCommand.SW_MINIMIZE);
      NativeMethods.ShowWindow(_parentWindow.GetHandle(), NativeMethods.ShowWindowCommand.SW_MINIMIZE);
    }

    private bool CanMinimize()
    {
      return _parentWindow != null && _parentWindow.ShowMinimizeButton;
    }

    public void NotifyStateChanged()
    {
      NotifyCanExecuteChanged(CloseCommand);
      NotifyCanExecuteChanged(MinimizeCommand);
      NotifyCanExecuteChanged(MaximizeOrRestoreCommand);
    }

    private static void NotifyCanExecuteChanged(DelegateCommand command)
    {
      if (command != null)
      {
        command.RaiseCanExecuteChanged();
      }
    }

    private void ChangeAndNotify<T>(ref T field, T value, string propertyName)
    {
      PropertyChanged.ChangeAndNotify(ref field, value, this, propertyName);
    }
  }
}
