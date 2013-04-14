// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DynamicObjectToTranslationConverter.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows.Data;

using ZTG.WPF.Dashboard.Shared.Extensions;

namespace ZTG.WPF.Dashboard.Shared.Localization.WPF.Internal
{
  /// <summary>
  /// Multi Value Converter used to dynamically provide a a translated value based on the value of a binding.
  /// </summary>
  public class DynamicObjectToTranslationConverter : IMultiValueConverter
  {
    #region Private Members

    private static readonly Type StringType = typeof(string);

    #endregion

    #region IMultiValueConverter Members

    /// <summary>
    /// See <see cref="IMultiValueConverter.Convert"/>
    /// </summary>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      object translatedKey = null;

      if (values != null)
      {
        object keyObject = (values.Length > 0) ? values[0] : null;

        // Manual bool conversion for independency from framework language (e.g. true = "Wahr")
        if (keyObject != null && keyObject is bool)
        {
          if ((bool)keyObject)
          {
            keyObject = "True";
          }
          else
          {
            keyObject = "False";
          }
        }

        string keyNamespace = (values.Length > 1) ? (values[1] as string) : null;
        IValueConverter keyConverter = (values.Length > 2) ? (values[2] as IValueConverter) : null;

        string keyString = null;
        if (keyNamespace != null)
        {
          keyString = "{0}{1}".FormatText(keyNamespace, keyObject);
        }
        else if (keyConverter != null)
        {
          object convertedValue = keyConverter.Convert(keyObject, StringType, null, culture);
          if (convertedValue != null)
          {
            if (convertedValue == Binding.DoNothing)
            {
              // In case the converter tells me to do nothing, I do nothing ;-)
              return keyObject;
            }

            keyString = convertedValue.ToString();
          }
        }
        else
        {
          keyString = keyObject != null ? keyObject.ToString() : null;
        }

        var keyStringFormatter = keyConverter as ITranslatedStringFormatter;

        translatedKey = TranslationManager.Translate(keyString);
        if (keyStringFormatter != null)
        {
          return keyStringFormatter.FormatString(keyObject,
                                                 translatedKey != null ? translatedKey.ToString() : string.Empty,
                                                 culture);
        }
      }

      return translatedKey;
    }

    /// <summary>
    /// See <see cref="IMultiValueConverter.ConvertBack"/>
    /// </summary>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
