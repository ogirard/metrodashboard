// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslateBindingExtension.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows.Data;

namespace ZTG.WPF.Dashboard.Shared.Localization.WPF
{
  /// <summary>
  /// Translate binding extensions
  /// </summary>
  public static class TranslateBindingExtension
  {
    /// <summary>
    /// Gets the translate binding.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public static Binding GetTranslateBinding(string key)
    {
      return new Binding("Value") { Source = new TranslationData(key) };
    }

    /// <summary>
    /// Gets the translate binding treeting this string as translation key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public static Binding AsTranslateBinding(this string key)
    {
      return GetTranslateBinding(key);
    }
  }
}
