// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewsServiceFixture.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ZTG.WPF.Dashboard.Main;

namespace ZTG.WPF.Dashboard.Tests
{
  /// <summary>
  /// Tests for <see cref="NewsService"/>
  /// </summary>
  [TestClass]
  public class NewsServiceFixture
  {
    [TestMethod]
    public void LoadFeedReturnsFeed()
    {
      // Arrange
      var target = new NewsService();
      var feedUri = new Uri("http://heise.de.feedsportal.com/c/35207/f/653902/index.rss");

      // Act
      var feed = target.LoadFeed(feedUri);

      // Assert
      Assert.IsNotNull(feed);
    }
  }
}
