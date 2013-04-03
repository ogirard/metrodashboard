//----------------------------------------------------------------------------------------------------
// <copyright file="NetworkDeserializer.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Sockets;

using ZTG.WPF.Dashboard.Shared.Extensions;
using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Data
{
  /// <summary>
  ///   Deserializes data from Little-Endian format.
  /// </summary>
  [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "The reader must not be disposed so the connection is not closed.")]
  [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Deserializer", Justification = "That's ok!")]
  public class NetworkDeserializer : DeserializerBase
  {
    /// <summary>
    /// The _client.
    /// </summary>
    private readonly TcpClient _client;

    /// <summary>
    /// The _reader.
    /// </summary>
    private readonly BinaryReader _reader;

    /// <summary>
    /// The _side action.
    /// </summary>
    private readonly Func<bool> _sideAction;

    /// <summary>
    /// The _stream.
    /// </summary>
    private readonly NetworkStream _stream;

    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkDeserializer"/> class.
    /// </summary>
    /// <param name="client">
    /// The _client.
    /// </param>
    /// <param name="sideAction">
    /// The side action. Will be called when not data can be read.
    ///   Must return true to continue or false to abort via IOException.
    /// </param>
    public NetworkDeserializer(TcpClient client, Func<bool> sideAction)
    {
      client.ArgumentNotNull();
      sideAction.ArgumentNotNull();

      _client = client;
      _stream = _client.GetStream();
      _reader = new BinaryReader(_stream);
      _sideAction = sideAction;
    }

    /// <summary>
    /// Waits until bytes of length can be read.
    ///   Calls the sideAction repeatedly if not.
    /// </summary>
    /// <param name="length">
    /// The length.
    /// </param>
    private void WaitForRead(int length)
    {
      while (_client.Available < length)
      {
        if (!_sideAction())
        {
          throw new IOException("Read aborted.");
        }
      }
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public override ushort ReadUInt16()
    {
      WaitForRead(sizeof(ushort));

      return _reader.ReadUInt16().NetworkToHost();
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public override short ReadInt16()
    {
      WaitForRead(sizeof(short));

      return _reader.ReadInt16().NetworkToHost();
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public override uint ReadUInt32()
    {
      WaitForRead(sizeof(uint));

      return _reader.ReadUInt32().NetworkToHost();
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public override int ReadInt32()
    {
      WaitForRead(sizeof(int));

      return _reader.ReadInt32().NetworkToHost();
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public override ulong ReadUInt64()
    {
      WaitForRead(sizeof(ulong));

      return _reader.ReadUInt64().NetworkToHost();
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public override long ReadInt64()
    {
      WaitForRead(sizeof(long));

      return _reader.ReadInt64().NetworkToHost();
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public override byte ReadByte()
    {
      WaitForRead(sizeof(byte));

      return _reader.ReadByte();
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
    public override byte[] ReadBytes(int count)
    {
      const int BlockSize = 4096;

      if (count > BlockSize)
      {
        var data = new byte[0];

        while (count > BlockSize)
        {
          WaitForRead(BlockSize);
          data = data.Concat(_reader.ReadBytes(BlockSize)).ToArray();
          count -= BlockSize;
        }

        WaitForRead(count);

        if (count > 0)
        {
          data = data.Concat(_reader.ReadBytes(count)).ToArray();
        }

        return data;
      }

      WaitForRead(count);

      return count > 0 ? _reader.ReadBytes(count) : new byte[0];
    }
  }
}