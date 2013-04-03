//----------------------------------------------------------------------------------------------------
// <copyright file="Deserializer.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

using ZTG.WPF.Dashboard.Shared.Extensions;

namespace ZTG.WPF.Dashboard.Shared.Data
{
  /// <summary>
  ///   Deserializes data from Little-Endian format.
  /// </summary>
  [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Deserializer", 
    Justification = "That's ok!")]
  public class Deserializer : DeserializerBase, IDisposable
  {
    /// <summary>
    /// The _reader.
    /// </summary>
    private BinaryReader _reader;

    /// <summary>
    /// The _stream.
    /// </summary>
    private Stream _stream;

    /// <summary>
    /// Initializes a new instance of the <see cref="Deserializer"/> class.
    /// </summary>
    /// <param name="data">
    /// The data.
    /// </param>
    public Deserializer(byte[] data)
    {
      _stream = new MemoryStream(data);
      _reader = new BinaryReader(_stream);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Deserializer"/> class.
    /// </summary>
    /// <param name="baseStream">
    /// The base stream.
    /// </param>
    public Deserializer(Stream baseStream)
    {
      _stream = baseStream;
      _reader = new BinaryReader(_stream);
    }

    /// <summary>
    ///   Gets the remaining byte count.
    /// </summary>
    /// <value>The remaining byte count.</value>
    public long RemainingByteCount
    {
      get
      {
        return _stream.Length - _stream.Position;
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
      if (_reader != null)
      {
        _reader.Close();
        _reader = null;
        _stream.Close();
        _stream = null;
      }
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public override ushort ReadUInt16()
    {
      return _reader.ReadUInt16().NetworkToHost();
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public override short ReadInt16()
    {
      return _reader.ReadInt16().NetworkToHost();
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public override uint ReadUInt32()
    {
      return _reader.ReadUInt32().NetworkToHost();
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public override int ReadInt32()
    {
      return _reader.ReadInt32().NetworkToHost();
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public override long ReadInt64()
    {
      return _reader.ReadInt64().NetworkToHost();
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public override ulong ReadUInt64()
    {
      return _reader.ReadUInt64().NetworkToHost();
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public override byte ReadByte()
    {
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
      return _reader.ReadBytes(count);
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public ushort PeekUInt16()
    {
      var value = _reader.ReadUInt16().NetworkToHost();
      _stream.Position -= sizeof(ushort);
      return value;
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public short PeekInt16()
    {
      var value = _reader.ReadInt16().NetworkToHost();
      _stream.Position -= sizeof(short);
      return value;
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public uint PeekUInt32()
    {
      var value = _reader.ReadUInt32().NetworkToHost();
      _stream.Position -= sizeof(uint);
      return value;
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public int PeekInt32()
    {
      var value = _reader.ReadInt32().NetworkToHost();
      _stream.Position -= sizeof(int);
      return value;
    }

    /// <summary>
    ///   Reads a value.
    /// </summary>
    /// <returns>The value.</returns>
    public byte PeekByte()
    {
      byte value = _reader.ReadByte();
      _stream.Position -= sizeof(byte);
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
    public byte[] PeekBytes(int count)
    {
      byte[] value = _reader.ReadBytes(count);
      _stream.Position -= count;
      return value;
    }
  }
}