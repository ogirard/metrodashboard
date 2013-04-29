// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedsManagerUIService.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using ZTG.WPF.Dashboard.Main.BusinessService;
using ZTG.WPF.Dashboard.Main.Model;
using ZTG.WPF.Dashboard.Shared.WPF;

namespace ZTG.WPF.Dashboard.Main.UserInterface
{
  public class FeedsManagerUIService : NotificationObject
  {
    private readonly FeedService _feedService;

    private readonly DelegateCommand _addFeedCommand;
    private readonly DelegateCommand _editFeedCommand;
    private readonly DelegateCommand _deleteFeedCommand;

    public ObservableCollection<Feed> Feeds { get; private set; }

    private Feed _selectedFeed;

    /// <summary>
    /// Gets or sets the SelectedFeed.
    /// </summary>
    /// <value>The SelectedFeed value.</value>
    public Feed SelectedFeed
    {
      get
      {
        return _selectedFeed;
      }

      set
      {
        if (ChangeAndNotify(ref _selectedFeed, value, "SelectedFeed"))
        {
          if (SelectedFeedChanged != null)
          {
            SelectedFeedChanged(this, EventArgs.Empty);
          }

          _editFeedCommand.RaiseCanExecuteChanged();
          _deleteFeedCommand.RaiseCanExecuteChanged();
        }
      }
    }

    public ICommand AddFeedCommand { get { return _addFeedCommand; } }

    public ICommand EditFeedCommand { get { return _editFeedCommand; } }

    public ICommand DeleteFeedCommand { get { return _deleteFeedCommand; } }

    /// <summary>
    /// Initializes a new instance of the <see cref="FeedsManagerUIService"/> class.
    /// </summary>
    public FeedsManagerUIService()
    {
      _feedService = new FeedService();
      Feeds = new ObservableCollection<Feed>(_feedService.Feeds);

      _addFeedCommand = new DelegateCommand(AddFeed, CanAddFeed);
      _editFeedCommand = new DelegateCommand(EditFeed, CanEditFeed);
      _deleteFeedCommand = new DelegateCommand(DeleteFeed, CanDeleteFeed);
    }

    public event EventHandler SelectedFeedChanged;

    private bool CanAddFeed()
    {
      return true;
    }

    private void AddFeed()
    {
    }

    private bool CanEditFeed()
    {
      return SelectedFeed != null;
    }

    private void EditFeed()
    {
    }

    private bool CanDeleteFeed()
    {
      return SelectedFeed != null;
    }

    private void DeleteFeed()
    {
    }
  }
}