// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetroDialogWindow.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows.Media;

using ZTG.WPF.Dashboard.Shared.Extensions;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Window
{
  /// <summary>
  /// Management Suite Dialog Window
  /// </summary>
  public class MetroDialogWindow : MetroWindowBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MetroDialogWindow"/> class.
    /// </summary>
    public MetroDialogWindow()
    {
      // set defaults
      ShowInTaskbar = false;
      ChromeBackground = Resources.GetResource<Brush>("DarkAccentBrush");
      ChromeForeground = Resources.GetResource<Brush>("WhiteBrush");
    }
  }
}