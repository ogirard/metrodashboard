//----------------------------------------------------------------------------------------------------
// <copyright file="SecureByteArray.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;

using ZTG.WPF.Dashboard.Shared.Extensions;
using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Data
{
  /// <summary>
  ///   Byte array that can be securely wiped.
  /// </summary>
  public class SecureByteArray : IDisposable
  {
    /// <summary>
    /// The _data.
    /// </summary>
    private byte[] _data;

    /// <summary>
    /// The _handle.
    /// </summary>
    private GCHandle _handle;

    /// <summary>
    /// Initializes a new instance of the <see cref="SecureByteArray"/> class.
    /// </summary>
    /// <param name="secureString">
    /// The secure string.
    /// </param>
    /// <param name="paddingMultiplier">
    /// The padding multiplier.
    /// </param>
    public SecureByteArray(SecureString secureString, int paddingMultiplier)
    {
      secureString.ArgumentNotNull();

      int paddedLength = secureString.Length % paddingMultiplier == 0
                           ? secureString.Length
                           : secureString.Length + paddingMultiplier - (secureString.Length % paddingMultiplier);
      _data = new byte[paddedLength];
      _handle = GCHandle.Alloc(_data, GCHandleType.Pinned);

      IntPtr pointer = IntPtr.Zero;

      try
      {
        _data[0] = (byte)secureString.Length;
        pointer = Marshal.SecureStringToGlobalAllocAnsi(secureString);
        Marshal.Copy(pointer, _data, 1, secureString.Length);
      }
      finally
      {
        Marshal.FreeHGlobal(pointer);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SecureByteArray"/> class.
    /// </summary>
    /// <param name="length">
    /// The length.
    /// </param>
    public SecureByteArray(int length)
    {
      _data = new byte[length];
      _handle = GCHandle.Alloc(_data, GCHandleType.Pinned);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SecureByteArray"/> class.
    /// </summary>
    /// <param name="data">
    /// The data.
    /// </param>
    public SecureByteArray(byte[] data)
    {
      _data = data;
      _handle = GCHandle.Alloc(_data, GCHandleType.Pinned);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SecureByteArray"/> class.
    /// </summary>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <param name="handle">
    /// The handle.
    /// </param>
    public SecureByteArray(byte[] data, GCHandle handle)
    {
      _data = data;
      _handle = handle;
    }

    /// <summary>
    ///   Gets as secure string.
    /// </summary>
    /// <value>As secure string.</value>
    /// <remarks>
    ///   Caller needs to care about disposing!
    /// </remarks>
    [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope",
      Justification = "Return but not dispose.")]
    public SecureString AsSecureString
    {
      get
      {
        int length = _data[0];
        var secureString = new SecureString();

        for (int i = 0; i < length; i++)
        {
          secureString.AppendChar((char)_data[1 + i]);
        }

        return secureString;
      }
    }

    /// <summary>
    ///   Gets the length.
    /// </summary>
    /// <value>The length.</value>
    public int Length
    {
      get
      {
        return _data.Length;
      }
    }

    /// <summary>
    ///   Returns the data of the bytearray.
    /// </summary>
    /// <returns></returns>
    [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays",
      Justification = "Just bytes.")]
    public byte[] Data
    {
      get
      {
        return _data;
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
    /// Parts the specified index.
    /// </summary>
    /// <param name="index">
    /// The index.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <returns>
    /// The <see cref="SecureByteArray"/>.
    /// </returns>
    /// <remarks>
    /// Caller needs to care about disposing!
    /// </remarks>
    [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope",
      Justification = "Return not dispose.")]
    public SecureByteArray Part(int index, int count)
    {
      var part = new SecureByteArray(count);
      Buffer.BlockCopy(_data, index, part._data, 0, count);
      return part;
    }

    /// <summary>
    /// Concats the specified other.
    /// </summary>
    /// <param name="other">
    /// The other.
    /// </param>
    /// <returns>
    /// The <see cref="SecureByteArray"/>.
    /// </returns>
    /// <remarks>
    /// Caller needs to care about disposing!
    /// </remarks>
    [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope",
      Justification = "Return not dispose.")]
    public SecureByteArray Concat(SecureByteArray other)
    {
      other.ArgumentNotNull();
      var concat = new SecureByteArray(Length + other.Length);
      Buffer.BlockCopy(_data, 0, concat._data, 0, Length);
      Buffer.BlockCopy(other._data, 0, concat._data, Length, other.Length);
      return concat;
    }

    /// <summary>
    /// Exports this instance.
    /// </summary>
    /// <returns>
    /// The byte array
    /// </returns>
    public byte[] Export()
    {
      var buffer = new byte[_data.Length];
      Buffer.BlockCopy(_data, 0, buffer, 0, _data.Length);
      return buffer;
    }
  }
}