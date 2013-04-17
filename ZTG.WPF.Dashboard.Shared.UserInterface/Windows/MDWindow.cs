// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MDWindow.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Interactivity;

using MahApps.Metro.Behaviours;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Windows
{
  public class MDWindow : Window
  {
    static MDWindow()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(MDWindow), new FrameworkPropertyMetadata(typeof(MDWindow)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MDWindow"/> class.
    /// </summary>
    public MDWindow()
    {
      var behaviors = Interaction.GetBehaviors(this);
      behaviors.Add(new BorderlessWindowBehavior());

      Style = Application.Current.FindResource("MDWindowDefaultStyle") as Style;
    }
  }
}