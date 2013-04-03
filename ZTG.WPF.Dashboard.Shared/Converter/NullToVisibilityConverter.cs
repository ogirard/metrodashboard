//----------------------------------------------------------------------------------------------------
// <copyright file="NullToVisibilityConverter.cs" company="Zühlke Engineering AG">
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
  /// Converts an object to a value of type <see cref="Visibility"/>
  /// object == null: Visibility.Collapsed
  /// object != null: Visibility.Visible
  /// By providing a parameter with boolean value "true", the return values 
  /// are inverted.
  /// </summary>
  public class NullToVisibilityConverter : ValueConverterMarkupExtensionBase, IValueConverter
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
        throw new NotSupportedException("Only target type Visibility supported!");
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

      bool visible = value != null;

      if (invert)
      {
        visible = !visible;
      }

      return visible ? Visibility.Visible : Visibility.Collapsed;
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