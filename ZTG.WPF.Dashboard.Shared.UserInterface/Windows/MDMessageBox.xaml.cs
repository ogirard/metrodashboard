// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MDMessageBox.xaml.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using ZTG.WPF.Dashboard.Shared.UserInterface.Windows.Enums;
using ZTG.WPF.Dashboard.Shared.Utilities;
using ZTG.WPF.Dashboard.Shared.WPF;

using MessageBoxResult = ZTG.WPF.Dashboard.Shared.UserInterface.Windows.Enums.MessageBoxResult;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Windows
{
  /// <summary>
  /// Interaction logic for MDMessageBox.xaml
  /// </summary>
  public partial class MDMessageBox
  {
    /// <summary>
    /// The StatusIconPath dependency property
    /// </summary>
    public static readonly DependencyProperty StatusIconPathProperty = DependencyProperty.Register(
      "StatusIconPath", typeof(Geometry), typeof(MDMessageBox), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the status icon path.
    /// </summary>
    public Geometry StatusIconPath
    {
      get
      {
        return (Geometry)GetValue(StatusIconPathProperty);
      }

      set
      {
        SetValue(StatusIconPathProperty, value);
      }
    }

    /// <summary>
    /// The StatusIconBrush dependency property
    /// </summary>
    public static readonly DependencyProperty StatusIconBrushProperty = DependencyProperty.Register(
      "StatusIconBrush", typeof(Brush), typeof(MDMessageBox), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the status icon brush.
    /// </summary>
    public Brush StatusIconBrush
    {
      get
      {
        return (Brush)GetValue(StatusIconBrushProperty);
      }

      set
      {
        SetValue(StatusIconBrushProperty, value);
      }
    }

    /// <summary>
    /// The Caption dependency property
    /// </summary>
    public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register(
      "Caption", typeof(string), typeof(MDMessageBox), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the caption.
    /// </summary>
    public string Caption
    {
      get
      {
        return (string)GetValue(CaptionProperty);
      }

      set
      {
        SetValue(CaptionProperty, value);
      }
    }

    /// <summary>
    /// The Message dependency property
    /// </summary>
    public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
      "Message", typeof(string), typeof(MDMessageBox), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    public string Message
    {
      get
      {
        return (string)GetValue(MessageProperty);
      }

      set
      {
        SetValue(MessageProperty, value);
      }
    }

    /// <summary>
    /// The Details dependency property
    /// </summary>
    public static readonly DependencyProperty DetailsProperty = DependencyProperty.Register(
      "Details", typeof(string), typeof(MDMessageBox), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the details.
    /// </summary>
    public string Details
    {
      get
      {
        return (string)GetValue(DetailsProperty);
      }

      set
      {
        SetValue(DetailsProperty, value);
      }
    }

    /// <summary>
    /// The ButtonClickCommand dependency property
    /// </summary>
    public static readonly DependencyProperty ButtonClickCommandProperty = DependencyProperty.Register(
      "ButtonClickCommand", typeof(ICommand), typeof(MDMessageBox), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the button click command.
    /// </summary>
    public ICommand ButtonClickCommand
    {
      get
      {
        return (ICommand)GetValue(ButtonClickCommandProperty);
      }

      set
      {
        SetValue(ButtonClickCommandProperty, value);
      }
    }

    /// <summary>
    /// The YesButtonVisibility dependency property
    /// </summary>
    public static readonly DependencyProperty YesButtonVisibilityProperty = DependencyProperty.Register(
      "YesButtonVisibility", typeof(Visibility), typeof(MDMessageBox), new PropertyMetadata(Visibility.Collapsed));

    /// <summary>
    /// Gets or sets the yes button visibility.
    /// </summary>
    public Visibility YesButtonVisibility
    {
      get
      {
        return (Visibility)GetValue(YesButtonVisibilityProperty);
      }

      set
      {
        SetValue(YesButtonVisibilityProperty, value);
      }
    }

    /// <summary>
    /// The OkButtonVisibility dependency property
    /// </summary>
    public static readonly DependencyProperty OkButtonVisibilityProperty = DependencyProperty.Register(
      "OkButtonVisibility", typeof(Visibility), typeof(MDMessageBox), new PropertyMetadata(Visibility.Collapsed));

    /// <summary>
    /// Gets or sets the Ok button visibility.
    /// </summary>
    public Visibility OkButtonVisibility
    {
      get
      {
        return (Visibility)GetValue(OkButtonVisibilityProperty);
      }

      set
      {
        SetValue(OkButtonVisibilityProperty, value);
      }
    }

    /// <summary>
    /// The CancelButtonVisibility dependency property
    /// </summary>
    public static readonly DependencyProperty CancelButtonVisibilityProperty = DependencyProperty.Register(
      "CancelButtonVisibility", typeof(Visibility), typeof(MDMessageBox), new PropertyMetadata(Visibility.Collapsed));

    /// <summary>
    /// Gets or sets the Cancel button visibility.
    /// </summary>
    public Visibility CancelButtonVisibility
    {
      get
      {
        return (Visibility)GetValue(CancelButtonVisibilityProperty);
      }

      set
      {
        SetValue(CancelButtonVisibilityProperty, value);
      }
    }

    /// <summary>
    /// The NoButtonVisibility dependency property
    /// </summary>
    public static readonly DependencyProperty NoButtonVisibilityProperty = DependencyProperty.Register(
      "NoButtonVisibility", typeof(Visibility), typeof(MDMessageBox), new PropertyMetadata(Visibility.Collapsed));

    /// <summary>
    /// Gets or sets the No button visibility.
    /// </summary>
    public Visibility NoButtonVisibility
    {
      get
      {
        return (Visibility)GetValue(NoButtonVisibilityProperty);
      }

      set
      {
        SetValue(NoButtonVisibilityProperty, value);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MDMessageBox"/> class.
    /// </summary>
    public MDMessageBox()
    {
      InitializeComponent();
    }

    public static MessageBoxResult ShowDialog(MessageBoxType type, string caption, string message, string details = null)
    {
      return ShowDialog(Application.Current.MainWindow, type, caption, message, details);
    }

    public static MessageBoxResult ShowDialog(Window owner, MessageBoxType type, string caption, string message, string details = null)
    {
      owner.ArgumentNotNull("owner");
      caption.ArgumentNotNullOrEmpty("caption");
      message.ArgumentNotNullOrEmpty("message");

      var messageBox = new MDMessageBox { Caption = caption, Message = message, Details = details, Owner = owner };
      var result = MessageBoxResult.None;
      messageBox.ButtonClickCommand = new DelegateCommand<MessageBoxResult>(
        mbResult =>
        {
          result = mbResult;
          messageBox.Close();
        });

      switch (type)
      {
        case MessageBoxType.Error:
          messageBox.StatusIconPath = Application.Current.FindResource("StatusIconError") as Geometry;
          messageBox.StatusIconBrush = Application.Current.FindResource("ErrorBrush") as Brush;
          messageBox.OkButtonVisibility = Visibility.Visible;
          break;
        case MessageBoxType.Confirmation:
          messageBox.StatusIconPath = Application.Current.FindResource("StatusIconConfirmation") as Geometry;
          messageBox.StatusIconBrush = Application.Current.FindResource("WarningBrush") as Brush;
          messageBox.YesButtonVisibility = Visibility.Visible;
          messageBox.NoButtonVisibility = Visibility.Visible;
          break;
        case MessageBoxType.Info:
        default:
          messageBox.StatusIconPath = Application.Current.FindResource("StatusIconInformation") as Geometry;
          messageBox.StatusIconBrush = Application.Current.FindResource("HintBrush") as Brush;
          messageBox.OkButtonVisibility = Visibility.Visible;
          break;
      }

      messageBox.Owner = Application.Current.MainWindow;
      messageBox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
      messageBox.ShowDialog();

      return result;
    }
  }
}
