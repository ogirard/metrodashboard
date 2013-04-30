// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedService.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using ZTG.WPF.Dashboard.Main.DataAccess;
using ZTG.WPF.Dashboard.Main.Model;
using ZTG.WPF.Dashboard.Shared.Validation;

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

    public Feed CreateNewFeed()
    {
      return _dataAccess.CreateFeed();
    }

    public void AddFeed(Feed feed)
    {
      _dataAccess.AddFeed(feed);
      _dataAccess.Commit();
    }

    public void EditFeed(Feed feed)
    {
      _dataAccess.Commit();
    }

    public void DeleteFeed(Feed feed)
    {
      _dataAccess.DeleteFeed(feed);
      _dataAccess.Commit();
    }

    public void UndoChanges()
    {
      _dataAccess.Rollback();
    }

    public ValidationErrors ValidateFeed(Feed feed)
    {
      var errors = new ValidationErrors();

      if (string.IsNullOrEmpty(feed.Name))
      {
        errors.AddValidationError(ValidationErrorScope.Property, "Name must not be empty", "Name", ValidationLevel.Error);
      }
      else if (Feeds.Any(f => f != feed && f.Name == feed.Name))
      {
        errors.AddValidationError(ValidationErrorScope.Property, "Name must be unique", "Name", ValidationLevel.Error);
      }

      if (feed.Path == null)
      {
        errors.AddValidationError(ValidationErrorScope.Property, "Path must not be empty", "Path", ValidationLevel.Error);
      }
      else if (!feed.Path.IsAbsoluteUri)
      {
        errors.AddValidationError(ValidationErrorScope.Property, "Feed URL must be valid", "Path", ValidationLevel.Error);
      }
      else if (Feeds.Any(f => f != feed && f.Path.AbsolutePath == feed.Path.AbsolutePath))
      {
        errors.AddValidationError(ValidationErrorScope.Property, "Path must be unique", "Path", ValidationLevel.Error);
      }

      return errors;
    }
  }
}