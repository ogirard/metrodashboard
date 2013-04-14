// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetroSimpleDialogButtonViewModel.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Media;

using ZTG.WPF.Dashboard.Shared.WPF;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Window
{
  public class MetroSimpleDialogButtonViewModel : NotificationObject
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MetroSimpleDialogButtonViewModel"/> class.
    /// </summary>
    internal MetroSimpleDialogButtonViewModel()
    {
    }

    private DelegateCommand _command;

    /// <summary>
    /// Gets or sets the Command.
    /// </summary>
    /// <value>The Command value.</value>
    public DelegateCommand Command
    {
      get
      {
        return _command;
      }

      set
      {
        ChangeAndNotify(ref _command, value, "Command");
      }
    }

    private string _displayText;

    /// <summary>
    /// Gets or sets the translated display text.
    /// </summary>
    /// <value>The DisplayText value.</value>
    public string DisplayText
    {
      get
      {
        return _displayText;
      }

      set
      {
        ChangeAndNotify(ref _displayText, value, "DisplayText");
      }
    }

    private ImageSource _icon;

    /// <summary>
    /// Gets or sets the Icon.
    /// </summary>
    /// <value>The Icon value.</value>
    public ImageSource Icon
    {
      get
      {
        return _icon;
      }

      set
      {
        ChangeAndNotify(ref _icon, value, "Icon");
      }
    }

    private bool _isDefaultButton;

    /// <summary>
    /// Gets a flag indicating if this button is the default button or not. Only one button can be the default button of one dialog!
    /// </summary>
    /// <value>The IsDefaultButton value.</value>
    public bool IsDefaultButton
    {
      get
      {
        return _isDefaultButton;
      }

      internal set
      {
        ChangeAndNotify(ref _isDefaultButton, value, "IsDefaultButton");
      }
    }

    private bool _isCancelButton;

    /// <summary>
    /// Gets a flag indicating if this button is the cancel button or not. Only one button can be the cancel button of one dialog!
    /// </summary>
    /// <value>The IsCancelButton value.</value>
    public bool IsCancelButton
    {
      get
      {
        return _isCancelButton;
      }

      internal set
      {
        ChangeAndNotify(ref _isCancelButton, value, "IsCancelButton");
      }
    }

    private Visibility _buttonVisibility = Visibility.Visible;

    /// <summary>
    /// Gets or sets the ButtonVisibility.
    /// </summary>
    /// <value>The ButtonVisibility value.</value>
    public Visibility ButtonVisibility
    {
      get
      {
        return _buttonVisibility;
      }

      set
      {
        ChangeAndNotify(ref _buttonVisibility, value, "ButtonVisibility");
      }
    }

    private MessageBoxResult _result = MessageBoxResult.None;

    /// <summary>
    /// Gets or sets the Result.
    /// </summary>
    /// <value>The Result value.</value>
    public MessageBoxResult Result
    {
      get
      {
        return _result;
      }

      set
      {
        ChangeAndNotify(ref _result, value, "Result");
      }
    }
  }
}