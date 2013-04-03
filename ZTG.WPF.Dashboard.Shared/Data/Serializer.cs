//----------------------------------------------------------------------------------------------------
// <copyright file="Serializer.cs" company="Zühlke Engineering AG">
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
  ///   Serializes data in Little-Endian format.
  /// </summary>
  public class Serializer : SerializerBase, IDisposable
  {
    /// <summary>
    /// The _stream.
    /// </summary>
    private Stream _stream;

    /// <summary>
    /// The _writer.
    /// </summary>
    private BinaryWriter _writer;

    /// <summary>
    ///   Initializes a new instance of the <see cref="Serializer" /> class.
    /// </summary>
    public Serializer()
    {
      _stream = new MemoryStream();
      _writer = new BinaryWriter(_stream);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Serializer"/> class.
    /// </summary>
    /// <param name="baseStream">
    /// The base Stream.
    /// </param>
    public Serializer(Stream baseStream)
    {
      _stream = baseStream;
      _writer = new BinaryWriter(_stream);
    }

    /// <summary>
    ///   Gets the data.
    /// </summary>
    /// <value>The data.</value>
    [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "It's clear that data is a byte array")]
    public byte[] Data
    {
      get
      {
        _writer.Flush();
        return ((MemoryStream)_stream).ToArray();
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
      if (_writer != null)
      {
        _writer.Close();
        _writer = null;
        _stream.Dispose();
        _stream = null;
      }
    }

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public override void Write(ushort value)
    {
      _writer.Write(value.HostToNetwork());
    }

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public override void Write(short value)
    {
      _writer.Write(value.HostToNetwork());
    }

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public override void Write(uint value)
    {
      _writer.Write(value.HostToNetwork());
    }

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public override void Write(int value)
    {
      _writer.Write(value.HostToNetwork());
    }

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public override void Write(ulong value)
    {
      _writer.Write(value.HostToNetwork());
    }

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public override void Write(long value)
    {
      _writer.Write(value.HostToNetwork());
    }

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public override void Write(byte value)
    {
      _writer.Write(value);
    }

    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public override void Write(byte[] value)
    {
      _writer.Write(value);
    }
  }
}