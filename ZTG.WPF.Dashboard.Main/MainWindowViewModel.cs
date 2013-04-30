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
    private readonly OptionsUIService _optionsUIService;

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

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    public MainWindowViewModel()
    {
      _newService = new NewsService(new FeedService());
      _optionsUIService = new OptionsUIService();

      FeedItems = new ObservableCollection<FeedItemViewModel>();
      ReloadCommand = new DelegateCommand(Reload);
    }

    private void Reload()
    {
      FeedItems.Clear();
      foreach (var feedItem in _newService.GetAllFeeds().SelectMany(feed => feed.Channel.Items.Select(item => new FeedItemViewModel(feed, item))).OrderByDescending(item => item.PublicationDate))
      {
        FeedItems.Add(feedItem);
      }
    }
  }
}