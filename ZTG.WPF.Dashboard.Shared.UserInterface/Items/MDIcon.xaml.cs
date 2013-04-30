// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MDIcon.xaml.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Media;

using ZTG.WPF.Dashboard.Shared.UserInterface.Items.Enums;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Items
{
  /// <summary>
  /// Interaction logic for MDIcon.xaml
  /// </summary>
  public partial class MDIcon
  {
    /// <summary>
    /// The IconType dependency property
    /// </summary>
    public static readonly DependencyProperty IconTypeProperty = DependencyProperty.Register(
      "IconType", typeof(IconType), typeof(MDIcon), new PropertyMetadata(IconType.None));

    /// <summary>
    /// Gets or sets the type of the icon. See Controls/Icons.xaml
    /// </summary>
    public IconType IconType
    {
      get
      {
        return (IconType)GetValue(IconTypeProperty);
      }

      set
      {
        SetValue(IconTypeProperty, value);
      }
    }

    /// <summary>
    /// The IconPath dependency property
    /// </summary>
    public static readonly DependencyProperty IconPathProperty = DependencyProperty.Register(
      "IconPath", typeof(Geometry), typeof(MDIcon), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the icon path vector data of this MD icon.
    /// </summary>
    public Geometry IconPath
    {
      get
      {
        return (Geometry)GetValue(IconPathProperty);
      }

      set
      {
        SetValue(IconPathProperty, value);
      }
    }

    /// <summary>
    /// The IconSize dependency property
    /// </summary>
    public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register(
      "IconSize", typeof(Size), typeof(MDIcon), new PropertyMetadata(new Size(32, 32), IconSizeChangedHandler));

    private static void IconSizeChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var icon = d as MDIcon;
      if (icon == null)
      {
        return;
      }

      icon.Width = icon.IconSize.Width;
      icon.Height = icon.IconSize.Height;
    }

    /// <summary>
    /// Gets or sets the size of the icon.
    /// </summary>
    public Size IconSize
    {
      get
      {
        return (Size)GetValue(IconSizeProperty);
      }

      set
      {
        SetValue(IconSizeProperty, value);
      }
    }

    /// <summary>
    /// The IconPadding dependency property
    /// </summary>
    public static readonly DependencyProperty IconPaddingProperty = DependencyProperty.Register(
      "IconPadding", typeof(Thickness), typeof(MDIcon), new PropertyMetadata(new Thickness(4)));

    /// <summary>
    /// Gets or sets the icon padding.
    /// </summary>
    public Thickness IconPadding
    {
      get
      {
        return (Thickness)GetValue(IconPaddingProperty);
      }

      set
      {
        SetValue(IconPaddingProperty, value);
      }
    }

    /// <summary>
    /// The IconBrush dependency property
    /// </summary>
    public static readonly DependencyProperty IconBrushProperty = DependencyProperty.Register(
      "IconBrush", typeof(Brush), typeof(MDIcon), new PropertyMetadata(Brushes.White));

    /// <summary>
    /// Gets or sets the icon brush.
    /// </summary>
    public Brush IconBrush
    {
      get
      {
        return (Brush)GetValue(IconBrushProperty);
      }

      set
      {
        SetValue(IconBrushProperty, value);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MDIcon"/> class.
    /// </summary>
    public MDIcon()
    {
      InitializeComponent();
    }
  }
}
