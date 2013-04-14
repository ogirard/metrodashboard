// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetroWindowBase.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

using ZTG.WPF.Dashboard.Shared.Extensions;
using ZTG.WPF.Dashboard.Shared.Native;
using ZTG.WPF.Dashboard.Shared.WPF;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Window
{
  /// <summary>
  /// Interaction logic for MetroWindowBase.xaml
  /// </summary>
  public abstract class MetroWindowBase : System.Windows.Window
  {
    private const string PartPrefix = "PART_Chrome_";
    private const int MinWidthHeight = 300;

    /// <summary>
    /// The ChromeBorderWidth dependency property
    /// </summary>
    public static readonly DependencyProperty ChromeBorderWidthProperty = DependencyProperty.Register("ChromeBorderWidth", typeof(double), typeof(MetroWindowBase), new PropertyMetadata(2d));

    /// <summary>
    /// Gets or sets the width of the chrome border.
    /// </summary>
    public double ChromeBorderWidth
    {
      get
      {
        return (double)GetValue(ChromeBorderWidthProperty);
      }

      set
      {
        SetValue(ChromeBorderWidthProperty, value);
      }
    }

    /// <summary>
    /// The ChromeIcon dependency property
    /// </summary>
    public static readonly DependencyProperty ChromeIconProperty = DependencyProperty.Register("ChromeIcon", typeof(ImageSource), typeof(MetroWindowBase), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the chrome icon. This icon is displayed as Window icon if <see cref="ShowIcon"/> is <tt>true</tt>
    /// </summary>
    public ImageSource ChromeIcon
    {
      get
      {
        return (ImageSource)GetValue(ChromeIconProperty);
      }

      set
      {
        SetValue(ChromeIconProperty, value);
      }
    }

    /// <summary>
    /// The ChromeIconPath dependency property
    /// </summary>
    public static readonly DependencyProperty ChromeIconPathProperty = DependencyProperty.Register("ChromeIconPath", typeof(Geometry), typeof(MetroWindowBase), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the chrome icon path. This icon is displayed as Window icon if <see cref="ShowIcon"/> is <tt>true</tt> and <see cref="ChromeIcon"/> is not set
    /// </summary>
    public Geometry ChromeIconPath
    {
      get
      {
        return (Geometry)GetValue(ChromeIconPathProperty);
      }

      set
      {
        SetValue(ChromeIconPathProperty, value);
      }
    }

    /// <summary>
    /// The ChromeBackground dependency property
    /// </summary>
    public static readonly DependencyProperty ChromeBackgroundProperty = DependencyProperty.Register("ChromeBackground", typeof(Brush), typeof(MetroWindowBase), new PropertyMetadata(Brushes.White));

    /// <summary>
    /// Gets or sets the caption background.
    /// </summary>
    public Brush ChromeBackground
    {
      get
      {
        return (Brush)GetValue(ChromeBackgroundProperty);
      }

      set
      {
        SetValue(ChromeBackgroundProperty, value);
      }
    }

    /// <summary>
    /// The ChromeForeground dependency property
    /// </summary>
    public static readonly DependencyProperty ChromeForegroundProperty = DependencyProperty.Register("ChromeForeground", typeof(Brush), typeof(MetroWindowBase), new PropertyMetadata(Brushes.White));

    /// <summary>
    /// Gets or sets the Chrome Foreground.
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
    /// The ContentBackground dependency property
    /// </summary>
    public static readonly DependencyProperty ContentBackgroundProperty = DependencyProperty.Register("ContentBackground", typeof(Brush), typeof(MetroWindowBase), new PropertyMetadata(Brushes.White));

    /// <summary>
    /// Gets or sets the content background.
    /// </summary>
    public Brush ContentBackground
    {
      get
      {
        return (Brush)GetValue(ContentBackgroundProperty);
      }

      set
      {
        SetValue(ContentBackgroundProperty, value);
      }
    }

    /// <summary>
    /// The ContentBorderBrush dependency property
    /// </summary>
    public static readonly DependencyProperty ContentBorderBrushProperty = DependencyProperty.Register("ContentBorderBrush", typeof(Brush), typeof(MetroWindowBase), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the inner border brush.
    /// </summary>
    public Brush ContentBorderBrush
    {
      get
      {
        return (Brush)GetValue(ContentBorderBrushProperty);
      }

      set
      {
        SetValue(ContentBorderBrushProperty, value);
      }
    }

    public static readonly DependencyProperty ContentBorderThicknessProperty = DependencyProperty.Register("ContentBorderThickness", typeof(Thickness), typeof(MetroWindowBase), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the inner border thickness.
    /// </summary>
    public Thickness ContentBorderThickness
    {
      get
      {
        return (Thickness)GetValue(ContentBorderThicknessProperty);
      }

      set
      {
        SetValue(ContentBorderThicknessProperty, value);
      }
    }

    /// <summary>
    /// The ShowMinimizeButton dependency property
    /// </summary>
    public static readonly DependencyProperty ShowMinimizeButtonProperty = DependencyProperty.Register("ShowMinimizeButton", typeof(bool), typeof(MetroWindowBase), new PropertyMetadata(true));

    /// <summary>
    /// Gets or sets a value indicating whether the Minimize button should be displayed.
    /// </summary>
    public bool ShowMinimizeButton
    {
      get
      {
        return (bool)GetValue(ShowMinimizeButtonProperty);
      }

      set
      {
        SetValue(ShowMinimizeButtonProperty, value);
      }
    }

    /// <summary>
    /// The ShowMinimizeButton dependency property
    /// </summary>
    public static readonly DependencyProperty ShowMaximizeOrRestoreButtonProperty = DependencyProperty.Register("ShowMaximizeOrRestoreButton", typeof(bool), typeof(MetroWindowBase), new PropertyMetadata(true));

    /// <summary>
    /// Gets or sets a value indicating whether the Maximize/Restore buttons should be displayed.
    /// </summary>
    public bool ShowMaximizeOrRestoreButton
    {
      get
      {
        return (bool)GetValue(ShowMaximizeOrRestoreButtonProperty);
      }

      set
      {
        SetValue(ShowMaximizeOrRestoreButtonProperty, value);
      }
    }

    /// <summary>
    /// The ShowCloseButton dependency property
    /// </summary>
    public static readonly DependencyProperty ShowCloseButtonProperty = DependencyProperty.Register("ShowCloseButton", typeof(bool), typeof(MetroWindowBase), new PropertyMetadata(true));

    /// <summary>
    /// Gets or sets a value indicating whether the Close button should be displayed.
    /// </summary>
    public bool ShowCloseButton
    {
      get
      {
        return (bool)GetValue(ShowCloseButtonProperty);
      }

      set
      {
        SetValue(ShowCloseButtonProperty, value);
      }
    }

    /// <summary>
    /// The ShowIcon dependency property
    /// </summary>
    public static readonly DependencyProperty ShowIconProperty = DependencyProperty.Register("ShowIcon", typeof(bool), typeof(MetroWindowBase), new PropertyMetadata(true));

    /// <summary>
    /// Gets or sets a value indicating whether the chrome icon should be displayed.
    /// </summary>
    public bool ShowIcon
    {
      get
      {
        return (bool)GetValue(ShowIconProperty);
      }

      set
      {
        SetValue(ShowIconProperty, value);
      }
    }

    /// <summary>
    /// The ShowShadow dependency property
    /// </summary>
    public static readonly DependencyProperty ShowShadowProperty = DependencyProperty.Register("ShowShadow", typeof(bool), typeof(MetroWindowBase), new PropertyMetadata(true));

    /// <summary>
    /// Gets or sets a value indicating whether the window shadow should be displayed.
    /// </summary>
    public bool ShowShadow
    {
      get
      {
        return (bool)GetValue(ShowShadowProperty);
      }

      set
      {
        SetValue(ShowShadowProperty, value);
      }
    }

    /// <summary>
    /// The HelpCommand dependency property
    /// </summary>
    public static readonly DependencyProperty HelpCommandProperty = DependencyProperty.Register("HelpCommand", typeof(DelegateCommand), typeof(MetroWindowBase), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the help command.
    /// </summary>
    public DelegateCommand HelpCommand
    {
      get
      {
        return (DelegateCommand)GetValue(HelpCommandProperty);
      }

      set
      {
        SetValue(HelpCommandProperty, value);
      }
    }

    /// <summary>
    /// The AboutCommand dependency property
    /// </summary>
    public static readonly DependencyProperty AboutCommandProperty = DependencyProperty.Register("AboutCommand", typeof(DelegateCommand), typeof(MetroWindowBase), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the about command.
    /// </summary>
    public DelegateCommand AboutCommand
    {
      get
      {
        return (DelegateCommand)GetValue(AboutCommandProperty);
      }

      set
      {
        SetValue(AboutCommandProperty, value);
      }
    }

    /// <summary>
    /// The IsBusy dependency property
    /// </summary>
    public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool), typeof(MetroWindowBase), new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets a value indicating whether this instance is busy.
    /// </summary>
    public bool IsBusy
    {
      get
      {
        return (bool)GetValue(IsBusyProperty);
      }

      set
      {
        SetValue(IsBusyProperty, value);
      }
    }

    /// <summary>
    /// The BusyText dependency property
    /// </summary>
    public static readonly DependencyProperty BusyTextProperty = DependencyProperty.Register("BusyText", typeof(string), typeof(MetroWindowBase), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the busy text.
    /// </summary>
    public string BusyText
    {
      get
      {
        return (string)GetValue(BusyTextProperty);
      }

      set
      {
        SetValue(BusyTextProperty, value);
      }
    }

    /// <summary>
    /// The DisplayAfter dependency property
    /// </summary>
    public static readonly DependencyProperty DisplayAfterProperty = DependencyProperty.Register("DisplayAfter", typeof(TimeSpan), typeof(MetroWindowBase), new PropertyMetadata(TimeSpan.Zero));

    /// <summary>
    /// Gets or sets the display after.
    /// </summary>
    public TimeSpan DisplayAfter
    {
      get
      {
        return (TimeSpan)GetValue(DisplayAfterProperty);
      }

      set
      {
        SetValue(DisplayAfterProperty, value);
      }
    }

    [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Static constructor is needed for Metadata overrides")]
    static MetroWindowBase()
    {
      // append change handlers
      DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroWindowBase), new FrameworkPropertyMetadata(typeof(MetroWindowBase)));
      WindowStyleProperty.OverrideMetadata(typeof(MetroWindowBase), new FrameworkPropertyMetadata(WindowStyle.None, FrameworkPropertyMetadataOptions.None, null, WindowStyleCoerceHandler));
      ResizeModeProperty.OverrideMetadata(typeof(MetroWindowBase), new FrameworkPropertyMetadata(ResizeMode.CanResize, FrameworkPropertyMetadataOptions.AffectsArrange, ResizeChangedHandler));
      WindowStateProperty.OverrideMetadata(typeof(MetroWindowBase), new FrameworkPropertyMetadata(WindowState.Normal, FrameworkPropertyMetadataOptions.AffectsArrange, WindowStateChangedHandler));
    }

    private static void ResizeChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var window = d as MetroWindowBase;
      if (window == null)
      {
        return;
      }

      window.SynchronizeResizeMode();
    }

    private static void WindowStateChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var window = d as MetroWindowBase;
      if (window == null)
      {
        return;
      }

      window.SynchronizeResizeMode();
    }

    private static object WindowStyleCoerceHandler(DependencyObject d, object basevalue)
    {
      if (basevalue is WindowStyle)
      {
        // keep always WindowStyle.None
        return WindowStyle.None;
      }

      return basevalue;
    }

    private MetroWindowCaption _windowHeader;

    private readonly IList<Tuple<Rectangle, Cursor>> _chromeBorders = new List<Tuple<Rectangle, Cursor>>();

    private bool CanResize
    {
      get
      {
        return ResizeMode != ResizeMode.NoResize && WindowState == WindowState.Normal;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MetroWindowBase"/> class.
    /// </summary>
    protected MetroWindowBase()
    {
      // load default resources of TCG.MS.UserControl.Controls
      Resources.MergedDictionaries.Add(VisualResources.Resources.DefaultResources);

      // apply default style (including control template)
      Style = Resources.GetResource<Style>("DefaultMSWindowStyle");

      // set default windows startup location
      WindowStartupLocation = WindowStartupLocation.CenterOwner;

      MinWidth = MinWidthHeight;
      MinHeight = MinWidthHeight;

      // Set language explicitly. New Telerik behavior since release 2012 Q2.
      Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);

      Loaded += OnLoaded;
      SourceInitialized += MSWindowBaseSourceInitializedHandler;
    }

    private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
    {
      _windowHeader = GetTemplateChild("PART_WindowHeader") as MetroWindowCaption;

      Initialize();

      // move the keyboard focus to first focusable 
      var frameworkElement = Content as FrameworkElement;
      if (frameworkElement != null)
      {
        frameworkElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
      }
      else
      {
        MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
      }
    }

    protected override void OnClosed(EventArgs e)
    {
      base.OnClosed(e);
      Loaded -= OnLoaded;

      foreach (WindowResizeDirection direction in Enum.GetValues(typeof(WindowResizeDirection)))
      {
        var chromePartName = PartPrefix + direction.ToString("F");
        var chromePart = GetTemplateChild(chromePartName) as Rectangle;
        if (chromePart == null)
        {
          continue;
        }

        chromePart.MouseLeftButtonDown -= ResizeDownHandler;
      }
    }

    private void Initialize()
    {
      foreach (WindowResizeDirection direction in Enum.GetValues(typeof(WindowResizeDirection)))
      {
        var chromePartName = PartPrefix + direction.ToString("F");
        var chromePart = GetTemplateChild(chromePartName) as Rectangle;
        if (chromePart == null)
        {
          continue;
        }

        _chromeBorders.Add(new Tuple<Rectangle, Cursor>(chromePart, chromePart.Cursor));
        chromePart.MouseLeftButtonDown += ResizeDownHandler;
      }

      SynchronizeResizeMode();
    }

    private void ResizeDownHandler(object sender, MouseButtonEventArgs e)
    {
      var rect = sender as Rectangle;
      if (rect == null)
      {
        return;
      }

      var directionLabel = rect.Name.Substring(PartPrefix.Length);
      var direction = (WindowResizeDirection)Enum.Parse(typeof(WindowResizeDirection), directionLabel);
      WpfHelper.DragResize(this, direction);
    }

    private void SynchronizeResizeMode()
    {
      foreach (var chromeBorder in _chromeBorders)
      {
        chromeBorder.Item1.Cursor = CanResize ? chromeBorder.Item2 : Cursors.Arrow;
      }

      if (_windowHeader != null)
      {
        _windowHeader.NotifyStateChanged();
      }
    }

    private void MSWindowBaseSourceInitializedHandler(object sender, EventArgs e)
    {
      var handle = this.GetHandle();
      var hwndSource = HwndSource.FromHwnd(handle);
      if (hwndSource != null)
      {
        hwndSource.AddHook(WindowProc);
      }
    }

    private IntPtr WindowProc(IntPtr windowHandle, int messageId, IntPtr wparam, IntPtr lparam, ref bool handled)
    {
      switch (messageId)
      {
        case NativeMethods.WM_GETMINMAXINFO:
          var minimumSize = new Size(MinWidth, MinHeight);
          NativeMethods.WmGetMinMaxInfo(windowHandle, lparam, minimumSize);
          handled = true;
          break;
      }

      return (IntPtr)0;
    }
  }
}
