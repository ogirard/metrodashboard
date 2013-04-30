// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedDataAccess.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;

using ZTG.WPF.Dashboard.Main.Model;
using ZTG.WPF.Dashboard.Shared.Data;
using ZTG.WPF.Dashboard.Shared.Extensions;

namespace ZTG.WPF.Dashboard.Main.DataAccess
{
  public class FeedDataAccess : IFeedDataAccess
  {
    private static readonly string XmlStorageFile = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location) + @"\Data\feeds.xml";

    private readonly IDictionary<Guid, Feed> _feeds = new Dictionary<Guid, Feed>();

    public FeedDataAccess()
    {
      LoadFeeds();
    }

    public IEnumerable<Feed> GetAllFeeds()
    {
      return _feeds.Values.ToList();
    }

    public Feed GetFeed(Guid id)
    {
      return _feeds.ContainsKey(id) ? _feeds[id] : null;
    }

    public Feed CreateFeed()
    {
      return new Feed(Guid.NewGuid());
    }

    public void AddFeed(Feed feed)
    {
      if (_feeds.ContainsKey(feed.Id))
      {
        throw new ArgumentException(@"Feed already exists!", "feed");
      }

      _feeds.Add(feed.Id, feed);
    }

    public void DeleteFeed(Feed feed)
    {
      if (!_feeds.ContainsKey(feed.Id))
      {
        throw new ArgumentException(@"Feed does not exist!", "feed");
      }

      _feeds.Remove(feed.Id);
    }

    public void Commit()
    {
      SaveFeeds();
    }

    public void Rollback()
    {
      LoadFeeds();
    }

    private void LoadFeeds()
    {
      var feedsXml = XDocument.Load(XmlStorageFile);
      foreach (var feedElement in feedsXml.XPathSelectElements("//feed"))
      {
        var id = feedElement.GetElementGuidValue("id");
        var feed = _feeds.ContainsKey(id) ? _feeds[id] : new Feed(id);
        feed.Name = feedElement.GetElementValue("name");
        feed.Description = feedElement.GetElementValue("description");
        feed.Path = new Uri(feedElement.GetElementValue("path"));
        feed.Tags = feedElement.GetElementValue("tags");

        if (!_feeds.ContainsKey(id))
        {
          _feeds.Add(feed.Id, feed);
        }
      }
    }

    private void SaveFeeds()
    {
      var feedsXml = XDocument.Load(XmlStorageFile);
      feedsXml.Root.RemoveNodes();

      foreach (var feed in _feeds.Values)
      {
        var feedElement = new XElement("feed");
        feedElement.SetElementValue("id", DefaultStringConverter.FormatGuid(feed.Id));
        feedElement.Add(new XElement("name", feed.Name));
        feedElement.Add(new XElement("path", feed.Path.AbsoluteUri));
        feedElement.Add(new XElement("description", feed.Description ?? string.Empty));
        feedElement.Add(new XElement("tags", feed.Tags ?? string.Empty));
        feedsXml.Root.Add(feedElement);
      }

      feedsXml.Save(XmlStorageFile);
    }
  }
}