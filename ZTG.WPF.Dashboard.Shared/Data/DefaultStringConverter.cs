//----------------------------------------------------------------------------------------------------
// <copyright file="DefaultStringConverter.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security;
using System.Text;

using ZTG.WPF.Dashboard.Shared.Extensions;
using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Data
{
  /// <summary>
  ///   Static util class providing default string serialization / deserialization functionalities
  /// </summary>
  public static class DefaultStringConverter
  {
    /// <summary>
    /// The check value format.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", 
      Justification = "OK, that's only used in this context")]
    public enum CheckValueFormat
    {
      /// <summary />
      Hex, 

      /// <summary />
      Decimal
    }

    #region    Default Serialization

    /// <summary>
    /// Formats the given <paramref name="numberValue"/> as string and pads '0' on the left to reach the
    ///   <paramref name="desiredLength"/>
    ///   (where needed).
    /// </summary>
    /// <param name="numberValue">
    /// The number value to be formatted.
    /// </param>
    /// <param name="desiredLength">
    /// Desired minimal length of the formatted string (default is 0).
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatNumber(long? numberValue, int desiredLength = 0)
    {
      if (!numberValue.HasValue)
      {
        return string.Empty;
      }

      return Convert.ToString(numberValue, CultureInfo.InvariantCulture).PadLeft(desiredLength, '0');
    }

    /// <summary>
    /// Formats the given <paramref name="numberValue"/> as string and pads '0' on the left to reach the
    ///   <paramref name="desiredLength"/>
    ///   (where needed).
    /// </summary>
    /// <param name="numberValue">
    /// The number value to be formatted.
    /// </param>
    /// <param name="desiredLength">
    /// Desired minimal length of the formatted string (default is 0).
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatNumber(int? numberValue, int desiredLength = 0)
    {
      if (!numberValue.HasValue)
      {
        return string.Empty;
      }

      return Convert.ToString(numberValue, CultureInfo.InvariantCulture).PadLeft(desiredLength, '0');
    }

    /// <summary>
    /// Formats the given <paramref name="numberValue"/> as string and pads '0' on the left to reach the
    ///   <paramref name="desiredLength"/>
    ///   (where needed).
    /// </summary>
    /// <param name="numberValue">
    /// The number value to be formatted.
    /// </param>
    /// <param name="desiredLength">
    /// Desired minimal length of the formatted string (default is 0).
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatNumber(uint? numberValue, int desiredLength = 0)
    {
      return FormatNumber((int?)numberValue, desiredLength);
    }

    /// <summary>
    /// Formats the given <paramref name="numberValue"/> as string and pads '0' on the left to reach the
    ///   <paramref name="desiredLength"/>
    ///   (where needed).
    /// </summary>
    /// <param name="numberValue">
    /// The number value to be formatted.
    /// </param>
    /// <param name="desiredLength">
    /// Desired minimal length of the formatted string (default is 0).
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatNumber(short? numberValue, int desiredLength = 0)
    {
      return FormatNumber((int?)numberValue, desiredLength);
    }

    /// <summary>
    /// Formats the given <paramref name="numberValue"/> as string and pads '0' on the left to reach the
    ///   <paramref name="desiredLength"/>
    ///   (where needed).
    /// </summary>
    /// <param name="numberValue">
    /// The number value to be formatted.
    /// </param>
    /// <param name="desiredLength">
    /// Desired minimal length of the formatted string (default is 0).
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatNumber(ushort? numberValue, int desiredLength = 0)
    {
      return FormatNumber((int?)numberValue, desiredLength);
    }

    /// <summary>
    /// Formats the given <paramref name="numberValue"/> as hex string of minimum <paramref name="desiredLength"/>.
    /// </summary>
    /// <param name="numberValue">
    /// The number value to be formatted.
    /// </param>
    /// <param name="desiredLength">
    /// Desired minimal length of the formatted string (default is 0).
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatHexNumber(int? numberValue, int desiredLength = 0)
    {
      if (!numberValue.HasValue)
      {
        return string.Empty;
      }

      return "0x" + Convert.ToString(numberValue.Value, 16).PadLeft(desiredLength, '0');
    }

    /// <summary>
    /// Formats the given <paramref name="doubleValue"/> as string and rounds the value to <paramref name="roundDigits"/> (no rounding if set to -1).
    /// </summary>
    /// <param name="doubleValue">
    /// The double value to be formatted.
    /// </param>
    /// <param name="roundDigits">
    /// Number of digits to be used for rounding the given value, no rounding if set to -1 (default).
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatDouble(double? doubleValue, int roundDigits = -1)
    {
      if (!doubleValue.HasValue)
      {
        return string.Empty;
      }

      double valueToFormat = doubleValue.Value;
      if (roundDigits > -1)
      {
        valueToFormat = Math.Round(valueToFormat, roundDigits);
      }

      return Convert.ToString(valueToFormat, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Formats the given <paramref name="decimalValue"/> as string and rounds the value to <paramref name="roundDigits"/> (no rounding if set to -1).
    /// </summary>
    /// <param name="decimalValue">
    /// The decimal value to be formatted.
    /// </param>
    /// <param name="roundDigits">
    /// Number of digits to be used for rounding the given value, no rounding if set to -1 (default).
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatDecimal(decimal? decimalValue, int roundDigits = -1)
    {
      if (!decimalValue.HasValue)
      {
        return string.Empty;
      }

      decimal valueToFormat = decimalValue.Value;
      if (roundDigits > -1)
      {
        valueToFormat = Math.Round(valueToFormat, roundDigits);
      }

      return Convert.ToString(valueToFormat, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Formats the given <paramref name="boolValue"/> as string.
    /// </summary>
    /// <param name="boolValue">
    /// Boolean value to be formatted.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatBoolean(bool? boolValue)
    {
      if (!boolValue.HasValue)
      {
        return string.Empty;
      }

      return boolValue.Value ? "true" : "false";
    }

    /// <summary>
    /// Formats the given <paramref name="dateValue"/> as string, the default format is "dd.MM.yyyy".
    /// </summary>
    /// <param name="dateValue">
    /// The date to be formatted.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatDate(DateTime? dateValue, string format = "dd.MM.yyyy")
    {
      if (!dateValue.HasValue)
      {
        return string.Empty;
      }

      return dateValue.Value.ToString(format, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Formats the given <paramref name="dateTimeValue"/> as string, the default format is "dd/MM/yyyy HH:mm:ss".
    /// </summary>
    /// <param name="dateTimeValue">
    /// The date and time to be formatted.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatDateTime(DateTime? dateTimeValue, string format = "dd.MM.yyyy HH:mm:ss")
    {
      if (!dateTimeValue.HasValue)
      {
        return string.Empty;
      }

      return dateTimeValue.Value.ToString(format, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Formats the given <paramref name="idValue"/> as string.
    /// </summary>
    /// <param name="idValue">
    /// The id be formatted.
    /// </param>
    /// <param name="extractTypeId">
    /// If set to <c>true</c> the type id is extracted and displayed in the formatted string.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatGuid(Guid? idValue, bool extractTypeId = false)
    {
      if (!idValue.HasValue)
      {
        return string.Empty;
      }

      string idString = Convert.ToString(idValue.Value, CultureInfo.InvariantCulture).Trim('{', '}');
      if (extractTypeId)
      {
        // convert first 8 character (= 4 bytes as hex) to uint = typeid, if the given id is a TUID
        uint typeId = Convert.ToUInt32(idString.Substring(0, 8), 16);
        idString += string.Format(CultureInfo.InvariantCulture, " (TypeId = {0})", typeId);
      }

      return idString;
    }

    /// <summary>
    /// Formats the given <paramref name="enumValue"/> as string.
    /// </summary>
    /// <param name="enumValue">
    /// The enum value be formatted.
    /// </param>
    /// <param name="asNumberValue">
    /// If set to <c>true</c> the enum's number value is formatted string, otherwise, it's string representation is used as default format.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatEnum<TEnum>(TEnum enumValue, bool asNumberValue = false) where TEnum : struct
    {
      return Enum.Format(typeof(TEnum), enumValue, asNumberValue ? "d" : "f");
    }

    /// <summary>
    /// Formats the given <paramref name="value"/> as string.
    /// </summary>
    /// <param name="value">
    /// Any object value to be formatted using default to string conversion.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatObject(object value)
    {
      if (value == null)
      {
        return string.Empty;
      }

      return Convert.ToString(value, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Formats the given <paramref name="value"/> as string.
    /// </summary>
    /// <param name="value">
    /// Any object value to be formatted using default to string conversion.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatBinary(byte[] value)
    {
      if (value == null)
      {
        return string.Empty;
      }

      var binaryString = new StringBuilder();
      foreach (byte b in value)
      {
        binaryString.Append(b.ToString("X2", CultureInfo.InvariantCulture).ToUpperInvariant());
        binaryString.Append(" ");
      }

      return binaryString.ToString().TrimEnd();
    }

    /// <summary>
    /// Formats the given <paramref name="value"/> as base64 string.
    /// </summary>
    /// <param name="value">
    /// Any object value to be formatted using default to string conversion.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatBase64(byte[] value)
    {
      if (value == null)
      {
        return string.Empty;
      }

      return Convert.ToBase64String(value);
    }

    /// <summary>
    /// Formats the check value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatCheckValue(byte[] value, CheckValueFormat format = CheckValueFormat.Hex)
    {
      if (value == null)
      {
        return string.Empty;
      }

      if (format == CheckValueFormat.Decimal)
      {
        var decimalValue = BitConverter.ToInt16(value, 0).NetworkToHost();
        return FormatNumber(Math.Abs(decimalValue));
      }

      return BitConverter.ToString(value).Replace("-", " ");
    }

    #endregion Default Serialization

    #region    Default Deserialization

    /// <summary>
    /// Converts the given value to an <see cref="Int64"/>.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="long"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="value"/> is of wrong type
    /// </exception>
    public static long ConvertToBigNumber(string value)
    {
      value.ArgumentNotNull("value");

      try
      {
        return Convert.ToInt64(value, CultureInfo.InvariantCulture);
      }
      catch (FormatException ex)
      {
        throw new ArgumentException("Element value is not of desired type Int64", ex);
      }
    }

    /// <summary>
    /// Converts the given value to an <see cref="Int32"/>.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="value"/> is of wrong type
    /// </exception>
    public static int ConvertToNumber(string value)
    {
      value.ArgumentNotNull("value");

      try
      {
        return Convert.ToInt32(value, CultureInfo.InvariantCulture);
      }
      catch (FormatException ex)
      {
        throw new ArgumentException("Element value is not of desired type Int32", ex);
      }
    }

    /// <summary>
    /// Converts the given value to an <see cref="UInt32"/>.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="value"/> is of wrong type
    /// </exception>
    [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "unsigned", 
      Justification = "We want this name")]
    public static uint ConvertToUnsignedNumber(string value)
    {
      value.ArgumentNotNull("value");

      try
      {
        return Convert.ToUInt32(value, CultureInfo.InvariantCulture);
      }
      catch (FormatException ex)
      {
        throw new ArgumentException("Element value is not of desired type UInt32", ex);
      }
    }

    /// <summary>
    /// Converts the given hex value to an <see cref="Int32"/>.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="value"/> is of wrong type
    /// </exception>
    public static int ConvertToNumberFromHex(string value)
    {
      value.ArgumentNotNull("value");

      try
      {
        return Convert.ToInt32(value, 16);
      }
      catch (FormatException ex)
      {
        throw new ArgumentException("Element value is not of desired type Int32", ex);
      }
    }

    /// <summary>
    /// Converts the given value to an <see cref="Double"/>.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="double"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="value"/> is of wrong type
    /// </exception>
    public static double ConvertToDouble(string value)
    {
      value.ArgumentNotNull("value");

      try
      {
        return Convert.ToDouble(value, CultureInfo.InvariantCulture);
      }
      catch (FormatException ex)
      {
        throw new ArgumentException("Element value is not of desired type Double", ex);
      }
    }

    /// <summary>
    /// Converts the given value to an <see cref="Decimal"/>.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="decimal"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="value"/> is of wrong type
    /// </exception>
    public static decimal ConvertToDecimal(string value)
    {
      value.ArgumentNotNull("value");

      try
      {
        return Convert.ToDecimal(value, CultureInfo.InvariantCulture);
      }
      catch (FormatException ex)
      {
        throw new ArgumentException("Element value is not of desired type Double", ex);
      }
    }

    /// <summary>
    /// Converts the given value to an <see cref="Boolean"/>.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="value"/> is of wrong type
    /// </exception>
    public static bool ConvertToBoolean(string value)
    {
      value.ArgumentNotNull("value");

      try
      {
        return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
      }
      catch (FormatException ex)
      {
        throw new ArgumentException("Element value is not of desired type Boolean", ex);
      }
    }

    /// <summary>
    /// Converts the given value to an <see cref="DateTime"/>, default format is dd.MM.yyyy.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="value"/> is of wrong type
    /// </exception>
    public static DateTime ConvertToDate(string value, string format = "dd.MM.yyyy")
    {
      value.ArgumentNotNull("value");

      try
      {
        return DateTime.ParseExact(value, format, CultureInfo.InvariantCulture);
      }
      catch (FormatException ex)
      {
        throw new ArgumentException("Element value is not of desired type Date", ex);
      }
    }

    /// <summary>
    /// Converts the given value to an <see cref="DateTime"/>, default format is dd.MM.yyyy HH:mm:ss
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="value"/> is of wrong type
    /// </exception>
    public static DateTime ConvertToDateTime(string value, string format = "dd.MM.yyyy HH:mm:ss")
    {
      value.ArgumentNotNull("value");

      try
      {
        return DateTime.ParseExact(value, format, CultureInfo.InvariantCulture);
      }
      catch (FormatException ex)
      {
        throw new ArgumentException("Element value is not of desired type DateTime", ex);
      }
    }

    /// <summary>
    /// Converts the given value to an <see cref="Guid"/>.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="value"/> is of wrong type
    /// </exception>
    public static Guid ConvertToGuid(string value)
    {
      value.ArgumentNotNull("value");

      try
      {
        return Guid.Parse(value);
      }
      catch (FormatException ex)
      {
        throw new ArgumentException("Element value is not of desired type Guid", ex);
      }
    }

    /// <summary>
    /// Converts the given value to an <see cref="TEnum"/>.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="TEnum"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="value"/> is of wrong type
    /// </exception>
    public static TEnum ConvertToEnum<TEnum>(string value) where TEnum : struct
    {
      value.ArgumentNotNull("value");

      try
      {
        return (TEnum)Enum.Parse(typeof(TEnum), value);
      }
      catch (ArgumentException ex)
      {
        throw new ArgumentException(
          string.Format(
            CultureInfo.InvariantCulture, 
            "Element value is not of desired type '{0}' or type is invalid", 
            typeof(TEnum).Name), 
          ex);
      }
    }

    /// <summary>
    /// Converts the given value to a byte array (from HEX).
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The byte array
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="value"/> is of wrong type
    /// </exception>
    public static byte[] ConvertToBinary(string value)
    {
      value.ArgumentNotNull();

      // replace all spaces (not relevant) and trailing 0x
      value = value.Replace(" ", string.Empty);
      if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
      {
        value = value.Substring(2);
      }

      if (value.Length % 2 != 0)
      {
        throw new ArgumentException("Length of value must be even.");
      }

      var byteList = new List<byte>();

      for (int index = 0; index < value.Length; index += 2)
      {
        var singleByte =
          (byte)int.Parse(value.Substring(index, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        byteList.Add(singleByte);
      }

      return byteList.ToArray();
    }

    /// <summary>
    /// Converts the given value to a byte array.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The byte array
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="value"/> is of wrong type
    /// </exception>
    public static byte[] ConvertToBinaryBase64(string value)
    {
      value.ArgumentNotNull("value");

      try
      {
        return Convert.FromBase64String(value);
      }
      catch (FormatException ex)
      {
        throw new ArgumentException("Element value is not of desired type base64 string", ex);
      }
    }

    /// <summary>
    /// Converts the given value string to a secure string.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The secure string or <tt>null</tt> if the value is null or empty
    /// </returns>
    [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", 
      Justification = "Cannot be disposed here!")]
    public static SecureString ConvertToSecureString(string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return null;
      }

      var secureString = new SecureString();
      char[] chars = value.ToCharArray();
      foreach (char c in chars)
      {
        secureString.AppendChar(c);
      }

      return secureString;
    }

    #endregion Default Deserialization
  }
}