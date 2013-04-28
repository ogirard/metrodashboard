// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedsManagerUIService.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows.Input;

using ZTG.WPF.Dashboard.Main.BusinessService;
using ZTG.WPF.Dashboard.Main.Model;
using ZTG.WPF.Dashboard.Shared.WPF;

namespace ZTG.WPF.Dashboard.Main.UserInterface
{
  public class FeedsManagerUIService : NotificationObject
  {
    private readonly FeedService _feedService;

    public IEnumerable<Feed> Feeds { get { return _feedService.Feeds; } }

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
        }
      }
    }

    public ICommand AddFeedCommand { get; private set; }

    public ICommand EditFeedCommand { get; private set; }

    public ICommand DeleteFeedCommand { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FeedsManagerUIService"/> class.
    /// </summary>
    public FeedsManagerUIService()
    {
      _feedService = new FeedService();
    }

    public event EventHandler SelectedFeedChanged;

    private void AddFeed()
    {
    }

    private void EditFeed()
    {
    }

    private void DeleteFeed()
    {
    }
  }
}