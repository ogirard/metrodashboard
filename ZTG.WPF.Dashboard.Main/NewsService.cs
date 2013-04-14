// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewsService.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using Argotic.Syndication;

using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Main
{
  public class NewsService
  {
    private readonly IList<Uri> _registeredFeeds = new List<Uri>
                                                     {
                                                       new Uri("http://heise.de.feedsportal.com/c/35207/f/653902/index.rss"),
                                                       new Uri("http://de.engadget.com/rss.xml"),
                                                       new Uri("http://www.microsoft.com/germany/msdn/rss/aktuell.xml")
                                                     };

    public NewsService()
    {
    }

    public RssFeed LoadFeed(Uri feedUri)
    {
      feedUri.ArgumentNotNull("feedUri");
      var feed = RssFeed.Create(feedUri);
      return feed;
    }

    public IEnumerable<RssFeed> GetAllFeeds()
    {
      return _registeredFeeds.Select(LoadFeed);
    }
  }
}