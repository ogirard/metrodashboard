// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

using ZTG.WPF.Dashboard.Main.BusinessService;
using ZTG.WPF.Dashboard.Main.DataAccess;
using ZTG.WPF.Dashboard.Main.UserInterface;
using ZTG.WPF.Dashboard.Shared.WPF;

namespace ZTG.WPF.Dashboard.Main
{
  public class MainWindowViewModel : NotificationObject
  {
    private readonly RssNewsService _rssNewsService;
    private readonly OptionsUIService _optionsUIService;
    private readonly IFeedDataAccess _dataAccess;
    private readonly FeedService _feedService;

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

    public ListCollectionView FeedItemCollection { get; set; }

    public ICommand ReloadCommand { get; private set; }

    public ICommand OptionsCommand
    {
      get { return _optionsUIService.OpenOptionsDialogCommand; }
    }

    private bool _isLoadingFeeds;

    /// <summary>
    /// Gets or sets the IsLoadingFeeds.
    /// </summary>
    public bool IsLoadingFeeds
    {
      get
      {
        return _isLoadingFeeds;
      }

      set
      {
        ChangeAndNotify(ref _isLoadingFeeds, value, "IsLoadingFeeds");
      }
    }

    private string _filterText;

    /// <summary>
    /// Gets or sets the FilterText.
    /// </summary>
    public string FilterText
    {
      get
      {
        return _filterText;
      }

      set
      {
        if (ChangeAndNotify(ref _filterText, value, "FilterText"))
        {
          FeedItemCollection.Refresh();
        }
      }
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    public MainWindowViewModel()
    {
      _dataAccess = new FeedDataAccess();
      _feedService = new FeedService(_dataAccess);
      _rssNewsService = new RssNewsService();
      _optionsUIService = new OptionsUIService(_feedService, _rssNewsService);

      FeedItems = new ObservableCollection<FeedItemViewModel>();
      FeedItemCollection = new ListCollectionView(FeedItems) { Filter = FilterFeedItem };

      ReloadCommand = new DelegateCommand(ReloadAsync);
    }

    private bool FilterFeedItem(object feedItemObj)
    {
      var feedItem = feedItemObj as FeedItemViewModel;
      if (feedItem == null)
      {
        return false;
      }

      if (string.IsNullOrEmpty(FilterText))
      {
        return true;
      }

      var filter = FilterText.ToLowerInvariant().Trim();

      return feedItem.Title.ToLowerInvariant().Contains(filter)
             || feedItem.Description.ToLowerInvariant().Contains(filter);
    }

    private void ReloadAsync()
    {
      IsLoadingFeeds = true;
      var context = TaskScheduler.FromCurrentSynchronizationContext();
      Task.Factory.StartNew<IEnumerable<FeedItemViewModel>>(
        () =>
        _rssNewsService.LoadFeeds(_feedService.Feeds.Select(f => f.Path))
                    .SelectMany(feed => feed.Items.Select(item => new FeedItemViewModel(item)))
                    .OrderByDescending(item => item.PublicationDate)).ContinueWith(ReloadFinished, context);
    }

    private void ReloadFinished(Task<IEnumerable<FeedItemViewModel>> reloadTask)
    {
      IsLoadingFeeds = false;

      if (reloadTask.Status != TaskStatus.RanToCompletion || !reloadTask.IsCompleted)
      {
        throw new Exception("Feeds could not be loaded!", reloadTask.Exception);
      }

      FeedItems.Clear();
      foreach (var feedItem in reloadTask.Result)
      {
        FeedItems.Add(feedItem);
      }
    }
  }
}