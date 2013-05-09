// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RssNewsService.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Xml.Linq;

using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Main.BusinessService
{
  public class RssNewsService
  {
    /// <summary>
    /// Loads the feed published at the given URI.
    /// </summary>
    /// <param name="feedPath">The feed path.</param>
    /// <returns></returns>
    /// <exception cref="System.Exception">Thrown if feed could not be loaded properly</exception>
    public RssFeed LoadFeed(Uri feedPath)
    {
      feedPath.ArgumentNotNull("feedPath");

      try
      {
        var request = WebRequest.Create(feedPath);
        using (var reader = new StreamReader(request.GetResponse().GetResponseStream()))
        {
          var feedXml = XDocument.Parse(reader.ReadToEnd());
          return new RssFeed(feedPath, feedXml.Root);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Could not load RssFeed " + feedPath, ex);
      }
    }

    /// <summary>
    /// Loads the all the feeds (<see cref="LoadFeed"/>).
    /// </summary>
    /// <param name="feedUris">The feed uris.</param>
    /// <returns></returns>
    public IEnumerable<RssFeed> LoadFeeds(IEnumerable<Uri> feedUris)
    {
      // TODO: async delay for testing
      Thread.Sleep(2000);

      return feedUris.Select(LoadFeed);
    }
  }
}