// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedsManagerViewModel.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.ObjectModel;
using System.Windows.Input;

using ZTG.WPF.Dashboard.Main.Model;
using ZTG.WPF.Dashboard.Shared.WPF;

namespace ZTG.WPF.Dashboard.Main.UserInterface
{
  public class FeedsManagerViewModel : NotificationObject
  {
    private readonly FeedsManagerUIService _uiService;

    public ObservableCollection<Feed> Feeds
    {
      get { return _uiService.Feeds; }
    }

    public ICommand AddFeedCommand
    {
      get { return _uiService.AddFeedCommand; }
    }

    public ICommand EditFeedCommand
    {
      get { return _uiService.EditFeedCommand; }
    }

    public ICommand DeleteFeedCommand
    {
      get { return _uiService.DeleteFeedCommand; }
    }

    public FeedsManagerViewModel()
    {
      _uiService = new FeedsManagerUIService();
    }
  }
}