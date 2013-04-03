//----------------------------------------------------------------------------------------------------
// <copyright file="SerializerBase.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;

using ZTG.WPF.Dashboard.Shared.Extensions;
using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Data
{
  /// <summary>
  /// The serializer base.
  /// </summary>
  public abstract class SerializerBase : ISerializer
  {
    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    [SuppressMessage("CAG Code Analysis Rules", "CAG1000:GuidToByteArrayForbidden", 
      Justification = "Needed here as a base case.")]
    public void Write(Guid value)
    {
      byte[] bytes = value.ToByteArray();
      Write(BitConverter.ToInt32(bytes, 0));
      Write(BitConverter.ToInt16(bytes, 4));
      Write(BitConverter.ToInt16(bytes, 6));
      Write(bytes.Part(8, 8));
    }

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="encoding">
    /// The encoding.
    /// </param>
    /// <param name="prefixLength">
    /// Length of the prefix. May be 0 for zero termination, 1, 2 or 4.
    /// </param>
    public void Write(string value, StringEncoding encoding, int prefixLength)
    {
      value.ArgumentNotNull("value");
      var bytes = ByteConverter.FromString(value, encoding);

      switch (prefixLength)
      {
        case 0:
          Write(bytes);
          Write((byte)0);
          break;
        case 1:
          if (bytes.Length > byte.MaxValue)
          {
            throw new ArgumentException("String too long for prefix length.");
          }

          Write((byte)bytes.Length);
          Write(bytes);
          break;
        case 2:
          if (bytes.Length > ushort.MaxValue)
          {
            throw new ArgumentException("String too long for prefix length.");
          }

          Write((ushort)bytes.Length);
          Write(bytes);
          break;
        case 4:
          Write((uint)bytes.Length);
          Write(bytes);
          break;
        default:
          throw new ArgumentException("Bad prefix length");
      }
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public void Write(bool value)
    {
      Write((byte)(value ? 1 : 0));
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public void Write(bool? value)
    {
      Write((byte)(value.HasValue ? (value.Value ? 1 : 0) : 2));
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public abstract void Write(ushort value);

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public abstract void Write(short value);

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public abstract void Write(uint value);

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public abstract void Write(int value);

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public abstract void Write(ulong value);

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public abstract void Write(long value);

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public abstract void Write(byte value);

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public abstract void Write(byte[] value);

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="prefixLength">
    /// Length of the prefix. May be 0 for no prefix, 1, 2 or 4.
    /// </param>
    public void Write(Guid value, int prefixLength)
    {
      switch (prefixLength)
      {
        case 0:
          break;
        case 1:
          Write((byte)16);
          break;
        case 2:
          Write((ushort)16);
          break;
        case 4:
          Write((uint)16);
          break;
        default:
          throw new ArgumentException("Bad prefix length.");
      }

      Write(value);
    }
  }
}