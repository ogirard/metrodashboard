// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageBoxProvider.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ZTG.WPF.Dashboard.Shared.UserInterface.Window;
using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Navigation
{
  public class MessageBoxProvider
  {
    public static void ShowMessageBox(MessageInfo messageInfo)
    {
      messageInfo.ArgumentNotNull("messageInfo");

      var messageBox = new MetroMessageBox(messageInfo);
      messageBox.ShowDialog();
    }
  }
}