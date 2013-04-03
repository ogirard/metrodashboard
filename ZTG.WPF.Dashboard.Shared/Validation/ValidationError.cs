//----------------------------------------------------------------------------------------------------
// <copyright file="ValidationError.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Validation
{
  /// <summary>
  ///   Provides Validation Error Data
  /// </summary>
  [Serializable]
  public sealed class ValidationError
  {
    /// <summary>
    /// The _error scope.
    /// </summary>
    private readonly ValidationErrorScope _errorScope;

    /// <summary>
    /// The _level.
    /// </summary>
    private readonly ValidationLevel _level;

    /// <summary>
    /// The _property name.
    /// </summary>
    private readonly string _propertyName;

    /// <summary>
    /// The _message.
    /// </summary>
    private string _message;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationError"/> class.
    /// </summary>
    /// <param name="errorScope">
    /// The error scope.
    /// </param>
    /// <param name="message">
    /// The transalted message.
    /// </param>
    /// <param name="level">
    /// The validation level.
    /// </param>
    public ValidationError(ValidationErrorScope errorScope, string message, ValidationLevel level)
      : this(errorScope, message, null, level)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationError"/> class.
    /// </summary>
    /// <param name="errorScope">
    /// The error scope.
    /// </param>
    /// <param name="message">
    /// The transalted message.
    /// </param>
    /// <param name="propertyName">
    /// Name of the property or <tt>null</tt> if the message does not refer to a specific property.
    /// </param>
    /// <param name="level">
    /// The validation level.
    /// </param>
    public ValidationError(ValidationErrorScope errorScope, string message, string propertyName, ValidationLevel level)
    {
      message.ArgumentNotNull("message");
      if (errorScope.HasFlag(ValidationErrorScope.Property))
      {
        propertyName.ArgumentNotNullOrEmpty("propertyName");
      }

      _errorScope = errorScope;
      _propertyName = propertyName;
      _message = message;
      _level = level;
    }

    /// <summary>
    ///   Gets the error  <see cref="ValidationErrorScope" /> of this validation error (more than one scope can be combined)
    /// </summary>
    public ValidationErrorScope ErrorScope
    {
      get
      {
        return _errorScope;
      }
    }

    /// <summary>
    /// Gets the level.
    /// </summary>
    public ValidationLevel Level
    {
      get
      {
        return _level;
      }
    }

    /// <summary>
    ///   Gets the name of the property if this <see cref="ValidationError" /> refers to a specific field
    /// </summary>
    public string PropertyName
    {
      get
      {
        return _propertyName;
      }
    }

    /// <summary>
    ///   Gets the translated validation message.
    /// </summary>
    public string Message
    {
      get
      {
        return _message;
      }
    }

    /// <summary>
    /// Determines whether this validation error belongs to the given <paramref name="errorScope"/>
    /// </summary>
    /// <param name="errorScope">
    /// The error scope.
    /// </param>
    /// <param name="exactMatch">
    /// if set to <c>true</c> this validation error has to belong to all of the listed scopes, otherwise it may belong to any (default).
    /// </param>
    /// <returns>
    /// if <paramref name="exactMatch"/> is set to <c>false</c>, returns <c>true</c> if this validation error belongs to any of the given scopes in
    ///   <paramref name="errorScope"/>
    ///   , otherwise <c>false</c>.
    ///   if an exact match is required, <c>true</c> is returned only if this error belongs to all of the listed scopes
    /// </returns>
    public bool BelongsToErrorScope(ValidationErrorScope errorScope, bool exactMatch = false)
    {
      // hidden scope can not be combined with other scopes, requires always an exact match
      if (errorScope == ValidationErrorScope.Hidden || exactMatch)
      {
        return _errorScope == errorScope;
      }

      IEnumerable<ValidationErrorScope> selectedFlags =
        Enum.GetValues(typeof(ValidationErrorScope))
            .Cast<ValidationErrorScope>()
            .Where(s => errorScope.HasFlag(s) && s != ValidationErrorScope.Hidden);

      // return true when one of the given flags
      return selectedFlags.Any(s => _errorScope.HasFlag(s));
    }

    /// <summary>
    /// Adds the error message to the existing, separated by line break.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    public void AddErrorMessage(string message)
    {
      message.ArgumentNotNull("message");

      lock (this)
      {
        _message += Environment.NewLine + message;
      }
    }

    /// <summary>
    /// Checks if the given validation error belongs the <see cref="ValidationErrorScope.Property"/> error scope and if it maps to the given
    ///   <paramref name="propertyName"/>
    /// </summary>
    /// <param name="propertyName">
    /// Name of the property.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool IsErrorForProperty(string propertyName)
    {
      if (!BelongsToErrorScope(ValidationErrorScope.Property) || string.IsNullOrEmpty(propertyName))
      {
        return false;
      }

      return string.Compare(propertyName, _propertyName, StringComparison.CurrentCultureIgnoreCase) == 0;
    }
  }
}