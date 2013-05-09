// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RssFeed.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Xml.Linq;

using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Main.BusinessService
{
  public class RssFeed
  {
    private readonly XElement _feedElement;

    public string Name { get; private set; }

    public Uri ImagePath { get; private set; }

    public string Description { get; private set; }

    public Uri FeedPath { get; private set; }

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
    }
  }
}