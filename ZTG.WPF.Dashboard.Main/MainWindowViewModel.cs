// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using ZTG.WPF.Dashboard.Main.BusinessService;
using ZTG.WPF.Dashboard.Main.UserInterface;
using ZTG.WPF.Dashboard.Shared.WPF;

namespace ZTG.WPF.Dashboard.Main
{
  public class MainWindowViewModel : NotificationObject
  {
    private readonly NewsService _newService;

    private ObservableCollection<FeedItemViewModel> _feedItems;

    /// <summary>
    /// Gets the items of all registered feeds
    /// </summary>
    public ObservableCollection<FeedItemViewModel> FeedItems
    {
      get
      {
        return _feedItems;
      }

      private set
      {
        ChangeAndNotify(ref _feedItems, value, "FeedItems");
      }
    }

    public ICommand LoadFeedItemsCommand { get; private set; }

    public ICommand ManageFeedsCommand { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    public MainWindowViewModel()
    {
      _newService = new NewsService(new FeedService());
      FeedItems = new ObservableCollection<FeedItemViewModel>();
      LoadFeedItemsCommand = new DelegateCommand(LoadFeedItems);
      ManageFeedsCommand = new DelegateCommand(ManageFeeds);
    }

    private static void ManageFeeds()
    {
      var feedsManagerDialog = new FeedsManagerDialog
                                 {
                                   ViewModel = new FeedsManagerViewModel(),
                                   Owner = Application.Current.MainWindow
                                 };

      feedsManagerDialog.ShowDialog();
    }

    private void LoadFeedItems()
    {
      FeedItems.Clear();
      foreach (var feedItem in _newService.GetAllFeeds().SelectMany(feed => feed.Channel.Items.Select(item => new FeedItemViewModel(feed, item))).OrderByDescending(item => item.PublicationDate))
      {
        FeedItems.Add(feedItem);
      }
    }
  }
}