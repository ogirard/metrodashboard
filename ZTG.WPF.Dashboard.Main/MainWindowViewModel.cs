// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.ObjectModel;

using Argotic.Syndication;

using ZTG.WPF.Dashboard.Shared.WPF;

namespace ZTG.WPF.Dashboard.Main
{
  public class MainWindowViewModel : NotificationObject
  {
    private readonly NewsService _newService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    public MainWindowViewModel()
    {
      _newService = new NewsService();
      Feeds = new ObservableCollection<RssFeed>(_newService.GetAllFeeds());
    }

    private ObservableCollection<RssFeed> _feeds;

    /// <summary>
    /// Gets or sets the Feeds.
    /// </summary>
    /// <value>The Feeds value.</value>
    public ObservableCollection<RssFeed> Feeds
    {
      get
      {
        return _feeds;
      }

      set
      {
        ChangeAndNotify(ref _feeds, value, "Feeds");
      }
    }
  }
}

