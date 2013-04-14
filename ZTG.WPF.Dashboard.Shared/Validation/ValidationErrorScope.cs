//----------------------------------------------------------------------------------------------------
// <copyright file="ValidationErrorScope.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;

namespace ZTG.WPF.Dashboard.Shared.Validation
{
  /// <summary>
  ///   Enum defining possible scopes for validation errors
  /// </summary>
  [SuppressMessage("Microsoft.Naming", "CA1714:FlagsEnumsShouldHavePluralNames", Justification = "Singular is OK")]
  [SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "None is not accurate enough here")]
  [Flags]
  public enum ValidationErrorScope
  {
    /// <summary>
    ///   The validation error is never displayed to the user (this is not combinable with other scopes)
    /// </summary>
    Hidden = 0x0,

    /// <summary>
    ///   The validation error should be logged
    /// </summary>
    Log = 0x1,

    /// <summary>
    ///   The validation error contains mapping information to a propert and should be displayed on that property field
    /// </summary>
    Property = 0x10,

    /// <summary>
    ///   The validation error has a general cause and should be displayed at a general location (e.g. at bottom / top of dialog)
    /// </summary>
    General = 0x100,

    /// <summary>
    ///   The validation error is a system validation error (due to inconsistency) and should be displayed and logged at prominent location
    /// </summary>
    System = 0x1000
  }
}