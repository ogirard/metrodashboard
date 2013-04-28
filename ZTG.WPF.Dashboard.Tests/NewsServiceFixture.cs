// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewsServiceFixture.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using ZTG.WPF.Dashboard.Main.BusinessService;
using ZTG.WPF.Dashboard.Main.Model;

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
      var mockedFeedService = new Mock<FeedService>();
      var feeds = new[] { new Feed(Guid.NewGuid()) { Path = new Uri("http://heise.de.feedsportal.com/c/35207/f/653902/index.rss") } };
      mockedFeedService.Setup(fs => fs.Feeds).Returns(feeds);

      var target = new NewsService(mockedFeedService.Object);

      // Act
      var feed = target.GetAllFeeds();

      // Assert
      Assert.IsNotNull(feed);
    }
  }
}
