//----------------------------------------------------------------------------------------------------
// <copyright file="EventArgs.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;

namespace ZTG.WPF.Dashboard.Shared.Utilities
{
  /// <summary>
  /// Generic <see cref="EventArgs"/>
  /// </summary>
  /// <typeparam name="T">
  /// </typeparam>
  public class EventArgs<T> : EventArgs
  {
    /// <summary>
    /// The _value.
    /// </summary>
    private readonly T _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventArgs{T}"/> class. 
    /// Initializes a new instance of the <see cref="EventArgs&lt;T&gt;"/> class.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    public EventArgs(T value)
    {
      _value = value;
    }

    /// <summary>
    ///   Gets the value.
    /// </summary>
    /// <value>The value.</value>
    public T Value
    {
      get
      {
        return _value;
      }
    }
  }
}