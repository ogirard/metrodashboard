// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.IO;
using System.Reflection;

using Ninject;
using Ninject.Extensions.Conventions;

using ZTG.WPF.Dashboard.Main.DataAccess;
using ZTG.WPF.Dashboard.Shared.Localization;

namespace ZTG.WPF.Dashboard.Main
{
  public class Bootstrapper : IDisposable
  {
    private readonly StandardKernel _kernel;

    private ExceptionHandler _excpetionHandler;

    public Bootstrapper()
    {
      // setup DI container
      _kernel = new StandardKernel();
      _kernel.Bind<IFeedDataAccess>().To<FeedDataAccess>();
      _kernel.Bind<MainWindowViewModel>().ToSelf().InSingletonScope();

      // auto-bind all services
      _kernel.Bind(scanner => scanner.FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                                    .Select(s => s.Name.Contains("Service"))
                                    .BindToSelf()
                                    .Configure(binding => binding.InSingletonScope()));
    }

    public MainWindow Run()
    {
      InitializeTranslation();

      var mainWindow = new MainWindow();
      _excpetionHandler = new ExceptionHandler(mainWindow);
      mainWindow.ViewModel = _kernel.Get<MainWindowViewModel>();
      return mainWindow;
    }

    private static void InitializeTranslation()
    {
      TranslationManager.InitTranslationManager();
      TranslationManager.Language = new CultureInfo("en");
    }

    /// <summary>
    /// Dispose data containers
    /// </summary>
    protected virtual void Dispose(bool unmanaged)
    {
      if (_excpetionHandler != null)
      {
        _excpetionHandler.Dispose();
      }
    }

    /// <summary>
    /// Dispose resources.
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }
}