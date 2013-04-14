//----------------------------------------------------------------------------------------------------
// <copyright file="DeserializerBase.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Data
{
  /// <summary>
  /// The deserializer base.
  /// </summary>
  public abstract class DeserializerBase : IDeserializer
  {
    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public abstract ushort ReadUInt16();

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public abstract short ReadInt16();

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public abstract uint ReadUInt32();

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public abstract int ReadInt32();

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public abstract long ReadInt64();

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public abstract ulong ReadUInt64();

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public abstract byte ReadByte();

    /// <summary>
    /// Reads a value.
    /// </summary>
    /// <param name="count">
    /// </param>
    /// <returns>
    /// The value.
    /// </returns>
    public abstract byte[] ReadBytes(int count);

    /// <summary>
    ///   Reads the GUID.
    /// </summary>
    /// <returns>The value.</returns>
    public Guid ReadGuid()
    {
      return new Guid(ReadInt32(), ReadInt16(), ReadInt16(), ReadBytes(8));
    }

    /// <summary>
    /// Reads the string.
    /// </summary>
    /// <param name="encoding">
    /// The encoding.
    /// </param>
    /// <param name="prefixLength">
    /// Length of the prefix. May be 0 for zero termination 1, 2 or 4.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string ReadString(StringEncoding encoding, int prefixLength)
    {
      switch (prefixLength)
      {
        case 0:
          return ByteConverter.ToString(ReadZeroTerminatedBytes(), encoding);
        case 1:
          return ByteConverter.ToString(ReadBytes(ReadByte()), encoding);
        case 2:
          return ByteConverter.ToString(ReadBytes(ReadUInt16()), encoding);
        case 4:
          return ByteConverter.ToString(ReadBytes((int)ReadUInt32()), encoding);
        default:
          throw new ArgumentException("Invalid prefix length.");
      }
    }

    /// <summary>
    ///   Reads the boolean.
    /// </summary>
    /// <returns>The value.</returns>
    public bool ReadBoolean()
    {
      switch (ReadByte())
      {
        case 1:
          return true;
        case 0:
          return false;
        default:
          throw new InvalidOperationException("Boolean out of range.");
      }
    }

    /// <summary>
    ///   Reads the nullable boolean.
    /// </summary>
    /// <returns>The value.</returns>
    public bool? ReadNullableBoolean()
    {
      switch (ReadByte())
      {
        case 1:
          return true;
        case 0:
          return false;
        case 2:
          return null;
        default:
          throw new InvalidOperationException("Boolean out of range.");
      }
    }

    /// <summary>
    /// Reads the zero terminated bytes.
    /// </summary>
    /// <returns>
    /// The byte array
    /// </returns>
    private byte[] ReadZeroTerminatedBytes()
    {
      var bytes = new List<byte>();

      while (true)
      {
        byte b = ReadByte();

        if (b == 0)
        {
          return bytes.ToArray();
        }

        bytes.Add(b);
      }
    }
  }
}