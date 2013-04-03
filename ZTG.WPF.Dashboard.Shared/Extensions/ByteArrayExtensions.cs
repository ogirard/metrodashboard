//----------------------------------------------------------------------------------------------------
// <copyright file="ByteArrayExtensions.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using ZTG.WPF.Dashboard.Shared.Data;
using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Extensions
{
  /// <summary>
  ///   Extension methods for <see cref="ByteArrayExtensions" />.
  /// </summary>
  public static class ByteArrayExtensions
  {
    /// <summary>
    /// Wipes the specified byte array.
    /// </summary>
    /// <param name="byteArray">
    /// The byte array.
    /// </param>
    public static void Wipe(this byte[] byteArray)
    {
      if (byteArray != null)
      {
        var random = new Random((int)DateTime.Now.Ticks);
        random.NextBytes(byteArray);

        for (int i = 0; i < byteArray.Length; i++)
        {
          byteArray[i] = 0;
        }
      }
    }

    /// <summary>
    /// Converts a byte array to a nullable byte array.
    /// </summary>
    /// <param name="byteArray">
    /// The byte array.
    /// </param>
    /// <param name="wipeSource">
    /// if set to <c>true</c> wipes source byte array.
    /// </param>
    /// <returns>
    /// The byte? array
    /// </returns>
    public static byte?[] Convert(this byte[] byteArray, bool wipeSource)
    {
      byte?[] nullableByteArray = null;

      if (byteArray != null)
      {
        nullableByteArray = Array.ConvertAll(byteArray, b => (byte?)b);

        if (wipeSource)
        {
          byteArray.Wipe();
        }
      }

      return nullableByteArray;
    }

    /// <summary>
    /// Wipes the specified nullable byte array.
    /// </summary>
    /// <param name="nullableByteArray">
    /// The nullable byte array.
    /// </param>
    public static void Wipe(this byte?[] nullableByteArray)
    {
      if (nullableByteArray != null)
      {
        for (int i = 0; i < nullableByteArray.Length; i++)
        {
          nullableByteArray[i] = null;
        }
      }
    }

    /// <summary>
    /// Converts the specified nullable byte array.
    /// </summary>
    /// <param name="nullableByteArray">
    /// The nullable byte array.
    /// </param>
    /// <param name="wipeSource">
    /// if set to <c>true</c> wipes source nullable byte array.
    /// </param>
    /// <returns>
    /// The byte array
    /// </returns>
    public static byte[] Convert(this byte?[] nullableByteArray, bool wipeSource)
    {
      byte[] byteArray = null;

      if (nullableByteArray != null)
      {
        if (nullableByteArray.All(b => b.HasValue))
        {
          byteArray = Array.ConvertAll(nullableByteArray, b => b ?? 0);
        }

        if (wipeSource)
        {
          nullableByteArray.Wipe();
        }
      }

      return byteArray;
    }

    /// <summary>
    /// Are both byte arrays equal?
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="other">
    /// The other.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool AreEqual(this byte[] value, byte[] other)
    {
      value.ArgumentNotNull("value");
      other.ArgumentNotNull("other");

      if (value.Length == other.Length)
      {
        for (int index = 0; index < value.Length; index++)
        {
          if (value[index] != other[index])
          {
            return false;
          }
        }

        return true;
      }

      return false;
    }

    /// <summary>
    /// Toes the hex string.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="space">
    /// The space.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ToHexString(this byte[] value, string space = null)
    {
      value.ArgumentNotNull("value");

      return string.Join(
        space ?? string.Empty, value.Select(v => string.Format(CultureInfo.InvariantCulture, "{0:X2}", v)).ToArray());
    }

    /// <summary>
    /// Xors the add.
    /// </summary>
    /// <param name="subject">
    /// The subject.
    /// </param>
    /// <param name="addendum">
    /// The addendum.
    /// </param>
    /// <param name="addendumOffset">
    /// The addendum offset.
    /// </param>
    public static void XorAdd(this byte[] subject, byte[] addendum, int addendumOffset)
    {
      subject.ArgumentNotNull("subject");

      subject.XorAdd(0, addendum, addendumOffset, subject.Length);
    }

    /// <summary>
    /// Xors the add.
    /// </summary>
    /// <param name="subject">
    /// The subject.
    /// </param>
    /// <param name="subjectOffset">
    /// The subject offset.
    /// </param>
    /// <param name="addendum">
    /// The addendum.
    /// </param>
    /// <param name="addendumOffset">
    /// The addendum offset.
    /// </param>
    /// <param name="length">
    /// The length.
    /// </param>
    public static void XorAdd(this byte[] subject, int subjectOffset, byte[] addendum, int addendumOffset, int length)
    {
      subject.ArgumentNotNull("subject");
      addendum.ArgumentNotNull("addendum");

      if (subjectOffset + length > subject.Length)
      {
        throw new ArgumentOutOfRangeException("subjectOffset");
      }

      if (addendumOffset + length > addendum.Length)
      {
        throw new ArgumentOutOfRangeException("addendumOffset");
      }

      for (int index = 0; index < length; index++)
      {
        subject[index + subjectOffset] ^= addendum[index + addendumOffset];
      }
    }

    /// <summary>
    /// Concats the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="other">
    /// The other.
    /// </param>
    /// <returns>
    /// The byte array
    /// </returns>
    public static byte[] Concat(this byte[] value, byte[] other)
    {
      value.ArgumentNotNull("value");
      other.ArgumentNotNull("other");

      var buffer = new byte[value.Length + other.Length];
      Buffer.BlockCopy(value, 0, buffer, 0, value.Length);
      Buffer.BlockCopy(other, 0, buffer, value.Length, other.Length);
      return buffer;
    }

    /// <summary>
    /// Truncates the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="length">
    /// The length.
    /// </param>
    /// <returns>
    /// The byte array
    /// </returns>
    public static byte[] Truncate(this byte[] value, int length)
    {
      value.ArgumentNotNull("value");

      var buffer = new byte[length];
      Buffer.BlockCopy(value, 0, buffer, 0, length);
      return buffer;
    }

    /// <summary>
    /// Inserts the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="offset">
    /// The offset.
    /// </param>
    /// <param name="insertData">
    /// The inserted data.
    /// </param>
    public static void Insert(this byte[] value, int offset, byte[] insertData)
    {
      value.ArgumentNotNull("value");
      insertData.ArgumentNotNull("insertData");

      if (offset + insertData.Length > value.Length)
      {
        throw new ArgumentOutOfRangeException("offset");
      }

      for (int index = 0; index < insertData.Length; index++)
      {
        value[offset + index] = insertData[index];
      }
    }

    /// <summary>
    /// Pads to length.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="paddingValue">
    /// The padding value.
    /// </param>
    /// <param name="totalLength">
    /// The total length.
    /// </param>
    /// <returns>
    /// The byte array
    /// </returns>
    public static byte[] PadToLength(this byte[] value, byte paddingValue, int totalLength)
    {
      value.ArgumentNotNull("value");

      if (totalLength < value.Length)
      {
        throw new ArgumentOutOfRangeException("totalLength");
      }

      var buffer = new byte[totalLength];
      Buffer.BlockCopy(value, 0, buffer, 0, value.Length);

      for (int index = value.Length; index < buffer.Length; index++)
      {
        buffer[index] = paddingValue;
      }

      return buffer;
    }

    /// <summary>
    /// Pads to length if needed and saves the length of <see cref="value"/> to the first byte if
    ///   <paramref name="serializeLength"/>
    ///   is <tt>true</tt>.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="paddingValue">
    /// The padding value.
    /// </param>
    /// <param name="valueLengthMultiplier">
    /// The value lenght multiplier.
    /// </param>
    /// <param name="serializeLength">
    /// if set to <tt>true</tt> the length is serialized.
    /// </param>
    /// <returns>
    /// Padded byte array
    /// </returns>
    /// <remarks>
    /// empty byte arrays are returned as empty byte arrays, padding is skipped in this case
    /// </remarks>
    public static byte[] AddPadding(
      this byte[] value, byte paddingValue, int valueLengthMultiplier, bool serializeLength = false)
    {
      value.ArgumentNotNull("value");

      if (value.Length == 0)
      {
        return new byte[0];
      }

      byte[] sizedByteArray;

      using (var serializer = new Serializer())
      {
        if (serializeLength)
        {
          serializer.Write((byte)value.Length);
        }

        serializer.Write(value);
        sizedByteArray = serializer.Data;
      }

      int finalLength = (sizedByteArray.Length + valueLengthMultiplier - 1) / valueLengthMultiplier
                        * valueLengthMultiplier;
      return sizedByteArray.PadToLength(paddingValue, sizedByteArray.Length + finalLength - sizedByteArray.Length);
    }

    /// <summary>
    /// Removes padding.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The byte array
    /// </returns>
    public static byte[] RemovePadding(this byte[] value)
    {
      value.ArgumentNotNull("value");

      if (value.Length <= 1)
      {
        return new byte[] { };
      }

      return value.Part(1, value[0]);
    }

    /// <summary>
    /// Copies the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The byte array
    /// </returns>
    public static byte[] Copy(this byte[] value)
    {
      value.ArgumentNotNull("value");

      var buffer = new byte[value.Length];
      Buffer.BlockCopy(value, 0, buffer, 0, value.Length);
      return buffer;
    }

    /// <summary>
    /// Parts the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="offset">
    /// The offset.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <returns>
    /// The byte array
    /// </returns>
    public static byte[] Part(this byte[] value, int offset, int count)
    {
      value.ArgumentNotNull("value");

      if (offset + count > value.Length)
      {
        throw new ArgumentOutOfRangeException("offset");
      }

      var buffer = new byte[count];
      Buffer.BlockCopy(value, offset, buffer, 0, count);
      return buffer;
    }

    /// <summary>
    /// Parts the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="offset">
    /// The offset.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <returns>
    /// The byte array
    /// </returns>
    public static byte[] Part(this byte[] value, long offset, long count)
    {
      value.ArgumentNotNull("value");

      if (offset + count > value.LongLength)
      {
        throw new ArgumentOutOfRangeException("offset");
      }

      var buffer = new byte[count];
      BlockCopy(value, offset, buffer, 0, count);
      return buffer;
    }

    /// <summary>
    /// Copyies a block of memory. Support 64 bit offset and count.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="sourceOffset">
    /// The source offset.
    /// </param>
    /// <param name="destination">
    /// The destination.
    /// </param>
    /// <param name="destinationOffset">
    /// The destination offset.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    public static unsafe void BlockCopy(
      byte[] source, long sourceOffset, byte[] destination, long destinationOffset, long count)
    {
      fixed (byte* sourceFixedPointer = source)
      {
        fixed (byte* destFixedPointer = destination)
        {
          byte* sourcePointer = sourceFixedPointer + sourceOffset;
          byte* destPointer = destFixedPointer + destinationOffset;

          var sourceLongPointer = (long*)sourcePointer;
          var destLongPointer = (long*)destPointer;

          for (; count >= sizeof(long); count -= sizeof(long))
          {
            *destLongPointer = *sourceLongPointer;
            sourceLongPointer++;
            destLongPointer++;
          }

          sourcePointer = (byte*)sourceLongPointer;
          destPointer = (byte*)destLongPointer;

          for (; count >= 1; count--)
          {
            *destPointer = *sourcePointer;
            sourcePointer++;
            destPointer++;
          }
        }
      }
    }

    /// <summary>
    /// Parses the hex bytes.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// Byte Array.
    /// </returns>
    public static byte[] ParseHexBytes(this string value)
    {
      value.ArgumentNotNull("value");

      var byteList = new List<byte>();

      value = value.Trim();

      for (int index = value.Length - 2; index >= -1; index -= 2)
      {
        string singleByteString = value.Substring(Math.Max(0, index), index >= 0 ? 2 : 1);

        byteList.Insert(0, (byte)int.Parse(singleByteString, NumberStyles.HexNumber, CultureInfo.InvariantCulture));
      }

      return byteList.ToArray();
    }

    /// <summary>
    /// Calculates the hash code.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// A hash code.
    /// </returns>
    public static int CalculateHashCode(this byte[] value)
    {
      value.ArgumentNotNull("value");

      var buffer = new byte[((value.Length / sizeof(int)) * sizeof(int)) + sizeof(int)];
      Buffer.BlockCopy(value, 0, buffer, 0, value.Length);
      int hashCode = 0;

      for (int index = 0; index < buffer.Length; index += sizeof(int))
      {
        hashCode ^= BitConverter.ToInt32(buffer, index);
      }

      return hashCode;
    }
  }
}