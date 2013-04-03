//----------------------------------------------------------------------------------------------------
// <copyright file="ByteConverter.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Text;

using ZTG.WPF.Dashboard.Shared.Data;

namespace ZTG.WPF.Dashboard.Shared.Utilities
{
  /// <summary>
  ///   The supported encoding to onvert a string to a byte[]
  /// </summary>
  public enum StringEncoding
  {
    /// <summary />
    UsAscii,

    /// <summary />
    CodePage1252,

    /// <summary />
    Utf16,
  }

  /// <summary>
  ///   Converter that converts different objects to a byte array.
  /// </summary>
  public static class ByteConverter
  {
    /// <summary>
    /// Serializes an IP Address to a byte array with length 4.
    /// </summary>
    /// <param name="ipAddress">
    /// The ip address.
    /// </param>
    /// <returns>
    /// The serialized IP address
    /// </returns>
    public static byte[] FromIPAddress(string ipAddress)
    {
      ipAddress.ArgumentNotNull("ipAddress");

      string[] parts = ipAddress.Split('.');
      if (parts.Length != 4)
      {
        throw new ArgumentException("Not a valid IP Address");
      }

      uint address = 0;
      int shift = 24;
      foreach (string part in parts)
      {
        address += uint.Parse(part, CultureInfo.InvariantCulture) << shift;
        shift -= 8;
      }

      // convert the uint from big endian to little endian
      using (var serializer = new Serializer())
      {
        serializer.Write(address);
        return serializer.Data;
      }
    }

    /// <summary>
    /// Serializes an String to a byte array and uses the CodePage 1252 encoding.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The byte array.
    /// </returns>
    public static byte[] FromString(string value)
    {
      return FromString(value, StringEncoding.CodePage1252);
    }

    /// <summary>
    /// Serializes an String to a byte array with the given encoding.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="encoding">
    /// The encoding to be used.
    /// </param>
    /// <returns>
    /// The byte array.
    /// </returns>
    public static byte[] FromString(string value, StringEncoding encoding)
    {
      return string.IsNullOrEmpty(value) ? new byte[0] : GetEncoding(encoding).GetBytes(value);
    }

    /// <summary>
    /// Returns a <see cref="System.String"/> that represents this instance.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// A <see cref="System.String"/> that represents this instance.
    /// </returns>
    public static string ToString(byte[] value)
    {
      return ToString(value, StringEncoding.CodePage1252);
    }

    /// <summary>
    /// Returns a <see cref="System.String"/> that represents this instance.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="encoding">
    /// The encoding.
    /// </param>
    /// <returns>
    /// A <see cref="System.String"/> that represents this instance.
    /// </returns>
    public static string ToString(byte[] value, StringEncoding encoding)
    {
      return GetEncoding(encoding).GetString(value);
    }

    /// <summary>
    /// Serializes an String to a byte array
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="returnLength">
    /// Length of the return byte array including delimiter.length
    /// </param>
    /// <param name="addDelimiter">
    /// if set to <c>true</c> adds the delimiter '\0'.
    /// </param>
    /// <returns>
    /// The byte array.
    /// </returns>
    public static byte[] FromString(string value, int returnLength, bool addDelimiter = true)
    {
      return FromString(value, returnLength, StringEncoding.CodePage1252, addDelimiter);
    }

    /// <summary>
    /// Serializes an String to a byte array with the given encoding.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="returnLength">
    /// Length of the return byte array including delimiter.length
    /// </param>
    /// <param name="encoding">
    /// The encoding to be used.
    /// </param>
    /// <param name="addDelimiter">
    /// if set to <c>true</c> adds the delimiter '\0'.
    /// </param>
    /// <returns>
    /// The byte array.
    /// </returns>
    public static byte[] FromString(string value, int returnLength, StringEncoding encoding, bool addDelimiter = true)
    {
      int realStringLength = returnLength - (addDelimiter ? 1 : 0);
      byte[] convertedString = FromString(value, encoding);

      // create the byte array of desired length and copy the string into it.
      var returnValue = new byte[returnLength];
      Array.Copy(
        convertedString,
        returnValue,
        convertedString.Length > realStringLength ? realStringLength : convertedString.Length);

      return returnValue;
    }

    /// <summary>
    /// Converts the available enconding to the real encoding object.
    /// </summary>
    /// <param name="encoding">
    /// The encoding.
    /// </param>
    /// <returns>
    /// The <see cref="Encoding"/>.
    /// </returns>
    private static Encoding GetEncoding(StringEncoding encoding)
    {
      switch (encoding)
      {
        case StringEncoding.UsAscii:
          return Encoding.GetEncoding("us-ascii");
        case StringEncoding.CodePage1252:
          return Encoding.GetEncoding(1252);
        case StringEncoding.Utf16:
          return Encoding.Unicode;
        default:
          throw new ArgumentOutOfRangeException("encoding");
      }
    }

    /// <summary>
    /// Serializes a timeZoneOffset froms hours to seconds and then to unsigned integer and then to a byte array with length 4.
    /// </summary>
    /// <param name="hours">
    /// The time zone offset.
    /// </param>
    /// <returns>
    /// The byte array.
    /// </returns>
    public static byte[] FromTimeZoneOffset(decimal hours)
    {
      using (var serializer = new Serializer())
      {
        serializer.Write(Convert.ToInt32(hours * 3600));
        return serializer.Data;
      }
    }

    /// <summary>
    /// Serializes a bool to a byte array with length 4.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The byte array.
    /// </returns>
    public static byte[] FromBool(bool value)
    {
      using (var serializer = new Serializer())
      {
        serializer.Write(Convert.ToInt32(value));
        return serializer.Data;
      }
    }

    /// <summary>
    /// Serializes a <see cref="DateTime"/> to a byte array with length 4.
    /// </summary>
    /// <param name="dateTime">
    /// The date time.
    /// </param>
    /// <returns>
    /// The byte array.
    /// </returns>
    public static byte[] FromDateTime(DateTime dateTime)
    {
      using (var serializer = new Serializer())
      {
        serializer.Write(Convert.ToInt32(ConvertToUnixTime(dateTime)));
        return serializer.Data;
      }
    }

    /// <summary>
    /// Converts a <see cref="DateTime"/> to UnixTime
    /// </summary>
    /// <param name="dateTime">
    /// The date time.
    /// </param>
    /// <returns>
    /// The <see cref="double"/>.
    /// </returns>
    public static double ConvertToUnixTime(DateTime dateTime)
    {
      return ConvertToUnixTimeSpan(dateTime).TotalSeconds;
    }

    public static DateTime UnixBirthDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

    /// <summary>
    /// Converts a <see cref="DateTime"/> to UnixTime
    /// </summary>
    /// <param name="dateTime">
    /// The date time.
    /// </param>
    /// <returns>
    /// The <see cref="TimeSpan"/>.
    /// </returns>
    public static TimeSpan ConvertToUnixTimeSpan(DateTime dateTime)
    {
      return dateTime - UnixBirthDateTime;
    }
  }
}