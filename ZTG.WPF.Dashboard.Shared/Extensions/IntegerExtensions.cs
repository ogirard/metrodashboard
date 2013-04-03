//----------------------------------------------------------------------------------------------------
// <copyright file="IntegerExtensions.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

namespace ZTG.WPF.Dashboard.Shared.Extensions
{
  /// <summary>
  ///   Extensions to integer. Reverse, HostToNetwork, NetworkToHost.
  /// </summary>
  public static class IntegerExtensions
  {
    /// <summary>
    /// Reverses the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="ushort"/>.
    /// </returns>
    public static ushort Reverse(ushort value)
    {
      return (ushort)((value >> 8) | (value << 8));
    }

    /// <summary>
    /// Reverses the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    public static uint Reverse(uint value)
    {
      return ((value >> 24) & 0x000000FFu) | ((value >> 8) & 0x0000FF00u) | ((value << 8) & 0x00FF0000u) | ((value << 24) & 0xFF000000u);
    }

    /// <summary>
    /// Reverses the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="ulong"/>.
    /// </returns>
    public static ulong Reverse(ulong value)
    {
      return ((value >> 56) & 0x00000000000000FFul) | ((value >> 40) & 0x000000000000FF00ul)
             | ((value >> 24) & 0x0000000000FF0000ul) | ((value >> 8) & 0x00000000FF000000ul)
             | ((value << 8) & 0x000000FF00000000ul) | ((value << 24) & 0x0000FF0000000000ul)
             | ((value << 40) & 0x00FF000000000000ul) | ((value << 56) & 0xFF00000000000000ul);
    }

    /// <summary>
    /// Reverses the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="short"/>.
    /// </returns>
    public static short Reverse(short value)
    {
      return (short)Reverse((ushort)value);
    }

    /// <summary>
    /// Reverses the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public static int Reverse(int value)
    {
      return (int)Reverse((uint)value);
    }

    /// <summary>
    /// Reverses the specified value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="long"/>.
    /// </returns>
    public static long Reverse(long value)
    {
      return (long)Reverse((ulong)value);
    }

    /// <summary>
    /// Networks to host.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="ushort"/>.
    /// </returns>
    public static ushort NetworkToHost(this ushort value)
    {
      return Reverse(value);
    }

    /// <summary>
    /// Networks to host.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    public static uint NetworkToHost(this uint value)
    {
      return Reverse(value);
    }

    /// <summary>
    /// Hosts to network.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="ushort"/>.
    /// </returns>
    public static ushort HostToNetwork(this ushort value)
    {
      return Reverse(value);
    }

    /// <summary>
    /// Hosts to network.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    public static uint HostToNetwork(this uint value)
    {
      return Reverse(value);
    }

    /// <summary>
    /// Networks to host.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="short"/>.
    /// </returns>
    public static short NetworkToHost(this short value)
    {
      return Reverse(value);
    }

    /// <summary>
    /// Networks to host.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public static int NetworkToHost(this int value)
    {
      return Reverse(value);
    }

    /// <summary>
    /// Networks to host.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="long"/>.
    /// </returns>
    public static long NetworkToHost(this long value)
    {
      return Reverse(value);
    }

    /// <summary>
    /// Networks to host.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="ulong"/>.
    /// </returns>
    public static ulong NetworkToHost(this ulong value)
    {
      return Reverse(value);
    }

    /// <summary>
    /// Hosts to network.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="short"/>.
    /// </returns>
    public static short HostToNetwork(this short value)
    {
      return Reverse(value);
    }

    /// <summary>
    /// Hosts to network.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public static int HostToNetwork(this int value)
    {
      return Reverse(value);
    }

    /// <summary>
    /// Hosts to network.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="long"/>.
    /// </returns>
    public static long HostToNetwork(this long value)
    {
      return Reverse(value);
    }

    /// <summary>
    /// Hosts to network.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="ulong"/>.
    /// </returns>
    public static ulong HostToNetwork(this ulong value)
    {
      return Reverse(value);
    }
  }
}