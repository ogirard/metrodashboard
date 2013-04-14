// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetroIcon.xaml.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

using ZTG.WPF.Dashboard.Shared.Data;
using ZTG.WPF.Dashboard.Shared.Extensions;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Item
{
  /// <summary>
  /// Interaction logic for MetroIcon.xaml
  /// </summary>
  public partial class MetroIcon
  {
    #region    Nested Type : IconType

    /// <summary>
    /// Enum to identify the shape to be displayed (see TCG.MS.UserInterface.Controls.VisualResources\Defaults.Shapes.xaml). The resource has to be named as $enumValue$Shape24.
    /// </summary>
    public enum IconType
    {
      /// <summary />
      Cut,

      /// <summary />
      Copy,

      /// <summary />
      Paste,

      /// <summary />
      Edit,

      /// <summary />
      Save,

      /// <summary />
      Definition,

      /// <summary />
      Management,

      /// <summary />
      Tools,

      /// <summary />
      NoIcon,

      /// <summary />
      ArrowLeft,

      /// <summary />
      ArrowLeftAll,

      /// <summary />
      ArrowRight,

      /// <summary />
      Number,

      /// <summary />
      DateTime,

      /// <summary />
      Clear,

      /// <summary />
      Filter,

      /// <summary />
      Search,

      /// <summary />
      Delete,

      /// <summary />
      Tree,

      /// <summary />
      List,

      /// <summary />
      HelpDoc,

      /// <summary />
      Print,

      /// <summary />
      PageUp,

      /// <summary />
      PageDown,

      /// <summary />
      ZoomIn,

      /// <summary />
      ZoomOut,

      /// <summary />
      FindPrevious,

      /// <summary />
      FindNext,

      /// <summary />
      Find,

      /// <summary />
      User
    }

    #endregion Nested Type : IconType

    private static readonly Brush DefaultSimpleIconBrush = VisualResources.Resources.DefaultResources.GetResource<Brush>("DefaultSimpleIconBrush");

    public static readonly DependencyProperty IconBrushProperty = DependencyProperty.Register("IconBrush", typeof(Brush), typeof(MetroIcon), new PropertyMetadata(DefaultSimpleIconBrush, IconChangedHandler));

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

    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(IconType), typeof(MetroIcon), new PropertyMetadata(IconType.NoIcon, IconChangedHandler));

    public IconType Type
    {
      get
      {
        return (IconType)GetValue(TypeProperty);
      }

      set
      {
        SetValue(TypeProperty, value);
      }
    }

    /// <summary>
    /// The IconSize dependency property
    /// </summary>
    public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register("IconSize", typeof(double), typeof(MetroIcon), new PropertyMetadata(24d, IconSizeChangedHandler));

    private static void IconSizeChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var icon = d as MetroIcon;
      if (icon != null)
      {
        icon.Height = icon.IconSize;
        icon.Width = icon.IconSize;
      }
    }

    /// <summary>
    /// Gets or sets the size of the icon.
    /// </summary>
    /// <value>
    /// The size of the icon.
    /// </value>
    public double IconSize
    {
      get
      {
        return (double)GetValue(IconSizeProperty);
      }

      set
      {
        SetValue(IconSizeProperty, value);
      }
    }

    private static void IconChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var icon = d as MetroIcon;
      if (icon != null)
      {
        icon.UpdateIcon();
      }
    }

    public static readonly DependencyProperty IconBackgroundProperty = DependencyProperty.Register("IconBackground", typeof(Brush), typeof(MetroIcon), new PropertyMetadata(Brushes.Transparent));

    public Brush IconBackground
    {
      get
      {
        return (Brush)GetValue(IconBackgroundProperty);
      }

      set
      {
        SetValue(IconBackgroundProperty, value);
      }
    }

    public static readonly DependencyProperty IconCornerRadiusProperty = DependencyProperty.Register("IconCornerRadius", typeof(CornerRadius), typeof(MetroIcon), new PropertyMetadata(new CornerRadius(0)));

    public CornerRadius IconCornerRadius
    {
      get
      {
        return (CornerRadius)GetValue(IconCornerRadiusProperty);
      }

      set
      {
        SetValue(IconCornerRadiusProperty, value);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MetroIcon"/> class.
    /// </summary>
    public MetroIcon()
    {
      InitializeComponent();
      Background = Brushes.Transparent;
    }

    private void UpdateIcon()
    {
      if (Type == IconType.NoIcon)
      {
        _viewBox.Visibility = Visibility.Collapsed;
      }
      else
      {
        _viewBox.Visibility = Visibility.Visible;
        var shapeName = DefaultStringConverter.FormatEnum(Type).ToLowerInvariant() + "Shape24";
        var shapeTemplate = VisualResources.Resources.DefaultResources.GetResource<Path>(shapeName);

        // copy shape (re-use on UI not possible with more than one parent at the same time)
        var shape = new Path
          {
            Width = shapeTemplate.Width,
            Height = shapeTemplate.Height,
            Fill = IconBrush,
            Data = shapeTemplate.Data,
            Margin = shapeTemplate.Margin,
            Stretch = shapeTemplate.Stretch,
            RenderTransform = shapeTemplate.RenderTransform,
            RenderTransformOrigin = shapeTemplate.RenderTransformOrigin
          };

        _viewBox.Child = shape;
      }
    }
  }
}
