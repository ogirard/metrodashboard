//----------------------------------------------------------------------------------------------------
// <copyright file="GenericSorter.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace ZTG.WPF.Dashboard.Shared.Utilities
{
  /// <summary>
  /// The generic sorter.
  /// </summary>
  /// <typeparam name="T">
  /// </typeparam>
  public class GenericSorter<T> : Comparer<T>
    where T : class
  {
    /// <summary>
    /// The _compare.
    /// </summary>
    private readonly Func<T, T, int> _compare;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericSorter{T}"/> class.
    /// </summary>
    /// <param name="compare">
    /// The compare.
    /// </param>
    public GenericSorter(Func<T, T, int> compare)
    {
      compare.ArgumentNotNull("compare");
      _compare = compare;
    }

    /// <summary>
    /// Compares x to y.
    ///   Returns 0 if x equals y, -1 if x smaller than y or y <tt>null</tt>, 1 if x greater than y or x <tt>null</tt>
    /// </summary>
    /// <param name="x">
    /// The first element.
    /// </param>
    /// <param name="y">
    /// The second element.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public override int Compare(T x, T y)
    {
      if (x == y)
      {
        return 0;
      }

      if (x == null)
      {
        return 1;
      }

      if (y == null)
      {
        return -1;
      }

      return _compare(x, y);
    }
  }
}