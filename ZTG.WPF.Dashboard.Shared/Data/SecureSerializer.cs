//----------------------------------------------------------------------------------------------------
// <copyright file="SecureSerializer.cs" company="Zühlke Engineering AG">
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
  ///   Serializes data in Little-Endian format.
  /// </summary>
  public class SecureSerializer : IDisposable
  {
    /// <summary>
    /// The data size.
    /// </summary>
    private const int DataSize = 64;

    /// <summary>
    /// The _data.
    /// </summary>
    private byte[] _data;

    /// <summary>
    /// The _handle.
    /// </summary>
    private GCHandle _handle;

    /// <summary>
    /// The _length.
    /// </summary>
    private int _length;

    /// <summary>
    ///   Initializes a new instance of the <see cref="SecureSerializer" /> class.
    /// </summary>
    public SecureSerializer()
    {
      _data = new byte[DataSize];
      _handle = GCHandle.Alloc(_data, GCHandleType.Pinned);
    }

    /// <summary>
    ///   Gets the data.
    /// </summary>
    /// <value>The data.</value>
    [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope",
      Justification = "Return not dispose.")]
    public SecureByteArray Data
    {
      get
      {
        var data = new SecureByteArray(_length);
        Buffer.BlockCopy(_data, 0, data.Data, 0, _length);
        return data;
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

      _data = null;
    }

    /// <summary>
    /// Allocs the specified size.
    /// </summary>
    /// <param name="size">
    /// The size.
    /// </param>
    private void Alloc(int size)
    {
      if (_length + size > _data.Length)
      {
        int newLength = Math.Max(_data.Length + DataSize, _data.Length + size);
        var newData = new byte[newLength];
        GCHandle newHandle = GCHandle.Alloc(newData, GCHandleType.Pinned);
        Buffer.BlockCopy(_data, 0, newData, 0, _data.Length);
        _data.Wipe();
        _handle.Free();
        _data = newData;
        _handle = newHandle;
      }
    }

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public void Write(ushort value)
    {
      Alloc(2);

      _data[_length] = (byte)((uint)value >> 8 & 0xFFu);
      _data[_length + 1] = (byte)(value & 0xFFu);
      _length += 2;
    }

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public void Write(short value)
    {
      Alloc(2);

      _data[_length] = (byte)((uint)value >> 8 & 0xFFu);
      _data[_length + 1] = (byte)(value & 0xFFu);
      _length += 2;
    }

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public void Write(uint value)
    {
      Alloc(4);

      _data[_length] = (byte)(value >> 24 & 0xFFu);
      _data[_length + 1] = (byte)(value >> 16 & 0xFFu);
      _data[_length + 2] = (byte)(value >> 8 & 0xFFu);
      _data[_length + 3] = (byte)(value & 0xFFu);
      _length += 4;
    }

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public void Write(int value)
    {
      Alloc(4);

      _data[_length] = (byte)(value >> 24 & 0xFFu);
      _data[_length + 1] = (byte)(value >> 16 & 0xFFu);
      _data[_length + 2] = (byte)(value >> 8 & 0xFFu);
      _data[_length + 3] = (byte)(value & 0xFFu);
      _length += 4;
    }

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public void Write(byte value)
    {
      Alloc(1);

      _data[_length] = value;
      _length++;
    }

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public void Write(byte[] value)
    {
      value.ArgumentNotNull();

      Alloc(value.Length);

      Buffer.BlockCopy(value, 0, _data, _length, value.Length);
      _length += value.Length;
    }

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public void Write(SecureByteArray value)
    {
      value.ArgumentNotNull();

      Alloc(value.Length);

      Buffer.BlockCopy(value.Data, 0, _data, _length, value.Length);
      _length += value.Length;
    }
  }
}