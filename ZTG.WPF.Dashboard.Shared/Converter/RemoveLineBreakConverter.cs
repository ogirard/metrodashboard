// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveLineBreakConverter.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace ZTG.WPF.Dashboard.Shared.Converter
{
  /// <summary>
  /// Value converter removing line breaks in the given input string. Line breaks and surrounding whitespace are replaced by one space
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "No! We want this name")]
  public class RemoveLineBreakConverter : ValueConverterMarkupExtensionBase, IValueConverter
  {
    #region IValueConverter Members

    /// <summary>
    /// See <see cref="IValueConverter.Convert"/>.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns null, the valid null value is used.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if ((targetType != typeof(string)) && (targetType != typeof(object)))
      {
        throw new InvalidOperationException("Only string conversion supported!");
      }

      var input = value as string;
      if (!string.IsNullOrEmpty(input))
      {
        return string.Join(" ", input.Replace(Environment.NewLine, "\n").Replace('\r', '\n').Split('\n').Select(line => line.Trim()));
      }

      return value;
    }

    /// <summary>
    /// See <see cref="IValueConverter.ConvertBack"/>.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns null, the valid null value is used.
    /// </returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}