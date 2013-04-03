//----------------------------------------------------------------------------------------------------
// <copyright file="ISerializer.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;

using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Data
{
  /// <summary>
  ///   Interface for the little-endian serializer.
  /// </summary>
  public interface ISerializer
  {
    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    void Write(ushort value);

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    void Write(short value);

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    void Write(uint value);

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    void Write(int value);

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    void Write(ulong value);

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    void Write(long value);

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    void Write(byte value);

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    void Write(byte[] value);

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    void Write(Guid value);

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// if set to <c>true</c> [value].
    /// </param>
    void Write(bool value);

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    void Write(bool? value);

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
    void Write(string value, StringEncoding encoding, int prefixLength);
  }
}