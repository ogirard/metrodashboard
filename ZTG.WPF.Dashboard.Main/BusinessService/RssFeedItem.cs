// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RssFeedItem.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Main.BusinessService
{
  public class RssFeedItem
  {
    private readonly XElement _feedItemElement;
    private static readonly Regex UrlRegex = new Regex(@"(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*", RegexOptions.Compiled);
    private static readonly IEnumerable<string> SupportedImages = new[] { ".png", ".jpg", ".gif" };

    public RssFeed Feed { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public DateTime PublicationDate { get; private set; }

    public Uri ImagePath { get; private set; }
    
    public Uri Link { get; private set; }

    public RssFeedItem(RssFeed feed, XElement feedItemElement)
    {
      feed.ArgumentNotNull("feed");
      feedItemElement.ArgumentNotNull("feedItemElement");

      Feed = feed;
      _feedItemElement = feedItemElement;
    }

    private Uri FindImageUri()
    {
      foreach (var urlMatch in UrlRegex.Matches(_feedItemElement.ToString()))
      {
        var url = urlMatch.ToString();

        if (!string.IsNullOrEmpty(url) && Uri.IsWellFormedUriString(url, UriKind.Absolute) && SupportedImages.Any(e => url.EndsWith(e, StringComparison.OrdinalIgnoreCase)))
        {
          return new Uri(url);
        }
      }

      return null;
    }
  }
}