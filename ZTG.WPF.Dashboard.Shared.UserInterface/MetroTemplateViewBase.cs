// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetroTemplateViewBase.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Windows.Controls;
using System.Windows.Media;

using ZTG.WPF.Dashboard.Shared.Extensions;

namespace ZTG.WPF.Dashboard.Shared.UserInterface
{
  /// <summary>
  /// User Control providing access to resources of TCG.MS.UserInterface.Controls
  /// </summary>
  public class MetroTemplateViewBase : UserControl
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MetroTemplateViewBase"/> class.
    /// </summary>
    public MetroTemplateViewBase()
    {
      // load default resources of TCG.MS.UserControl.Controls
      Resources.MergedDictionaries.Add(VisualResources.Resources.DefaultResources);

      // set defaults
      FontFamily = Resources.GetResource<FontFamily>("DefaultFontFamily");
      FontSize = Resources.GetResource<double>("DefaultFontSize");
    }
  }
}