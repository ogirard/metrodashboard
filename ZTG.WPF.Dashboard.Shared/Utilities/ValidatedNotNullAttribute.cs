//----------------------------------------------------------------------------------------------------
// <copyright file="ValidatedNotNullAttribute.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;

namespace ZTG.WPF.Dashboard.Shared.Utilities
{
  /// <summary>
  ///   Validate not null attribute
  ///   To get CA1062 ValidateArgumentsOfPublicMethods to stop firing  when you validate the argument using a method,
  ///   define an attribute called ValidatedNotNullAttribute in your assembly and apply it to EnsureNotNull's "value" parameter:
  ///   http://www.go4answers.com/Example/incorrect-firing-ca1062-4346.aspx
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter)]
  public sealed class ValidatedNotNullAttribute : Attribute
  {
  }
}