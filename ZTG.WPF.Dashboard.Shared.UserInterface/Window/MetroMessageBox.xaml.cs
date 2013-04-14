// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetroMessageBox.xaml.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

using ZTG.WPF.Dashboard.Shared.Localization;
using ZTG.WPF.Dashboard.Shared.UserInterface.Item;
using ZTG.WPF.Dashboard.Shared.Utilities;
using ZTG.WPF.Dashboard.Shared.WPF;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Window
{
  /// <summary>
  /// Interaction logic for MetroMessageBox.xaml This is the MetroMessageBox dialog class itself
  /// </summary>
  public partial class MetroMessageBox
  {
    private const double DefaultDetailsHeight = 150;

    private readonly MessageInfo _messageInfo;

    private double _withoutDetailsHeight;
    private double _withDetailsHeight;

    #region Constructors

    /// <summary>
    /// Creates a new message box.
    /// </summary>
    /// <param name="messageInfo">The message box info.</param>
    /// <returns></returns>
    public static MetroMessageBox CreateMessageBox(MessageInfo messageInfo)
    {
      return new MetroMessageBox(messageInfo);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MetroMessageBox"/> class.
    /// </summary>
    /// <param name="messageInfo">The message box info containing the message box content to be displayed.</param>
    internal MetroMessageBox(MessageInfo messageInfo)
    {
      messageInfo.ArgumentNotNull("MessageInfo");
      _messageInfo = messageInfo;

      InitializeComponent();
      Initialize();
      DataContext = this;

      Loaded += LoadedHandler;
    }

    private void Initialize()
    {
      // Create buttons
      Buttons = new List<Button> { new Button(this), new Button(this), new Button(this) };

      // Initialize buttons
      switch (_messageInfo.Buttons)
      {
        case MessageBoxButton.OK:
          Buttons[0].Text = "TCG.MS.UserInterface.Controls.Windows.MessageBox.Ok".TranslateText();
          Buttons[0].MessageBoxResult = MessageBoxResult.OK;
          break;
        case MessageBoxButton.OKCancel:
          Buttons[0].Text = "TCG.MS.UserInterface.Controls.Windows.MessageBox.Ok".TranslateText();
          Buttons[0].MessageBoxResult = MessageBoxResult.OK;
          Buttons[1].Text = "TCG.MS.UserInterface.Controls.Windows.MessageBox.Cancel".TranslateText();
          Buttons[1].MessageBoxResult = MessageBoxResult.Cancel;
          break;
        case MessageBoxButton.YesNo:
          Buttons[0].Text = "TCG.MS.UserInterface.Controls.Windows.MessageBox.Yes".TranslateText();
          Buttons[0].MessageBoxResult = MessageBoxResult.Yes;
          Buttons[1].Text = "TCG.MS.UserInterface.Controls.Windows.MessageBox.No".TranslateText();
          Buttons[1].MessageBoxResult = MessageBoxResult.No;
          break;
        case MessageBoxButton.YesNoCancel:
          Buttons[0].Text = "TCG.MS.UserInterface.Controls.Windows.MessageBox.Yes".TranslateText();
          Buttons[0].MessageBoxResult = MessageBoxResult.Yes;
          Buttons[1].Text = "TCG.MS.UserInterface.Controls.Windows.MessageBox.No".TranslateText();
          Buttons[1].MessageBoxResult = MessageBoxResult.No;
          Buttons[2].Text = "TCG.MS.UserInterface.Controls.Windows.MessageBox.Cancel".TranslateText();
          Buttons[2].MessageBoxResult = MessageBoxResult.Cancel;
          break;
      }

      // Initialize default button
      var defaultButtonSet = false;

      if (_messageInfo.DefaultResult != MessageBoxResult.None)
      {
        foreach (var button in Buttons.Where(button => button.MessageBoxResult == _messageInfo.DefaultResult))
        {
          button.IsDefault = defaultButtonSet = true;
          break;
        }
      }

      if (!defaultButtonSet)
      {
        Buttons[0].IsDefault = true;
      }
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the buttons.
    /// </summary>
    public IList<Button> Buttons { get; private set; }

    /// <summary>
    /// Gets the caption.
    /// </summary>
    public string Caption
    {
      get { return _messageInfo.Caption; }
    }

    /// <summary>
    /// Gets the main message.
    /// </summary>
    public string MainMessage
    {
      get { return _messageInfo.MainMessage; }
    }

    /// <summary>
    /// Gets the description.
    /// </summary>
    public string Description
    {
      get { return _messageInfo.Description; }
    }

    /// <summary>
    /// Gets the details.
    /// </summary>
    public string Details
    {
      get { return _messageInfo.Details; }
    }

    /// <summary>
    /// Gets the image.
    /// </summary>
    public MessageBoxImage Image
    {
      get { return _messageInfo.Image; }
    }

    /// <summary>
    /// Gets the type of the status icon.
    /// </summary>
    public MetroStatusIcon.StatusIconType StatusIconType
    {
      get
      {
        switch (_messageInfo.Image)
        {
          case MessageBoxImage.Information:
            return MetroStatusIcon.StatusIconType.Information;

          case MessageBoxImage.Warning:
            return MetroStatusIcon.StatusIconType.Caution;

          case MessageBoxImage.Error:
            return MetroStatusIcon.StatusIconType.Critical;

          case MessageBoxImage.Question:
            return MetroStatusIcon.StatusIconType.Question;

          default:
            return MetroStatusIcon.StatusIconType.NoIcon;
        }
      }
    }

    /// <summary>
    /// The result of this message box (available after closing)
    /// </summary>
    public MessageBoxResult Result { get; set; }

    #endregion

    #region EventHandler

    private void LoadedHandler(object sender, RoutedEventArgs e)
    {
      Loaded -= LoadedHandler;

      Dispatcher.BeginInvoke(new Action(() =>
        {
          // let the dialog size to content, then remember height. Call this when size calculation has been finished.
          _withoutDetailsHeight = Height;
          _withDetailsHeight = _withoutDetailsHeight + DefaultDetailsHeight;
          SizeToContent = SizeToContent.Manual;
          MinHeight = _withoutDetailsHeight;
        }),
        DispatcherPriority.Background);
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
      base.OnRenderSizeChanged(sizeInfo);

      if (_detailsExpander.IsExpanded)
      {
        // remember height with details (changed by user, do not override!)
        _withDetailsHeight = Math.Max(Height, _withoutDetailsHeight + DefaultDetailsHeight);
      }
    }

    private void IsExpandedChangedHandler(object sender, EventArgs e)
    {
      Height = _detailsExpander.IsExpanded ? _withDetailsHeight : _withoutDetailsHeight;
    }

    /// <summary>
    /// Copies all information to clipboard.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
    private void CopyButtonClickHandler(object sender, RoutedEventArgs e)
    {
      Clipboard.SetText(_messageInfo.CompleteMessageText);
    }

    #endregion

    #region    Static Methods

    /// <summary>
    /// Show a modal message box
    /// </summary>
    /// <param name="owner">The owner window if needed, otherwise <tt>null</tt></param>
    /// <param name="messageInfo">The message content to be displayed in the message box</param>
    /// <returns>A message box result enum with the result of the dialog</returns>
    public static MessageBoxResult Show(System.Windows.Window owner, MessageInfo messageInfo)
    {
      // use the same logic as used with navigation info
      var messageBox = CreateMessageBox(messageInfo);
      messageBox.Owner = owner;
      messageBox.ShowDialog();
      return messageBox.Result;
    }

    #endregion

    #region Nested Type: Button

    public class Button
    {
      private readonly MetroMessageBox _window;

      internal Button(MetroMessageBox window)
      {
        window.ArgumentNotNull("window");
        _window = window;

        ClickCommand = new DelegateCommand(Click);
      }

      public string Text { get; internal set; }

      public bool IsDefault { get; internal set; }

      public MessageBoxResult MessageBoxResult { get; internal set; }

      public Visibility Visibility
      {
        get
        {
          return string.IsNullOrEmpty(Text) ? Visibility.Collapsed : Visibility.Visible;
        }
      }

      public DelegateCommand ClickCommand { get; internal set; }

      private void Click()
      {
        _window.DialogResult = MessageBoxResult == MessageBoxResult.OK || MessageBoxResult == MessageBoxResult.Yes;
        _window.Result = MessageBoxResult;

        _window.Close();
      }
    }

    #endregion
  }
}