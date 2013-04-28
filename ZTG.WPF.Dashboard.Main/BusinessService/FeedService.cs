// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedService.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using ZTG.WPF.Dashboard.Main.DataAccess;
using ZTG.WPF.Dashboard.Main.Model;

namespace ZTG.WPF.Dashboard.Main.BusinessService
{
  public class FeedService
  {
    private readonly IFeedDataAccess _dataAccess;

    /// <summary>
    /// Initializes a new instance of the <see cref="FeedService"/> class.
    /// </summary>
    public FeedService()
    {
      _dataAccess = new FeedDataAccess();
    }

    public IEnumerable<Feed> Feeds
    {
      get
      {
        return _dataAccess.GetAllFeeds();
      }
    }
  }
}