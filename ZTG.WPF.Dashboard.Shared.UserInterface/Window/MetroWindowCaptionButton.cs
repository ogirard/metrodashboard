// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetroWindowCaptionButton.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

using ZTG.WPF.Dashboard.Shared.Extensions;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Window
{
  public class MetroWindowCaptionButton : Button
  {
    /// <summary>
    /// The Type dependency property
    /// </summary>
    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(ButtonType), typeof(MetroWindowCaptionButton), new PropertyMetadata(ButtonType.Close));

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "OK")]
    public ButtonType Type
    {
      get
      {
        return (ButtonType)GetValue(TypeProperty);
      }

      set
      {
        SetValue(TypeProperty, value);
      }
    }

    public MetroWindowCaptionButton()
    {
      // apply default style (including control template)
      Style = VisualResources.Resources.DefaultResources.GetResource<Style>("DefaultMSWindowCaptionButtonStyle");
    }
  }
}