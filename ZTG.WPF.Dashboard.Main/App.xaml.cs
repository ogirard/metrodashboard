//----------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System.Windows;

namespace ZTG.WPF.Dashboard.Main
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private Bootstrapper _bootstrapper;
    private bool _isInitializing;

    protected override void OnStartup(StartupEventArgs e)
    {
      _isInitializing = true;
      base.OnStartup(e);

      _bootstrapper = new Bootstrapper();

      MainWindow = _bootstrapper.Run();
      MainWindow.Show();
      _isInitializing = false;
    }

    /// <summary>
    /// Exit the Application and Dispose all stuff
    /// </summary>
    public void ExitApplication()
    {
      if (_bootstrapper == null)
      {
        return;
      }

      if (!_isInitializing)
      {
        _bootstrapper.Dispose();
        _bootstrapper = null;
      }
    }

    protected override void OnExit(ExitEventArgs e)
    {
      _bootstrapper.Dispose();
      base.OnExit(e);
    }
  }
}
