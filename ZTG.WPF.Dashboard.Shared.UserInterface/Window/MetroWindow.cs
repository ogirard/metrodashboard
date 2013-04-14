// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetroWindow.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows.Media;

using ZTG.WPF.Dashboard.Shared.Extensions;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Window
{
  /// <summary>
  /// Management Suite Main Window
  /// </summary>
  public class MetroWindow : MetroWindowBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MetroWindow"/> class.
    /// </summary>
    public MetroWindow()
    {
      ChromeBackground = Resources.GetResource<Brush>("DarkAccentBrush");
      ChromeForeground = Resources.GetResource<Brush>("WhiteBrush");
    }
  }
}