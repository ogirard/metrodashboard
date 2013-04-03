//----------------------------------------------------------------------------------------------------
// <copyright file="NullableToBoolConverter.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows.Data;

namespace ZTG.WPF.Dashboard.Shared.Converter
{
  /// <summary>
  /// Value converter converting a null value a boolean. Null converts to false, not null to true.
  /// </summary>
  public class NullableToBoolConverter : ValueConverterMarkupExtensionBase, IValueConverter
  {
    #region IValueConverter Members

    /// <summary>
    /// See <see cref="IValueConverter.Convert"/>.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>False if the value is null, otherwise true.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return value != null;
    }

    /// <summary>
    /// See <see cref="IValueConverter.ConvertBack"/>.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}