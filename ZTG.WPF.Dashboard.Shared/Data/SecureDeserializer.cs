//----------------------------------------------------------------------------------------------------
// <copyright file="SecureDeserializer.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using ZTG.WPF.Dashboard.Shared.Extensions;
using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Data
{
  /// <summary>
  ///   Deserializes data from Little-Endian format.
  /// </summary>
  [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Deserializer", Justification = "That's ok!")]
  public class SecureDeserializer : IDisposable
  {
    /// <summary>
    /// The _data.
    /// </summary>
    private readonly byte[] _data;

    /// <summary>
    /// The _handle.
    /// </summary>
    private readonly GCHandle _handle;

    /// <summary>
    /// The _position.
    /// </summary>
    private int _position;

    /// <summary>
    /// Initializes a new instance of the <see cref="SecureDeserializer"/> class.
    /// </summary>
    /// <param name="data">
    /// The data.
    /// </param>
    public SecureDeserializer(SecureByteArray data)
    {
      data.ArgumentNotNull();

      _data = new byte[data.Length];
      _handle = GCHandle.Alloc(_data, GCHandleType.Pinned);
      Buffer.BlockCopy(data.Data, 0, _data, 0, data.Length);
      _position = 0;
    }

    /// <summary>
    ///   Gets the remaining byte count.
    /// </summary>
    /// <value>The remaining byte count.</value>
    public long RemainingByteCount
    {
      get
      {
        return _data.Length - _position;
      }
    }

    /// <summary>
    ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources
    /// </summary>
    /// <param name="disposing">
    /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
      if (_handle.IsAllocated)
      {
        _data.Wipe();
        _handle.Free();
      }
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public ushort ReadUInt16()
    {
      var value = (ushort)((uint)_data[_position] << 8 | _data[_position + 1]);
      _position += 2;
      return value;
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public short ReadInt16()
    {
      var value = (short)((uint)_data[_position] << 8 | _data[_position + 1]);
      _position += 2;
      return value;
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public uint ReadUInt32()
    {
      uint value = (uint)_data[_position] << 24 | (uint)_data[_position + 1] << 16 | (uint)_data[_position + 2] << 8
                   | _data[_position + 3];
      _position += 4;
      return value;
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public int ReadInt32()
    {
      int value = _data[_position] << 24 | _data[_position + 1] << 16 | _data[_position + 2] << 8 | _data[_position + 3];
      _position += 4;
      return value;
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public byte ReadByte()
    {
      byte value = _data[_position];
      _position++;
      return value;
    }

    /// <summary>
    /// Reads a value.
    /// </summary>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <returns>
    /// The value.
    /// </returns>
    /// <remarks>
    /// Caller needs to care about disposing!
    /// </remarks>
    [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope",
      Justification = "Return but not dispose.")]
    public SecureByteArray ReadBytes(int count)
    {
      var value = new SecureByteArray(count);
      Buffer.BlockCopy(_data, _position, value.Data, 0, count);
      _position += count;
      return value;
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public ushort PeekUInt16()
    {
      return (ushort)((uint)_data[_position] << 8 | _data[_position + 1]);
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public short PeekInt16()
    {
      return (short)((uint)_data[_position] << 8 | _data[_position + 1]);
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public uint PeekUInt32()
    {
      return (uint)_data[_position] << 24 | (uint)_data[_position + 1] << 16 | (uint)_data[_position + 2] << 8
             | _data[_position + 3];
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public int PeekInt32()
    {
      return _data[_position] << 24 | _data[_position + 1] << 16 | _data[_position + 2] << 8 | _data[_position + 3];
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public byte PeekByte()
    {
      return _data[_position];
    }

    /// <summary>
    /// Reads a value.
    /// </summary>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <returns>
    /// The value.
    /// </returns>
    /// <remarks>
    /// Caller needs to care about disposing!
    /// </remarks>
    [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope",
      Justification = "Return but not dispose.")]
    public SecureByteArray PeekBytes(int count)
    {
      var value = new SecureByteArray(count);
      Buffer.BlockCopy(_data, _position, value.Data, 0, count);
      return value;
    }
  }
}