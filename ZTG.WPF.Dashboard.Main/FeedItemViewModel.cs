// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedItemViewModel.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media;

using Argotic.Syndication;

using ZTG.WPF.Dashboard.Shared.Utilities;
using ZTG.WPF.Dashboard.Shared.WPF;

namespace ZTG.WPF.Dashboard.Main
{
  public class FeedItemViewModel : NotificationObject
  {
    private readonly RssFeed _feed;

    private readonly RssItem _feedItem;

    public string Title
    {
      get
      {
        return _feedItem.Title;
      }
    }

    public DateTime PublicationDate
    {
      get
      {
        return _feedItem.PublicationDate;
      }
    }

    public string Description
    {
      get
      {
        return _feedItem.Description;
      }
    }

    public string Information
    {
      get
      {
        return _feed.Channel.Title + " - " + _feedItem.PublicationDate.ToString("dd.MM.yyyy HH:mm");
      }
    }

    public ImageSource Image
    {
      get
      {
        return null;
      }
    }

    public ICommand OpenLinkCommand { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FeedItemViewModel" /> class.
    /// </summary>
    /// <param name="feed">The feed.</param>
    /// <param name="feedItem">The feed item.</param>
    public FeedItemViewModel(RssFeed feed, RssItem feedItem)
    {
      feed.ArgumentNotNull("feed");
      feedItem.ArgumentNotNull("feedItem");

      _feed = feed;
      _feedItem = feedItem;

      OpenLinkCommand = new DelegateCommand(OpenLink, () => _feedItem.Link != null);
    }

    private void OpenLink()
    {
      try
      {
        Process.Start(_feedItem.Link.AbsoluteUri);
      }
      catch
      {
      }
    }
  }
}