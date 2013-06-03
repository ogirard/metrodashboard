// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;

using ZTG.WPF.Dashboard.Main.BusinessService;
using ZTG.WPF.Dashboard.Main.DataAccess;
using ZTG.WPF.Dashboard.Main.UserInterface;
using ZTG.WPF.Dashboard.Shared.Localization;

namespace ZTG.WPF.Dashboard.Main
{
  public class Bootstrapper : IDisposable
  {
    private ExceptionHandler _excpetionHandler;

    public MainWindow Run()
    {
      InitializeTranslation();

      var mainWindow = new MainWindow();
      _excpetionHandler = new ExceptionHandler(mainWindow);

      var dataAccess = new FeedDataAccess();
      var feedService = new FeedService(dataAccess);
      var rssNewsService = new RssNewsService();
      var optionsUIService = new OptionsUIService(feedService, rssNewsService);
      var mainWindowViewVodel = new MainWindowViewModel(dataAccess, feedService, rssNewsService, optionsUIService);
      mainWindow.ViewModel = mainWindowViewVodel;
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