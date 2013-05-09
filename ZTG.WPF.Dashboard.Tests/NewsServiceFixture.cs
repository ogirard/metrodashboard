// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewsServiceFixture.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ZTG.WPF.Dashboard.Main.BusinessService;

namespace ZTG.WPF.Dashboard.Tests
{
  /// <summary>
  /// Tests for <see cref="RssNewsService"/>
  /// </summary>
  [TestClass]
  public class NewsServiceFixture
  {
    [TestMethod]
    public void LoadFeedsReturnsFeeds()
    {
      // Arrange
      var feeds = new[] { new Uri("http://www.20min.ch/rss/rss.tmpl?type=channel&get=4") };
      var target = new RssNewsService();

      // Act
      var feed = target.LoadFeeds(feeds);

      // Assert
      Assert.IsNotNull(feed);
    }
  }
}
