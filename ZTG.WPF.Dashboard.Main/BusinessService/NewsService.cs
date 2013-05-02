// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewsService.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Argotic.Syndication;

using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Main.BusinessService
{
  public class NewsService
  {
    #region    Test

    private readonly IList<Uri> _registeredFeeds = new List<Uri>
                                                     {
                                                       new Uri("http://heise.de.feedsportal.com/c/35207/f/653902/index.rss"),
                                                       new Uri("http://de.engadget.com/rss.xml"),
                                                       new Uri("http://www.microsoft.com/germany/msdn/rss/aktuell.xml")
                                                     };

    #endregion Test

    private readonly FeedService _feedService;

    public NewsService(FeedService feedService)
    {
      feedService.ArgumentNotNull("feedService");
      _feedService = feedService;
    }

    public RssFeed LoadFeed(Uri feedUri)
    {
      feedUri.ArgumentNotNull("feedUri");
      var feed = RssFeed.Create(feedUri);
      return feed;
    }

    public IEnumerable<RssFeed> GetAllFeeds()
    {
      Thread.Sleep(2000);
      return _feedService.Feeds.Select(f => LoadFeed(f.Path));

      // return _registeredFeeds.Select(LoadFeed);
    }
  }
}