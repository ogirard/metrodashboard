// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedsManagerDialog.xaml.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ZTG.WPF.Dashboard.Main.UserInterface
{
  /// <summary>
  /// Interaction logic for FeedsManagerDialog.xaml
  /// </summary>
  public partial class FeedsManagerDialog
  {
    public FeedsManagerDialog()
    {
      InitializeComponent();
    }

    public FeedsManagerViewModel ViewModel
    {
      get
      {
        return DataContext as FeedsManagerViewModel;
      }

      set
      {
        DataContext = value;
      }
    }
  }
}
