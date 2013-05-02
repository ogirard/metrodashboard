// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MDWindow.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

using MahApps.Metro.Behaviours;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Windows
{
  /// <summary>
  /// CustomControl: Window with Metro Style
  /// </summary>
  public class MDWindow : Window
  {
    static MDWindow()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(MDWindow), new FrameworkPropertyMetadata(typeof(MDWindow)));
    }

    /// <summary>
    /// The TitleBarHeight dependency property
    /// </summary>
    public static readonly DependencyProperty TitleBarHeightProperty = DependencyProperty.Register("TitleBarHeight", typeof(int), typeof(MDWindow), new PropertyMetadata(48));

    /// <summary>
    /// Gets or sets the height of the title bar.
    /// </summary>
    public int TitleBarHeight
    {
      get
      {
        return (int)GetValue(TitleBarHeightProperty);
      }

      set
      {
        SetValue(TitleBarHeightProperty, value);
      }
    }

    /// <summary>
    /// The ChromeIconPathData dependency property
    /// </summary>
    public static readonly DependencyProperty ChromeIconPathDataProperty = DependencyProperty.Register("ChromeIconPathData", typeof(string), typeof(MDWindow), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the chrome icon path data.
    /// </summary>
    public string ChromeIconPathData
    {
      get
      {
        return (string)GetValue(ChromeIconPathDataProperty);
      }

      set
      {
        SetValue(ChromeIconPathDataProperty, value);
      }
    }

    /// <summary>
    /// The ChromeIconBrush dependency property
    /// </summary>
    public static readonly DependencyProperty ChromeIconBrushProperty = DependencyProperty.Register(
      "ChromeIconBrush", typeof(Brush), typeof(MDWindow), new PropertyMetadata(Brushes.White));

    /// <summary>
    /// Gets or sets the chrome icon brush.
    /// </summary>
    public Brush ChromeIconBrush
    {
      get
      {
        return (Brush)GetValue(ChromeIconBrushProperty);
      }

      set
      {
        SetValue(ChromeIconBrushProperty, value);
      }
    }

    /// <summary>
    /// The ChromeForeground dependency property
    /// </summary>
    public static readonly DependencyProperty ChromeForegroundProperty = DependencyProperty.Register("ChromeForeground", typeof(Brush), typeof(MDWindow), new PropertyMetadata(Brushes.White));

    /// <summary>
    /// Gets or sets the chrome foreground.
    /// </summary>
    public Brush ChromeForeground
    {
      get
      {
        return (Brush)GetValue(ChromeForegroundProperty);
      }

      set
      {
        SetValue(ChromeForegroundProperty, value);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MDWindow"/> class.
    /// </summary>
    public MDWindow()
    {
      var behaviors = Interaction.GetBehaviors(this);
      behaviors.Add(new BorderlessWindowBehavior());
      Style = Application.Current.FindResource("MDWindowDefaultStyle") as Style;

      InputBindings.Add(new InputBinding(ApplicationCommands.Help, new KeyGesture(Key.F1)));
      CommandBindings.Add(new CommandBinding(ApplicationCommands.Help, ShowHelp));
    }

    private static void ShowHelp(object sender, ExecutedRoutedEventArgs e)
    {
    }
  }
}