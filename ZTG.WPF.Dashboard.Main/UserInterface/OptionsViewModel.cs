// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OptionsViewModel.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.ObjectModel;
using System.Windows.Input;

using ZTG.WPF.Dashboard.Main.Model;
using ZTG.WPF.Dashboard.Shared.Utilities;
using ZTG.WPF.Dashboard.Shared.WPF;

namespace ZTG.WPF.Dashboard.Main.UserInterface
{
  public class OptionsViewModel : NotificationObject
  {
    private readonly OptionsUIService _uiService;

    public ObservableCollection<Feed> Feeds
    {
      get { return _uiService.Feeds; }
    }

    public Feed SelectedFeed
    {
      get
      {
        return _uiService.SelectedFeed;
      }

      set
      {
        _uiService.SelectedFeed = value;
      }
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

    public OptionsViewModel(OptionsUIService uiService)
    {
      uiService.ArgumentNotNull("uiService");
      _uiService = uiService;
    }
  }
}