// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetroLabel.xaml.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Item
{
  /// <summary>
  /// Interaction logic for MetroLabel.xaml
  /// </summary>
  public partial class MetroLabel
  {
    #region    Nested Enum : Label Style

    public enum LabelStyle
    {
      /// <summary>
      /// Caption label style, e.g. to be used as window title
      /// </summary>
      Caption,

      /// <summary>
      /// Title label style, e.g. to be used as form header in an add/edit dialog
      /// </summary>
      Title,

      /// <summary>
      /// Section label style
      /// </summary>
      Section,

      /// <summary>
      /// Form section label style
      /// </summary>
      FormSection,

      /// <summary>
      /// Paragraph label style
      /// </summary>
      Paragraph,

      /// <summary>
      /// Field label style, e.g. to be used to label a field
      /// </summary>
      Field,

      /// <summary>
      /// Value label style, e.g. to be used to visualize a content value
      /// </summary>
      Value,

      /// <summary>
      /// Info label style, e.g. to be used to visualize a hint
      /// </summary>
      Info,

      /// <summary>
      /// Tooltip label style, e.g. to be used to visualize a tool tip
      /// </summary>
      Tooltip,

      /// <summary>
      /// Label which is bold to higlight it.
      /// </summary>
      Highlighted,

      /// <summary>
      /// Error label style
      /// </summary>
      Error
    }

    #endregion Nested Enum : Label Style

    /// <summary>
    /// The Type dependency property
    /// </summary>
    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(LabelStyle), typeof(MetroLabel), new PropertyMetadata(LabelStyle.Title));

    /// <summary>
    /// Gets or sets the type of the header.
    /// </summary>
    public LabelStyle Type
    {
      get
      {
        return (LabelStyle)GetValue(TypeProperty);
      }

      set
      {
        SetValue(TypeProperty, value);
      }
    }

    /// <summary>
    /// The IsErrorLabel dependency property
    /// </summary>
    public static readonly DependencyProperty IsErrorLabelProperty = DependencyProperty.Register("IsErrorLabel", typeof(bool), typeof(MetroLabel), new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets a value indicating whether this instance is error label.
    /// </summary>
    public bool IsErrorLabel
    {
      get
      {
        return (bool)GetValue(IsErrorLabelProperty);
      }

      set
      {
        SetValue(IsErrorLabelProperty, value);
      }
    }

    /// <summary>
    /// The IsTextTrimmed dependency property (readonly)
    /// </summary>
    private static readonly DependencyPropertyKey IsTextTrimmedPropertyKey = DependencyProperty.RegisterReadOnly("IsTextTrimmed", typeof(bool), typeof(MetroLabel), new PropertyMetadata(false));

    public static readonly DependencyProperty IsTextTrimmedProperty = IsTextTrimmedPropertyKey.DependencyProperty;

    /// <summary>
    /// Gets a value indicating whether this instance is text trimmed.
    /// </summary>
    public bool IsTextTrimmed
    {
      get
      {
        return (bool)GetValue(IsTextTrimmedPropertyKey.DependencyProperty);
      }

      private set
      {
        SetValue(IsTextTrimmedPropertyKey, value);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MetroLabel"/> class.
    /// </summary>
    public MetroLabel()
    {
      InitializeComponent();

      SizeChanged += SizeChangedHandler;
    }

    private void SizeChangedHandler(object sender, SizeChangedEventArgs e)
    {
      IsTextTrimmed = TextTrimming != TextTrimming.None && CalculateIsTextTrimmed();
    }

    private bool CalculateIsTextTrimmed()
    {
      var typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);

      var formattedText = new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection, typeface, FontSize, Foreground);

      return formattedText.Width > ActualWidth - Padding.Left - Padding.Right;
    }
  }
}
