// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetroSimpleDialogViewModelBase.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

using ZTG.WPF.Dashboard.Shared.Utilities;
using ZTG.WPF.Dashboard.Shared.WPF;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Window
{
  /// <summary>
  /// ViewModel for a simple dialog providing header, title (=dialog caption), error message, and support for Save / Cancel
  /// </summary>
  public abstract class MetroSimpleDialogViewModelBase : NotificationObject
  {
    private string _dialogTitle;
    private string _dialogHeader;
    private string _errorMessage;

    /// <summary>
    /// Initializes a new instance of the <see cref="MetroSimpleDialogViewModelBase"/> class.
    /// </summary>
    protected MetroSimpleDialogViewModelBase()
    {
      Buttons = new ObservableCollection<MetroSimpleDialogButtonViewModel>();
    }

    /// <summary>
    /// Gets or sets the DialogHeader.
    /// </summary>
    /// <value>The DialogHeader value.</value>
    public string DialogHeader
    {
      get
      {
        return _dialogHeader;
      }

      set
      {
        ChangeAndNotify(ref _dialogHeader, value, "DialogHeader");
      }
    }

    /// <summary>
    /// Gets or sets the DialogTitle.
    /// </summary>
    /// <value>The DialogTitle value.</value>
    public string DialogTitle
    {
      get
      {
        return _dialogTitle;
      }

      set
      {
        ChangeAndNotify(ref _dialogTitle, value, "DialogTitle");
      }
    }

    /// <summary>
    /// Gets or sets the localized error message to be displayed in the footer area of the dialog.
    /// </summary>
    public string ErrorMessage
    {
      get
      {
        return _errorMessage;
      }

      set
      {
        ChangeAndNotify(ref _errorMessage, value, "ErrorMessage");
      }
    }

    private MetroSimpleDialogButtonViewModel _defaultButton;

    /// <summary>
    /// Gets or sets the DefaultButton.
    /// </summary>
    /// <value>The DefaultButton value.</value>
    public MetroSimpleDialogButtonViewModel DefaultButton
    {
      get
      {
        return _defaultButton;
      }

      set
      {
        if (ChangeAndNotify(ref _defaultButton, value, "DefaultButton"))
        {
          foreach (var button in Buttons)
          {
            button.IsDefaultButton = button == _defaultButton;
          }
        }
      }
    }

    private MetroSimpleDialogButtonViewModel _cancelButton;

    /// <summary>
    /// Gets or sets the CancelButton.
    /// </summary>
    /// <value>The CancelButton value.</value>
    public MetroSimpleDialogButtonViewModel CancelButton
    {
      get
      {
        return _cancelButton;
      }

      set
      {
        if (ChangeAndNotify(ref _cancelButton, value, "CancelButton"))
        {
          foreach (var button in Buttons)
          {
            button.IsCancelButton = button == _cancelButton;
          }
        }
      }
    }

    /// <summary>
    /// Gets the Buttons.
    /// </summary>
    /// <value>The Buttons value.</value>
    public ObservableCollection<MetroSimpleDialogButtonViewModel> Buttons { get; private set; }

    /// <summary>
    /// Adds the buttons (from right to left)
    /// <remarks>
    /// Buttons with <paramref name="result"/> <see cref="MessageBoxResult.OK"/>/<see cref="MessageBoxResult.Yes"/> are set as <see cref="DefaultButton"/> automatically<br />
    /// Buttons with <paramref name="result"/> <see cref="MessageBoxResult.Cancel"/>/<see cref="MessageBoxResult.No"/> are set as <see cref="CancelButton"/> automatically
    /// </remarks>
    /// </summary>
    /// <param name="displayText">The display text.</param>
    /// <param name="command">The command.</param>
    /// <param name="result">The result.</param>
    /// <returns></returns>
    public MetroSimpleDialogButtonViewModel AddButton(string displayText, DelegateCommand command, MessageBoxResult result)
    {
      displayText.ArgumentNotNullOrEmpty("displayText");
      
      var buttonViewModel = new MetroSimpleDialogButtonViewModel
        {
          DisplayText = displayText,
          Command = command,
          Result = result
        };

      Buttons.Add(buttonViewModel);

      if (result == MessageBoxResult.OK || result == MessageBoxResult.Yes)
      {
        DefaultButton = buttonViewModel;
      }

      if (result == MessageBoxResult.Cancel || result == MessageBoxResult.No)
      {
        CancelButton = buttonViewModel;
      }

      return buttonViewModel;
    }

    private MessageBoxResult _dialogResult = MessageBoxResult.None;

    /// <summary>
    /// Gets the DialogResult
    /// </summary>
    /// <value>The DialogResult value.</value>
    public MessageBoxResult DialogResult
    {
      get
      {
        return _dialogResult;
      }

      private set
      {
        ChangeAndNotify(ref _dialogResult, value, "DialogResult");
      }
    }

    private bool _isClosingHandled;

    /// <summary>
    /// Called when the surrounding window is closing.<br />
    /// - Handles the close button in title bar, ALT + F4, ... actions.<br/>
    /// - Sets the <see cref="DialogResult"/>
    /// </summary>
    internal void HandleClosing(CancelEventArgs e)
    {
      var executingButton = Buttons.FirstOrDefault(button => button.Command != null && button.Command.IsExecuting);

      if (executingButton != null)
      {
        DialogResult = executingButton.Result;
      }
      else if (!_isClosingHandled && CancelButton != null && CancelButton.Command != null)
      {
        // If none of the commands is being executed -> call the CancelButton action if there is any
        e.Cancel = true;
        _isClosingHandled = true;
        Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => CancelButton.Command.Execute(null)));
      }
    }
  }
}