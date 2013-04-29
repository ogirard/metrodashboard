// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedDialog.xaml.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ZTG.WPF.Dashboard.Main.UserInterface
{
  /// <summary>
  /// Interaction logic for FeedDialog.xaml
  /// </summary>
  public partial class FeedDialog
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="FeedDialog"/> class.
    /// </summary>
    public FeedDialog()
    {
      InitializeComponent();
    }

    public FeedViewModel ViewModel
    {
      get
      {
        return DataContext as FeedViewModel;
      }

      set
      {
        DataContext = value;
      }
    }
  }
}
