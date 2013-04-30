// //----------------------------------------------------------------------------------------------------
// // <copyright file="MDChromeButton.cs" company="Zühlke Engineering AG">
// //     Copyright (c) Zühlke Engineering AG. All rights reserved.
// // </copyright>
// //----------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

using ZTG.WPF.Dashboard.Shared.UserInterface.Windows.Enums;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Windows
{
  public class MDChromeButton : Button
  {
    /// <summary>
    /// The Type dependency property
    /// </summary>
    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(ChromeButtonType), typeof(MDChromeButton), new PropertyMetadata(ChromeButtonType.Close));

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "OK")]
    public ChromeButtonType Type
    {
      get
      {
        return (ChromeButtonType)GetValue(TypeProperty);
      }

      set
      {
        SetValue(TypeProperty, value);
      }
    }

    public MDChromeButton()
    {
      // apply default style (including control template)
      Style = Application.Current.FindResource("MDChromeButtonDefaultStyle") as Style;
    }
  }
}