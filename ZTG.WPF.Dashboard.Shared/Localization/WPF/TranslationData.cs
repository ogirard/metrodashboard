// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslationData.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Globalization;

namespace ZTG.WPF.Dashboard.Shared.Localization.WPF
{
  /// <summary>
  /// Encapsulates a translated value
  /// </summary>
  public class TranslationData
  {
    private readonly string _key;
    private object _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="TranslationData"/> class.
    /// </summary>
    /// <param name="key">The key.</param>
    public TranslationData(string key)
    {
      _key = key;
      Value = TranslationManager.Translate(key);
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>The value.</value>
    public object Value
    {
      get { return _value; }
      private set { _value = value; }
    }

    /// <summary>
    /// Called when language changed.
    /// </summary>
    /// <param name="newLanguage">The new language.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "We wil use it in the future"), 
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "newLanguage", Justification = "We wil use it in the future")]
    private void OnLanguageChanged(CultureInfo newLanguage)
    {
      Value = TranslationManager.Translate(_key);
    }
  }
}
