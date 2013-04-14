// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslationSource.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel;

namespace ZTG.WPF.Dashboard.Shared.Localization.WPF.Internal
{
  /// <summary>
  /// Class acting as source for the binding.
  /// </summary>
  public class TranslationSource : INotifyPropertyChanged
  {
    #region Private Members

    private readonly string _key;

    #endregion

    #region Construction

    /// <summary>
    /// Initializes a new instance of the <see cref="TranslationSource"/> class.
    /// </summary>
    public TranslationSource(string key)
    {
      _key = key;
      TranslationManager.LanguageChanged += TranslationManagerLanguageChanged;
    }

    #endregion

    #region Public Methodes

    /// <summary>
    /// Gets the translated value.
    /// </summary>
    /// <value>The translated value.</value>
    public object TranslatedValue
    {
      get
      {
        if (_key == null)
        {
          // If no key is given, return null as translated value to avoid unnecessary translation rounds
          return null;
        }

        return TranslationManager.Translate(_key);
      }
    }

    #endregion

    #region Private Methodes

    private void TranslationManagerLanguageChanged(object sender, object eventArgs)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
      }
    }

    #endregion

    #region INotifyPropertyChanged

    /// <summary>
    /// See <see cref="INotifyPropertyChanged"/>
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}
