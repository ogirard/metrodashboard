// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedItemViewModel.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using ZTG.WPF.Dashboard.Main.BusinessService;
using ZTG.WPF.Dashboard.Shared.Utilities;
using ZTG.WPF.Dashboard.Shared.WPF;

namespace ZTG.WPF.Dashboard.Main
{
  public class FeedItemViewModel : NotificationObject
  {
    private readonly RssFeedItem _feedItem;

    public string Title
    {
      get
      {
        return _feedItem.Title;
      }
    }

    public DateTime PublicationDate
    {
      get
      {
        return _feedItem.PublicationDate;
      }
    }

    public string Description
    {
      get
      {
        return _feedItem.Description;
      }
    }

    public string Information
    {
      get
      {
        return _feedItem.Feed.Name + " - " + _feedItem.PublicationDate.ToString("dd.MM.yyyy HH:mm");
      }
    }

    private BitmapImage _image;

    public BitmapImage Image
    {
      get
      {
        if (_image == null && _feedItem.ImagePath != null)
        {
          _image = new BitmapImage(_feedItem.ImagePath);
        }

        return _image;
      }
    }

    public ICommand OpenLinkCommand { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FeedItemViewModel" /> class.
    /// </summary>
    /// <param name="feedItem">The feed item.</param>
    public FeedItemViewModel(RssFeedItem feedItem)
    {
      feedItem.ArgumentNotNull("feedItem");

      _feedItem = feedItem;

      OpenLinkCommand = new DelegateCommand(OpenLink, () => _feedItem.Link != null);
    }

    private void OpenLink()
    {
      try
      {
        Process.Start(_feedItem.Link.AbsoluteUri);
      }
      catch
      {
      }
    }
  }
}