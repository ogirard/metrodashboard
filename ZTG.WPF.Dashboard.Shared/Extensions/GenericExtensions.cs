//----------------------------------------------------------------------------------------------------
// <copyright file="GenericExtensions.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

namespace ZTG.WPF.Dashboard.Shared.Extensions
{
  /// <summary>
  ///   generic extensions
  /// </summary>
  public static class GenericExtensions
  {
    /// <summary>
    /// just an alternative inline syntax for casting objects
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    public static T Cast<T>(this object value)
    {
      return (T)value;
    }
  }
}