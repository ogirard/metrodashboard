// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetroToolButton.xaml.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Windows;
using System.Windows.Media;

using ZTG.WPF.Dashboard.Shared.Extensions;
using ZTG.WPF.Dashboard.Shared.UserInterface.VisualResources;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Item
{
  /// <summary>
  /// Interaction logic for MetroToolButton.xaml
  /// </summary>
  public partial class MetroToolButton
  {
    private static readonly Thickness ZeroPadding = new Thickness(0);
    private static readonly Brush AccentBrush = VisualResources.Resources.DefaultResources.GetResource<Brush>("AccentBrush");
    private static readonly Brush DefaultSimpleIconBrush = VisualResources.Resources.DefaultResources.GetResource<Brush>("DefaultSimpleIconBrush");
    private Thickness _defaultPadding;

    /// <summary>
    /// The IconType dependency property
    /// </summary>
    public static readonly DependencyProperty IconTypeProperty = DependencyProperty.Register("IconType", typeof(MetroIcon.IconType), typeof(MetroToolButton), new PropertyMetadata(MetroIcon.IconType.NoIcon, IconChangedHandler));

    /// <summary>
    /// Gets or sets the type of the icon.
    /// </summary>
    public MetroIcon.IconType IconType
    {
      get
      {
        return (MetroIcon.IconType)GetValue(IconTypeProperty);
      }

      set
      {
        SetValue(IconTypeProperty, value);
      }
    }

    /// <summary>
    /// The IsHighlighted dependency property
    /// </summary>
    public static readonly DependencyProperty IsHighlightedProperty = DependencyProperty.Register("IsHighlighted", typeof(bool), typeof(MetroToolButton), new PropertyMetadata(false, IconChangedHandler));

    /// <summary>
    /// Gets or sets a value indicating whether this instance is highlighted.
    /// </summary>
    public bool IsHighlighted
    {
      get
      {
        return (bool)GetValue(IsHighlightedProperty);
      }

      set
      {
        SetValue(IsHighlightedProperty, value);
      }
    }

    private static void IconChangedHandler(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
    {
      var toolButton = dependencyObject as MetroToolButton;
      if (toolButton != null)
      {
        toolButton.UpdateIcon();
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MetroToolButton"/> class.
    /// </summary>
    public MetroToolButton()
    {
      InitializeComponent();

      MinWidth = Constants.DefaultButtonHeight;
      MinHeight = Constants.DefaultButtonHeight;
      Height = Constants.DefaultButtonHeight;
      Width = Constants.DefaultButtonHeight;

      UpdateIcon();
    }

    protected override void OnContentChanged(object oldContent, object newContent)
    {
      base.OnContentChanged(oldContent, newContent);
      UpdateIcon();
    }

    private void UpdateIcon()
    {
      var icon = Content as MetroIcon;
      if (icon == null)
      {
        if (Padding == ZeroPadding)
        {
          Padding = _defaultPadding;
        }

        return;
      }

      if (Padding != ZeroPadding)
      {
        _defaultPadding = Padding;
        Padding = ZeroPadding;
      }

      if (IconType == MetroIcon.IconType.NoIcon)
      {
        icon.Visibility = Visibility.Hidden;
      }
      else
      {
        icon.Type = IconType;
        icon.Visibility = Visibility.Visible;
        icon.IconBrush = IsHighlighted ? AccentBrush : DefaultSimpleIconBrush;
      }
    }
  }
}
