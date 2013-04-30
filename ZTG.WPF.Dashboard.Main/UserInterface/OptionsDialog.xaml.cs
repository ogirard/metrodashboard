// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OptionsDialog.xaml.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows;

namespace ZTG.WPF.Dashboard.Main.UserInterface
{
  /// <summary>
  /// Interaction logic for OptionsDialog.xaml
  /// </summary>
  public partial class OptionsDialog
  {
    public OptionsDialog()
    {
      InitializeComponent();
    }

    public OptionsViewModel ViewModel
    {
      get
      {
        return DataContext as OptionsViewModel;
      }

      set
      {
        DataContext = value;
      }
    }

    private void CloseClickHandler(object sender, RoutedEventArgs e)
    {
      Close();
    }
  }
}
