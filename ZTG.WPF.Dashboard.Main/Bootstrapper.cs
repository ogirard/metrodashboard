// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;

using Ninject;

using ZTG.WPF.Dashboard.Main.BusinessService;
using ZTG.WPF.Dashboard.Main.DataAccess;
using ZTG.WPF.Dashboard.Main.UserInterface;
using ZTG.WPF.Dashboard.Shared.Localization;

namespace ZTG.WPF.Dashboard.Main
{
  public class Bootstrapper : IDisposable
  {
    private readonly IKernel _kernel;

    private ExceptionHandler _excpetionHandler;

    public Bootstrapper()
    {
      // setup DI container
      _kernel = new StandardKernel();
      _kernel.Bind<IFeedDataAccess>().To<FeedDataAccess>();
      _kernel.Bind<FeedService>().ToSelf();
      _kernel.Bind<RssNewsService>().ToSelf();
      _kernel.Bind<OptionsUIService>().ToSelf().InSingletonScope();
      _kernel.Bind<MainWindowViewModel>().ToSelf().InSingletonScope();
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