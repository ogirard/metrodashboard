//----------------------------------------------------------------------------------------------------
// <copyright file="MDLabel.xaml.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System.Windows;

using ZTG.WPF.Dashboard.Shared.UserInterface.Items.Enums;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Items
{
  /// <summary>
  /// Interaction logic for MDLabel.xaml
  /// </summary>
  public partial class MDLabel
  {
    /// <summary>
    /// The LabelType dependency property
    /// </summary>
    public static readonly DependencyProperty LabelTypeProperty = DependencyProperty.Register(
      "LabelType", typeof(LabelType), typeof(MDLabel), new PropertyMetadata(LabelType.Field));

    /// <summary>
    /// Gets or sets the type of the label.
    /// </summary>
    public LabelType LabelType
    {
      get
      {
        return (LabelType)GetValue(LabelTypeProperty);
      }

      set
      {
        SetValue(LabelTypeProperty, value);
      }
    }

    public MDLabel()
    {
      InitializeComponent();
    }
  }
}
