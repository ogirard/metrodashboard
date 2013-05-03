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
using System.Windows.Input;

using ZTG.WPF.Dashboard.Main.BusinessService;
using ZTG.WPF.Dashboard.Main.DataAccess;
using ZTG.WPF.Dashboard.Main.UserInterface;
using ZTG.WPF.Dashboard.Shared.WPF;

namespace ZTG.WPF.Dashboard.Main
{
  public class MainWindowViewModel : NotificationObject
  {
    private readonly NewsService _newsService;
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

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    public MainWindowViewModel()
    {
      _dataAccess = new FeedDataAccess();
      _feedService = new FeedService(_dataAccess);
      _newsService = new NewsService(_feedService);
      _optionsUIService = new OptionsUIService(_feedService, _newsService);

      FeedItems = new ObservableCollection<FeedItemViewModel>();
      ReloadCommand = new DelegateCommand(ReloadAsync);
    }

    private void ReloadAsync()
    {
      IsLoadingFeeds = true;
      var context = TaskScheduler.FromCurrentSynchronizationContext();
      Task.Factory.StartNew<IEnumerable<FeedItemViewModel>>(
        () =>
        _newsService.GetAllFeeds()
                    .SelectMany(feed => feed.Channel.Items.Select(item => new FeedItemViewModel(feed, item)))
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