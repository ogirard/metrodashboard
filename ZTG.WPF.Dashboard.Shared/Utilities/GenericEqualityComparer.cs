//----------------------------------------------------------------------------------------------------
// <copyright file="GenericEqualityComparer.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace ZTG.WPF.Dashboard.Shared.Utilities
{
  /// <summary>
  /// The generic equality comparer.
  /// </summary>
  /// <typeparam name="T">
  /// </typeparam>
  public class GenericEqualityComparer<T> : IEqualityComparer<T>
  {
    /// <summary>
    /// The _compare.
    /// </summary>
    private readonly Func<T, T, bool> _compare;

    /// <summary>
    /// The _compute hash.
    /// </summary>
    private readonly Func<T, int> _computeHash;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericEqualityComparer{T}"/> class. 
    /// Initializes a new instance of the <see cref="GenericEqualityComparer&lt;T&gt;"/> class.
    /// </summary>
    /// <param name="compare">
    /// The compare.
    /// </param>
    /// <param name="computeHash">
    /// The compute hash (<tt>null</tt> for default hash).
    /// </param>
    public GenericEqualityComparer(Func<T, T, bool> compare, Func<T, int> computeHash = null)
    {
      compare.ArgumentNotNull("compare");

      _compare = compare;
      _computeHash = computeHash;
    }

    /// <summary>
    /// Equalses the specified x.
    /// </summary>
    /// <param name="x">
    /// The x.
    /// </param>
    /// <param name="y">
    /// The y.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool Equals(T x, T y)
    {
      return _compare(x, y);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <param name="obj">
    /// The obj.
    /// </param>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
    /// </returns>
    public int GetHashCode(T obj)
    {
      return _computeHash != null ? _computeHash(obj) : ((object)obj).GetHashCode();
    }
  }
}