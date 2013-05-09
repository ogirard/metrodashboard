// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OptionsUIService.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

using ZTG.WPF.Dashboard.Main.BusinessService;
using ZTG.WPF.Dashboard.Main.Model;
using ZTG.WPF.Dashboard.Shared.UserInterface.Windows;
using ZTG.WPF.Dashboard.Shared.UserInterface.Windows.Enums;
using ZTG.WPF.Dashboard.Shared.Utilities;
using ZTG.WPF.Dashboard.Shared.WPF;

using MessageBoxResult = ZTG.WPF.Dashboard.Shared.UserInterface.Windows.Enums.MessageBoxResult;

namespace ZTG.WPF.Dashboard.Main.UserInterface
{
  public class OptionsUIService : NotificationObject
  {
    private readonly RssNewsService _rssNewsService;
    private readonly FeedService _feedService;

    private readonly DelegateCommand _openOptionsDialogCommand;
    private readonly DelegateCommand _addFeedCommand;
    private readonly DelegateCommand _editFeedCommand;
    private readonly DelegateCommand _deleteFeedCommand;

    public ObservableCollection<Feed> Feeds { get; private set; }

    private Feed _selectedFeed;

    private OptionsDialog _optionsDialog;

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

    public ICommand OpenOptionsDialogCommand { get { return _openOptionsDialogCommand; } }

    public ICommand AddFeedCommand { get { return _addFeedCommand; } }

    public ICommand EditFeedCommand { get { return _editFeedCommand; } }

    public ICommand DeleteFeedCommand { get { return _deleteFeedCommand; } }

    /// <summary>
    /// Initializes a new instance of the <see cref="OptionsUIService" /> class.
    /// </summary>
    /// <param name="feedService">The feed service.</param>
    /// <param name="rssNewsService">The news service.</param>
    public OptionsUIService(FeedService feedService, RssNewsService rssNewsService)
    {
      feedService.ArgumentNotNull("FeedService");
      rssNewsService.ArgumentNotNull("RssNewsService");

      _feedService = feedService;
      _rssNewsService = rssNewsService;

      Feeds = new ObservableCollection<Feed>(_feedService.Feeds);

      _openOptionsDialogCommand = new DelegateCommand(OpenOptionsDialog, CanOpenOptionsDialog);

      _addFeedCommand = new DelegateCommand(AddFeed, CanAddFeed);
      _editFeedCommand = new DelegateCommand(EditFeed, CanEditFeed);
      _deleteFeedCommand = new DelegateCommand(DeleteFeed, CanDeleteFeed);
    }

    public event EventHandler SelectedFeedChanged;

    private bool CanOpenOptionsDialog()
    {
      // options can only be opened once
      return _optionsDialog == null;
    }

    private void OpenOptionsDialog()
    {
      _optionsDialog = new OptionsDialog
                             {
                               ViewModel = new OptionsViewModel(this),
                               Owner = Application.Current.MainWindow
                             };

      _optionsDialog.ShowDialog();
      _optionsDialog = null;
    }

    private bool CanAddFeed()
    {
      return true;
    }

    private void AddFeed()
    {
      var addViewModel = new FeedViewModel { Feed = _feedService.CreateNewFeed(), HeaderText = "Register new RSS Feed" };
      var addDialog = new FeedDialog { Owner = _optionsDialog, ViewModel = addViewModel };
      addViewModel.CheckFeedCommand = new DelegateCommand(() => CheckFeed(addViewModel));
      addViewModel.CancelCommand = new DelegateCommand(addDialog.Close);
      addViewModel.SaveCommand = new DelegateCommand(() => SaveAndAddFeed(addDialog));
      addDialog.ShowDialog();
    }

    private void CheckFeed(FeedViewModel viewModel)
    {
      if (viewModel.Feed.Path == null)
      {
        return;
      }

      try
      {
        var feed = _rssNewsService.LoadFeed(viewModel.Feed.Path);
        viewModel.Feed.Name = feed.Name;
        viewModel.Feed.Description = feed.Description;
      }
      catch
      {
        viewModel.Feed.Name = "No valid RSS feed!";
        viewModel.Feed.Description = string.Empty;
      }
    }

    private void SaveAndAddFeed(FeedDialog addDialog)
    {
      var newFeed = addDialog.ViewModel.Feed;

      // validate
      var validationErrors = _feedService.ValidateFeed(newFeed);
      if (!validationErrors.IsValidForSave)
      {
        // report validation errors!
        return;
      }

      // Add feed
      _feedService.AddFeed(newFeed);

      // Update list and selection
      Feeds.Add(newFeed);
      SelectedFeed = newFeed;

      // Close dialog
      addDialog.Close();
    }

    private bool CanEditFeed()
    {
      return SelectedFeed != null;
    }

    private void EditFeed()
    {
      var editViewModel = new FeedViewModel { Feed = SelectedFeed, HeaderText = "Edit RSS Feed" };
      var editDialog = new FeedDialog { Owner = _optionsDialog, ViewModel = editViewModel };
      editViewModel.CheckFeedCommand = new DelegateCommand(() => CheckFeed(editViewModel));
      editViewModel.CancelCommand = new DelegateCommand(() => CancelEditFeed(editDialog));
      editViewModel.SaveCommand = new DelegateCommand(() => SaveAndEditFeed(editDialog));
      editDialog.ShowDialog();
    }

    private void CancelEditFeed(FeedDialog editDialog)
    {
      _feedService.UndoChanges();
      editDialog.Close();
    }

    private void SaveAndEditFeed(FeedDialog addDialog)
    {
      var editedFeed = addDialog.ViewModel.Feed;

      // validate
      var validationErrors = _feedService.ValidateFeed(editedFeed);
      if (!validationErrors.IsValidForSave)
      {
        // report validation errors!
        return;
      }

      // Edit feed
      _feedService.EditFeed(editedFeed);

      // Close dialog
      addDialog.Close();
    }

    private bool CanDeleteFeed()
    {
      return SelectedFeed != null;
    }

    private void DeleteFeed()
    {
      if (MDMessageBox.ShowDialog(_optionsDialog, MessageBoxType.Confirmation, "Delete", "Should the selected feed be deleted?") == MessageBoxResult.Yes)
      {
        var feed = SelectedFeed;
        SelectedFeed = null;
        _feedService.DeleteFeed(feed);
        Feeds.Remove(feed);
      }
    }
  }
}