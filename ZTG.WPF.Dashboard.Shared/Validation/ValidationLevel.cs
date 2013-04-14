//----------------------------------------------------------------------------------------------------
// <copyright file="ValidationLevel.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

namespace ZTG.WPF.Dashboard.Shared.Validation
{
  /// <summary>
  ///   Validation Levels
  /// </summary>
  public enum ValidationLevel
  {
    /// <summary>
    ///   Validation message with additional information, no impact on any action expected
    /// </summary>
    Information = 0,

    /// <summary>
    ///   Validation message notifying a warning which could probably have impact on a future action
    /// </summary>
    Warning = 1,

    /// <summary>
    ///   Level between <see cref="Warning" /> and <see cref="Error" /> indicating that eventually, this will be treated as an
    ///   <see
    ///     cref="Error" />
    /// </summary>
    Caution = 2,

    /// <summary>
    ///   Validation message notifying an error which has to be fixed immediately
    /// </summary>
    Error = 3
  }
}