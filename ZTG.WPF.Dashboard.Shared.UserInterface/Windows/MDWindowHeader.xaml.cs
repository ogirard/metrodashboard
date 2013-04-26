// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MDWindowHeader.xaml.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Windows
{
  /// <summary>
  /// Interaction logic for MDWindowHeader.xaml
  /// </summary>
  public partial class MDWindowHeader
  {
    /// <summary>
    /// The ParentWindow dependency property
    /// </summary>
    public static readonly DependencyPropertyKey ParentWindowPropertyKey = DependencyProperty.RegisterReadOnly("ParentWindow", typeof(MDWindow), typeof(MDWindowHeader), new PropertyMetadata(null));

    public static readonly DependencyProperty ParentWindowProperty = ParentWindowPropertyKey.DependencyProperty;

    /// <summary>
    /// Gets the parent window.
    /// </summary>
    public MDWindow ParentWindow
    {
      get
      {
        return (MDWindow)GetValue(ParentWindowProperty);
      }

      private set
      {
        SetValue(ParentWindowPropertyKey, value);
      }
    }

    public MDWindowHeader()
    {
      InitializeComponent();
      Loaded += OnLoadedHandler;
    }

    private void OnLoadedHandler(object sender, RoutedEventArgs e)
    {
      ParentWindow = Window.GetWindow(this) as MDWindow;
    }

    protected override void OnMouseDoubleClick(System.Windows.Input.MouseButtonEventArgs e)
    {
      base.OnMouseDoubleClick(e);
      MaximizeOrRestore();
    }

    private void MaximizeOrRestore()
    {
      ParentWindow.WindowState = ParentWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
      _maxRestoreButton.ToolTip = ParentWindow.WindowState == WindowState.Maximized ? "Restore" : "Maximize";
    }

    protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
    {
      base.OnMouseDown(e);
      ParentWindow.DragMove();
    }

    private void MinimizeButtonClickHandler(object sender, RoutedEventArgs e)
    {
      ParentWindow.WindowState = WindowState.Minimized;
    }

    private void MaximizeOrRestoreButtonClickHandler(object sender, RoutedEventArgs e)
    {
      MaximizeOrRestore();
    }

    private void CloseButtonClickHandler(object sender, RoutedEventArgs e)
    {
      ParentWindow.Close();
    }
  }
}
