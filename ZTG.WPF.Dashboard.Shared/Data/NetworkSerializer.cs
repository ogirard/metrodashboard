//----------------------------------------------------------------------------------------------------
// <copyright file="NetworkSerializer.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Sockets;

using ZTG.WPF.Dashboard.Shared.Extensions;
using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Data
{
  /// <summary>
  ///   Serializes data in Little-Endian format.
  /// </summary>
  [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Ceasing to write must not close the connection.")]
  public class NetworkSerializer : SerializerBase
  {
    /// <summary>
    /// The _client.
    /// </summary>
    private readonly TcpClient _client;

    /// <summary>
    /// The _stream.
    /// </summary>
    private readonly NetworkStream _stream;

    /// <summary>
    /// The _writer.
    /// </summary>
    private readonly BinaryWriter _writer;

    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkSerializer"/> class.
    /// </summary>
    /// <param name="client">
    /// The client.
    /// </param>
    public NetworkSerializer(TcpClient client)
    {
      client.ArgumentNotNull();

      _client = client;
      _stream = _client.GetStream();
      _writer = new BinaryWriter(_stream);
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
    public override void Write(ulong value)
    {
      _writer.Write(value.HostToNetwork());
    }

    /// <summary>
    ///   Flushes this instance.
    /// </summary>
    public void Flush()
    {
      _stream.Flush();
    }
  }
}