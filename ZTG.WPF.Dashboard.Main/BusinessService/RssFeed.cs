// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RssFeed.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using ZTG.WPF.Dashboard.Shared.Extensions;
using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Main.BusinessService
{
  public class RssFeed
  {
    private readonly XElement _feedElement;

    public Uri FeedPath { get; private set; }

    public string Title { get; private set; }

    public Uri Link { get; private set; }

    public string Description { get; private set; }

    public Uri ImagePath { get; private set; }

    public IEnumerable<RssFeedItem> Items { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RssFeed" /> class.
    /// </summary>
    /// <param name="feedPath">The feed path.</param>
    /// <param name="feedElement">The path of this feed</param>
    public RssFeed(Uri feedPath, XElement feedElement)
    {
      feedPath.ArgumentNotNull("feedPath");
      feedElement.ArgumentNotNull("feedElement");

      FeedPath = feedPath;
      _feedElement = feedElement;

      InitializeFeed();
    }

    private void InitializeFeed()
    {
      var channel = _feedElement.GetElement("channel");
      Title = channel.GetElementValue("title");
      Link = new Uri(channel.GetElementValue("link"));
      Description = channel.GetElementValue("description");

      if (channel.HasElement("image"))
      {
        var image = channel.GetElement("image");
        if (image.HasElement("url"))
        {
          ImagePath = new Uri(image.GetElementValue("url"));
        }
      }

      Items = channel.Elements("item").Select(i => new RssFeedItem(this, i)).ToList();
    }
  }
}