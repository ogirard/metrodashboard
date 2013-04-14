// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetroBullet.xaml.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Item
{
  /// <summary>
  /// Interaction logic for MetroBullet.xaml
  /// </summary>
  public partial class MetroBullet
  {
    /// <summary>
    /// The BulletText dependency property
    /// </summary>
    public static readonly DependencyProperty BulletNumberProperty = DependencyProperty.Register("BulletText", typeof(string), typeof(MetroBullet), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the bullet text. No text is shown if set to <tt>null</tt>
    /// </summary>
    public string BulletText
    {
      get
      {
        return (string)GetValue(BulletNumberProperty);
      }

      set
      {
        SetValue(BulletNumberProperty, value);
      }
    }

    /// <summary>
    /// The BorderVisibility dependency property
    /// </summary>
    public static readonly DependencyProperty BorderVisibilityProperty = DependencyProperty.Register("BorderVisibility", typeof(Visibility), typeof(MetroBullet), new PropertyMetadata(Visibility.Collapsed));

    /// <summary>
    /// Gets or sets the border visibility.
    /// </summary>
    public Visibility BorderVisibility
    {
      get
      {
        return (Visibility)GetValue(BorderVisibilityProperty);
      }

      set
      {
        SetValue(BorderVisibilityProperty, value);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MetroBullet"/> class.
    /// </summary>
    public MetroBullet()
    {
      InitializeComponent();
    }
  }
}
