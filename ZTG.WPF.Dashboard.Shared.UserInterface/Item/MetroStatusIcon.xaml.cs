// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetroStatusIcon.xaml.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

using ZTG.WPF.Dashboard.Shared.Data;
using ZTG.WPF.Dashboard.Shared.Extensions;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Item
{
  /// <summary>
  /// Interaction logic for MSIcon.xaml
  /// </summary>
  public partial class MetroStatusIcon : UserControl
  {
    #region    Nested Type : StatusIconType

    /// <summary>
    /// Enum to identify the shape to be displayed (see TCG.MS.UserInterface.Controls.VisualResources\Defaults\StatusIcons.xaml). The resource has to be named as status$enumValue$.
    /// </summary>
    public enum StatusIconType
    {
      /// <summary />
      Ok,

      /// <summary />
      Warning,

      /// <summary />
      Question,

      /// <summary />
      Information,

      /// <summary />
      Error,

      /// <summary />
      Critical,

      /// <summary />
      Caution,

      /// <summary />
      UpdateRequired,

      /// <summary />
      NoIcon
    }

    #endregion Nested Type : StatusIconType

    #region    Nested Type : StatusIconMode

    /// <summary>
    /// Enum to set the display mode of the status icon.
    /// </summary>
    public enum StatusIconMode
    {
      /// <summary />
      Gray,

      /// <summary />
      WhiteWithColorBackground
    }

    #endregion Nested Type : StatusIconMode

    private static readonly Brush DefaultSimpleIconBrush = VisualResources.Resources.DefaultResources.GetResource<Brush>("DefaultSimpleIconBrush");
    private static readonly Brush StatusOkBrush = VisualResources.Resources.DefaultResources.GetResource<Brush>("StatusOkBrush");
    private static readonly Brush StatusErrorBrush = VisualResources.Resources.DefaultResources.GetResource<Brush>("StatusErrorBrush");
    private static readonly Brush StatusCautionBrush = VisualResources.Resources.DefaultResources.GetResource<Brush>("StatusCautionBrush");
    private static readonly Brush StatusWarningBrush = VisualResources.Resources.DefaultResources.GetResource<Brush>("StatusWarningBrush");
    private static readonly Brush StatusInformationBrush = VisualResources.Resources.DefaultResources.GetResource<Brush>("StatusInformationBrush");
    private static readonly Brush StatusQuestionBrush = VisualResources.Resources.DefaultResources.GetResource<Brush>("StatusQuestionBrush");
    private static readonly Brush StatusUpdateRequiredBrush = VisualResources.Resources.DefaultResources.GetResource<Brush>("StatusUpdateRequiredBrush");

    public static readonly DependencyProperty IconBrushProperty = DependencyProperty.Register("IconBrush", typeof(Brush), typeof(MetroStatusIcon), new PropertyMetadata(DefaultSimpleIconBrush, IconChangedHandler));

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

    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(StatusIconType), typeof(MetroStatusIcon), new PropertyMetadata(StatusIconType.NoIcon, IconChangedHandler));

    public StatusIconType Type
    {
      get
      {
        return (StatusIconType)GetValue(TypeProperty);
      }

      set
      {
        SetValue(TypeProperty, value);
      }
    }

    public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(StatusIconMode), typeof(MetroStatusIcon), new PropertyMetadata(StatusIconMode.WhiteWithColorBackground, IconChangedHandler));

    public StatusIconMode Mode
    {
      get
      {
        return (StatusIconMode)GetValue(ModeProperty);
      }

      set
      {
        SetValue(ModeProperty, value);
      }
    }

    private static void IconChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var icon = d as MetroStatusIcon;
      if (icon != null)
      {
        icon.UpdateIcon();
      }
    }

    public static readonly DependencyProperty IconBackgroundProperty = DependencyProperty.Register("IconBackground", typeof(Brush), typeof(MetroStatusIcon), new PropertyMetadata(Brushes.Transparent));

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

    public static readonly DependencyProperty IconCornerRadiusProperty = DependencyProperty.Register("IconCornerRadius", typeof(CornerRadius), typeof(MetroStatusIcon), new PropertyMetadata(new CornerRadius(0)));

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
    /// The IconSize dependency property
    /// </summary>
    public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register("IconSize", typeof(double), typeof(MetroStatusIcon), new PropertyMetadata(48d, IconSizeChangedHandler));

    private static void IconSizeChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var icon = d as MetroStatusIcon;
      if (icon != null)
      {
        icon.Height = icon.IconSize;
        icon.Width = icon.IconSize;
        var padding = Math.Floor(icon.IconSize / 6.0);
        if (icon.IconSize < 16)
        {
          padding = 1;
        }

        icon._border.Padding = new Thickness(padding);
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

    /// <summary>
    /// The IconPadding dependency property
    /// </summary>
    public static readonly DependencyProperty IconPaddingProperty = DependencyProperty.Register("IconPadding", typeof(Thickness), typeof(MetroStatusIcon), new PropertyMetadata(new Thickness(6), IconPaddingChangedHandler));

    private static void IconPaddingChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs args)
    {
      var icon = d as MetroStatusIcon;
      if (icon == null)
      {
        return;
      }

      icon._border.Padding = icon.IconPadding;
    }

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

    private readonly IDictionary<StatusIconType, Brush> _defaultBrushes;

    /// <summary>
    /// Initializes a new instance of the <see cref="MetroStatusIcon"/> class.
    /// </summary>
    public MetroStatusIcon()
    {
      InitializeComponent();

      _defaultBrushes = new Dictionary<StatusIconType, Brush>
        {
          { StatusIconType.NoIcon, Brushes.White },
          { StatusIconType.Critical, StatusErrorBrush },
          { StatusIconType.Error, StatusErrorBrush },
          { StatusIconType.Caution, StatusCautionBrush },
          { StatusIconType.Information, StatusInformationBrush },
          { StatusIconType.Ok, StatusOkBrush },
          { StatusIconType.Question, StatusQuestionBrush },
          { StatusIconType.Warning, StatusWarningBrush },
          { StatusIconType.UpdateRequired, StatusUpdateRequiredBrush }
        };
    }

    private void UpdateIcon()
    {
      if (Type == StatusIconType.NoIcon)
      {
        _viewBox.Visibility = Visibility.Collapsed;
      }
      else
      {
        _viewBox.Visibility = Visibility.Visible;
        var shapeName = "status" + DefaultStringConverter.FormatEnum(Type);
        var shapeTemplate = VisualResources.Resources.DefaultResources.GetResource<Path>(shapeName);

        if (Mode == StatusIconMode.Gray)
        {
          ClearValue(IconBackgroundProperty);
          IconBrush = DefaultSimpleIconBrush;
        }
        else
        {
          IconBackground = _defaultBrushes[Type];
          IconBrush = Brushes.White;
        }

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
