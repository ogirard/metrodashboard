// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Resources.xaml.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Styles
{
  /// <summary>
  /// <see cref="ResourceDictionary"/> with styling resources to be loaded in base containers (windows) which use controls of this library
  /// </summary>
  public partial class Resources : ResourceDictionary
  {
    private static ResourceDictionary _defaultResources;

    /// <summary>
    /// Gets the default resources to be used in the Management Suite.
    /// </summary>
    public static ResourceDictionary DefaultResources
    {
      get
      {
        if (_defaultResources == null)
        {
          _defaultResources = new Resources();

          // Add the resources to the MergedDictionaries of dummyObject 
          // dummyObject will then be assigned to the Resources.
          // This is done to prevent memory leaks
          var dummyObject = new FrameworkElement();
          dummyObject.Resources.MergedDictionaries.Add(_defaultResources);
        }

        return _defaultResources;
      }
    }

    public Resources()
    {
      InitializeComponent();
    }
  }
}