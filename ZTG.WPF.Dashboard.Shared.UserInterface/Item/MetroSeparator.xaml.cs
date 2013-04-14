// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetroSeparator.xaml.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Item
{
  /// <summary>
  /// Interaction logic for MetroSeparator.xaml
  /// </summary>
  public partial class MetroSeparator
  {
    /// <summary>
    /// The SectionHeaderText dependency property
    /// </summary>
    public static readonly DependencyProperty SectionHeaderTextProperty = DependencyProperty.Register("SectionHeaderText", typeof(string), typeof(MetroSeparator), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the section header text.
    /// </summary>
    public string SectionHeaderText
    {
      get
      {
        return (string)GetValue(SectionHeaderTextProperty);
      }

      set
      {
        SetValue(SectionHeaderTextProperty, value);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MetroSeparator"/> class.
    /// </summary>
    public MetroSeparator()
    {
      InitializeComponent();
    }
  }
}
