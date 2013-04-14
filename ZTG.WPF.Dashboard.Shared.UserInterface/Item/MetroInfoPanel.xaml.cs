// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetroInfoPanel.xaml.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Controls;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Item
{
  /// <summary>
  /// Interaction logic for MetroInfoPanel.xaml
  /// </summary>
  public partial class MetroInfoPanel : UserControl
  {
    private static readonly Thickness ShowBordersThickness = new Thickness(0, 1, 0, 1);
    private static readonly Thickness ShowBordersPadding = new Thickness(10, 4, 10, 4);
    private static readonly Thickness NoBordersThickness = new Thickness(0);
    private static readonly Thickness NoBordersPadding = new Thickness(0);

    /// <summary>
    /// The IconType dependency property
    /// </summary>
    public static readonly DependencyProperty IconTypeProperty = DependencyProperty.Register("IconType", typeof(MetroStatusIcon.StatusIconType), typeof(MetroInfoPanel), new PropertyMetadata(MetroStatusIcon.StatusIconType.Information));

    /// <summary>
    /// Gets or sets the type of the icon.
    /// </summary>
    public MetroStatusIcon.StatusIconType IconType
    {
      get
      {
        return (MetroStatusIcon.StatusIconType)GetValue(IconTypeProperty);
      }

      set
      {
        SetValue(IconTypeProperty, value);
      }
    }

    /// <summary>
    /// The InfoText dependency property
    /// </summary>
    public static readonly DependencyProperty InfoTextProperty = DependencyProperty.Register("InfoText", typeof(string), typeof(MetroInfoPanel), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the info text.
    /// </summary>
    public string InfoText
    {
      get
      {
        return (string)GetValue(InfoTextProperty);
      }

      set
      {
        SetValue(InfoTextProperty, value);
      }
    }

    /// <summary>
    /// The ShowLines dependency property
    /// </summary>
    public static readonly DependencyProperty ShowLinesProperty = DependencyProperty.Register("ShowLines", typeof(bool), typeof(MetroInfoPanel), new PropertyMetadata(true, OnShowLinesChangedHandler));

    /// <summary>
    /// Gets or sets a value indicating whether the top and bottom line should be shown.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the top and bottom line should be shown; otherwise, <c>false</c>.
    /// </value>
    public bool ShowLines
    {
      get
      {
        return (bool)GetValue(ShowLinesProperty);
      }

      set
      {
        SetValue(ShowLinesProperty, value);
      }
    }

    private static void OnShowLinesChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var infoPanel = d as MetroInfoPanel;
      if (infoPanel == null)
      {
        return;
      }

      infoPanel.OnShowLinesChanged();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MetroInfoPanel"/> class.
    /// </summary>
    public MetroInfoPanel()
    {
      InitializeComponent();
    }

    private void OnShowLinesChanged()
    {
      _border.BorderThickness = ShowLines ? ShowBordersThickness : NoBordersThickness;
      _border.Padding = ShowLines ? ShowBordersPadding : NoBordersPadding;
    }
  }
}
