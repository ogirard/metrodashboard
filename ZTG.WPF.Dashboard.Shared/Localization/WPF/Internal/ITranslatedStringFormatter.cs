// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITranslatedStringFormatter.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Globalization;

namespace ZTG.WPF.Dashboard.Shared.Localization.WPF.Internal
{
  /// <summary>
  /// Formatter converting a translated string to another string based on the original data object.
  /// </summary>
  /// <remarks>
  /// The "DynamicTranslateExtension" supports this interface if the provided KeyConverter is implementing this interface:
  ///  - The returned string will the be formated using the method <see cref="FormatString"/>.
  /// </remarks>
  public interface ITranslatedStringFormatter
  {
    /// <summary>
    /// Formats the data object as string using a translated string.
    /// </summary>
    /// <returns>The formatted string.</returns>
    string FormatString(object data, string translatedValue, CultureInfo culture);
  }
}
