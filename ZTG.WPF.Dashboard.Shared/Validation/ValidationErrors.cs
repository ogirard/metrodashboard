//----------------------------------------------------------------------------------------------------
// <copyright file="ValidationErrors.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Validation
{
  /// <summary>
  ///   Provides <see cref="ValidationError" />s
  /// </summary>
  [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "I don't want to!")]
  [Serializable]
  public sealed class ValidationErrors : IEnumerable<ValidationError>
  {
    /// <summary>
    /// The _validation errors.
    /// </summary>
    private readonly List<ValidationError> _validationErrors = new List<ValidationError>();

    /// <summary>
    ///   Gets a value indicating whether messages of any <see cref="ValidationLevel" /> are available
    /// </summary>
    public bool HasMessages
    {
      get
      {
        return _validationErrors.Any();
      }
    }

    /// <summary>
    ///   Gets a value indicating whether the validation has succeeded.
    /// </summary>
    /// <value>
    ///   Returns <c>true</c> if this validation has succeeded; otherwise, <c>false</c>.
    /// </value>
    public bool IsValid
    {
      get
      {
        return _validationErrors.All(x => x.Level < ValidationLevel.Caution);
      }
    }

    /// <summary>
    ///   Gets a value indicating whether the validation resulted in <see cref="ValidationLevel.Error" /> messages
    /// </summary>
    /// <value>
    ///   Returns <c>true</c> if this validation resulted in <see cref="ValidationLevel.Error" /> messages; otherwise, <c>false</c>.
    /// </value>
    public bool IsValidForSave
    {
      get
      {
        return _validationErrors.All(x => x.Level < ValidationLevel.Error);
      }
    }

    /// <summary>
    ///   Gets the error message which does not refer to a specific property. Or <tt>null</tt> if no errors available.
    /// </summary>
    /// <value>The error messages formatted as one error message per line in no specific order</value>
    public string GeneralErrorMessage
    {
      get
      {
        return GetErrorMessage(ValidationErrorScope.General);
      }
    }

    /// <summary>
    ///   Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
    /// </returns>
    /// <filterpriority>1</filterpriority>
    public IEnumerator<ValidationError> GetEnumerator()
    {
      return _validationErrors.ToList().GetEnumerator();
    }

    /// <summary>
    ///   Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>
    ///   An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
    /// </returns>
    /// <filterpriority>2</filterpriority>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    /// <summary>
    /// Adds a validation error.
    /// </summary>
    /// <param name="errorScope">
    /// The error scope.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="level">
    /// The validation level.
    /// </param>
    public void AddValidationError(ValidationErrorScope errorScope, string message, ValidationLevel level)
    {
      AddValidationError(errorScope, message, null, level);
    }

    /// <summary>
    /// Adds a validation error.
    /// </summary>
    /// <param name="errorScope">
    /// The error scope.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="propertyName">
    /// Name of the property.
    /// </param>
    /// <param name="level">
    /// The validation level.
    /// </param>
    public void AddValidationError(
      ValidationErrorScope errorScope, string message, string propertyName, ValidationLevel level)
    {
      ValidationError validationError = _validationErrors.FirstOrDefault(v => v.IsErrorForProperty(propertyName));
      if (validationError != null)
      {
        validationError.AddErrorMessage(message);
      }
      else
      {
        _validationErrors.Add(new ValidationError(errorScope, message, propertyName, level));
      }
    }

    /// <summary>
    /// Gets all error messages relevant for the given <paramref name="errorScope"/>, combined to one string with NewLines between each message.
    ///   If there are no errors for the given scope, <tt>null</tt> is returned.
    /// </summary>
    /// <param name="errorScope">
    /// The error scope.
    /// </param>
    /// <param name="exactMatch">
    /// if set to <c>true</c> this validation error has to belong to ALL of the scopes listed in <paramref name="errorScope"/>, otherwise it may belong to ANY of the listed scopes(default).
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string GetErrorMessage(ValidationErrorScope errorScope, bool exactMatch = false)
    {
      if (!_validationErrors.Any())
      {
        return null;
      }

      IEnumerable<string> errorMessages =
        _validationErrors.Where(v => v.BelongsToErrorScope(errorScope, exactMatch) && !string.IsNullOrEmpty(v.Message))
                         .OrderBy(v => v.Message)
                         .Select(v => v.Message);
      string errorMessage = string.Join(Environment.NewLine, errorMessages);
      return string.IsNullOrEmpty(errorMessage) ? null : errorMessage;
    }

    /// <summary>
    /// Gets a value indicating whether the validation of the given property has succeeded.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the property.
    /// </param>
    /// <returns>
    /// Returns <c>true</c> if this validation of the given property has succeeded; otherwise, <c>false</c>.
    /// </returns>
    public bool IsPropertyValid(string propertyName)
    {
      propertyName.ArgumentNotNull("propertyName");

      return GetMessageForProperty(propertyName) == null;
    }

    /// <summary>
    /// Gets the <see cref="ValidationError"/> for the given property name or <tt>null</tt> if no message available (i.e. if the property is valid).
    /// </summary>
    /// <param name="propertyName">
    /// Name of the property.
    /// </param>
    /// <returns>
    /// The <see cref="ValidationError"/> for the given property name or <tt>null</tt> if no message available (i.e. if the property is valid).
    /// </returns>
    public string GetMessageForProperty(string propertyName)
    {
      propertyName.ArgumentNotNull("propertyName");

      ValidationError propertyValidationError = _validationErrors.FirstOrDefault(
        v => v.IsErrorForProperty(propertyName));

      return propertyValidationError != null ? propertyValidationError.Message : null;
    }

    /// <summary>
    /// Merges the given ValidationErrors object into this one.
    /// </summary>
    /// <param name="errors">
    /// The ValidationErrors object to be merged.
    /// </param>
    /// <param name="errorPrefix">
    /// A prefix that is added in front of each error message. Mustn't be null.
    /// </param>
    public void MergeValidationErrors(ValidationErrors errors, string errorPrefix)
    {
      errors.ArgumentNotNull("errors");
      errorPrefix.ArgumentNotNull("errorPrefix");

      // merge all errors
      foreach (ValidationError error in errors._validationErrors)
      {
        _validationErrors.Add(
          new ValidationError(
            error.ErrorScope, string.Concat(errorPrefix, error.Message), error.PropertyName, error.Level));
      }
    }
  }
}