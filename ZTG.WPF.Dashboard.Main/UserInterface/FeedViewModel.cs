// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedViewModel.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows.Input;

using ZTG.WPF.Dashboard.Main.Model;
using ZTG.WPF.Dashboard.Shared.WPF;

namespace ZTG.WPF.Dashboard.Main.UserInterface
{
  public class FeedViewModel : NotificationObject
  {
    private Feed _feed;

    /// <summary>
    /// Gets or sets the Feed.
    /// </summary>
    /// <value>The Feed value.</value>
    public Feed Feed
    {
      get
      {
        return _feed;
      }

      set
      {
        ChangeAndNotify(ref _feed, value, "Feed");
      }
    }

    private string _headerText;

    /// <summary>
    /// Gets or sets the HeaderText.
    /// </summary>
    /// <value>The HeaderText value.</value>
    public string HeaderText
    {
      get
      {
        return _headerText;
      }

      set
      {
        ChangeAndNotify(ref _headerText, value, "HeaderText");
      }
    }

    public ICommand CheckFeedCommand { get; set; }

    public ICommand SaveCommand { get; set; }

    public ICommand CancelCommand { get; set; }
  }
}