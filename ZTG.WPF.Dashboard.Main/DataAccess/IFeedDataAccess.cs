// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFeedDataAccess.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using ZTG.WPF.Dashboard.Main.Model;

namespace ZTG.WPF.Dashboard.Main.DataAccess
{
  public interface IFeedDataAccess
  {
    IEnumerable<Feed> GetAllFeeds();

    Feed GetFeed(Guid id);

    Feed CreateFeed();

    void AddFeed(Feed feed);

    void DeleteFeed(Feed feed);

    void Commit();

    void Rollback();
  }
}