// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageInfo.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Text;
using System.Windows;

using ZTG.WPF.Dashboard.Shared.Localization;
using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.MessageBox
{
  /// <summary>
  /// MessageBox information
  /// </summary>
  public class MessageInfo
  {
    private readonly DateTime _createdTimeStamp = DateTime.Now;

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageInfo"/> class.
    /// </summary>
    /// <param name="mainMessage">The mainMessage (mandatory, not null or empty) to be displayed.</param>
    public MessageInfo(string mainMessage)
    {
      mainMessage.ArgumentNotNullOrEmpty("mainMessage");
      MainMessage = mainMessage;

      // Init default values
      Caption = "ZTG.WPF.Dashboard.Shared.UserInterface.MessageBox.DefaultTitle".TranslateText();
      Image = MessageBoxImage.Warning;
      Buttons = MessageBoxButton.OK;
      DefaultResult = MessageBoxResult.None;
    }

    /// <summary>
    /// Gets or sets the (optional) caption (=title) of the mainMessage box window
    /// </summary>
    public string Caption { get; set; }

    /// <summary>
    /// Gets the main mainMessage (mandatory, 
    /// </summary>
    public string MainMessage { get; private set; }

    /// <summary>
    /// Gets or sets the description (hint / consequence) to be displayed beneath the <see cref="MainMessage"/>
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the (optional) default mainMessage to be displayed in the expansion area of the mainMessage box. Default is <tt>null</tt> (not displayed)
    /// </summary>
    public string Details { get; set; }

    /// <summary>
    /// Gets or sets the image the dialog should hold - default is <see cref="MessageBoxImage.Warning"/>
    /// </summary>
    public MessageBoxImage Image { get; set; }

    /// <summary>
    /// Gets or sets the buttons the dialog should have - default is <see cref="MessageBoxButton.OK"/>
    /// </summary>
    public MessageBoxButton Buttons { get; set; }

    /// <summary>
    /// Gets or sets the default result:
    /// Select the <see cref="DefaultResult"/> button as default button. If this button is not available, select the first button as default button,
    /// </summary>
    /// <value>The default result.</value>
    public MessageBoxResult DefaultResult { get; set; }

    /// <summary>
    /// Gets the complete Message text with description and details.
    /// </summary>
    /// <returns></returns>
    public string CompleteMessageText
    {
      get
      {
        var text = new StringBuilder();
        text.AppendLine("Dashboard Dialog mainMessage");
        text.AppendLine(_createdTimeStamp.ToString("dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture));
        text.AppendLine();
        text.AppendLine(MainMessage);

        if (!string.IsNullOrEmpty(Description))
        {
          text.AppendLine();
          text.AppendLine(Description);
        }

        if (!string.IsNullOrEmpty(Details))
        {
          text.AppendLine();
          text.AppendLine(Details);
        }

        return text.ToString();
      }
    }
  }
}