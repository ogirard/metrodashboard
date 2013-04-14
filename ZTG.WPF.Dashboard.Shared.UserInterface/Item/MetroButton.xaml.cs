// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetroButton.xaml.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ZTG.WPF.Dashboard.Shared.UserInterface.VisualResources;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Item
{
  /// <summary>
  /// Interaction logic for MetroButton.xaml
  /// </summary>
  public partial class MetroButton
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MetroButton"/> class.
    /// </summary>
    public MetroButton()
    {
      InitializeComponent();

      MinWidth = Constants.DefaultButtonWidth;
      MinHeight = Constants.DefaultButtonHeight;
      Height = Constants.DefaultButtonHeight;
    }
  }
}
