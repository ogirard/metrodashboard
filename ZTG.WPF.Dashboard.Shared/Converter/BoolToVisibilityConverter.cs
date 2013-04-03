//----------------------------------------------------------------------------------------------------
// <copyright file="BoolToVisibilityConverter.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ZTG.WPF.Dashboard.Shared.Converter
{
  /// <summary>
  /// Value converter converting a boolean to a <see cref="Visibility"/>. True converts to Visibility.Visible, false to Visibility.Collapsed.
  /// If the parameter is true (bool or string), the converter inverts the result.
  /// </summary>
  public class BoolToVisibilityConverter : ValueConverterMarkupExtensionBase, IValueConverter
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
      if (targetType != typeof(Visibility))
      {
        throw new InvalidOperationException("Only target type Visibility supported by this converter!");
      }

      var invert = false;
      if (parameter != null)
      {
        if (parameter is string)
        {
          invert = bool.Parse(parameter.ToString());
        }
        else
        {
          invert = (bool)parameter;
        }
      }

      if (value is bool)
      {
        return invert
                   ? ((bool)value) ? Visibility.Collapsed : Visibility.Visible
                   : ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
      }

      return Visibility.Collapsed;
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