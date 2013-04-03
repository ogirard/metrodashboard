//----------------------------------------------------------------------------------------------------
// <copyright file="IDeserializer.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;

using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Data
{
  /// <summary>
  ///   Interface for the little-endian deserializer.
  /// </summary>
  public interface IDeserializer
  {
    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    ushort ReadUInt16();

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    short ReadInt16();

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    uint ReadUInt32();

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    int ReadInt32();

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    long ReadInt64();

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    ulong ReadUInt64();

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    byte ReadByte();

    /// <summary>
    /// Reads a value.
    /// </summary>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <returns>
    /// The value.
    /// </returns>
    byte[] ReadBytes(int count);

    /// <summary>
    ///   Reads the GUID.
    /// </summary>
    /// <returns>The value.</returns>
    Guid ReadGuid();

    /// <summary>
    ///   Reads the boolean.
    /// </summary>
    /// <returns>The value.</returns>
    bool ReadBoolean();

    /// <summary>
    ///   Reads the nullable boolean.
    /// </summary>
    /// <returns>The value.</returns>
    bool? ReadNullableBoolean();

    /// <summary>
    /// Reads the string.
    /// </summary>
    /// <param name="encoding">
    /// The encoding.
    /// </param>
    /// <param name="prefixLength">
    /// Length of the prefix.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    string ReadString(StringEncoding encoding, int prefixLength);
  }
}