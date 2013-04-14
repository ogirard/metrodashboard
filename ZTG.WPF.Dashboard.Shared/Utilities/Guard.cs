//----------------------------------------------------------------------------------------------------
// <copyright file="Guard.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using ZTG.WPF.Dashboard.Shared.Extensions;

namespace ZTG.WPF.Dashboard.Shared.Utilities
{
  /// <summary>
  ///   Common guard clauses to be used to check arguments e.g. in constructors.
  /// </summary>
  public static class Guard
  {
    /// <summary>
    /// Check string for string.Empty
    /// </summary>
    /// <param name="argumentValue">
    /// The argument value to check.
    /// </param>
    /// <param name="argumentName">
    /// The name of the argument.
    /// </param>
    /// <remarks>
    /// Please do not add the this parameter to this method to make it usable as extension method.
    ///   It makes the code unreadable and looks strange especially, if the argument is null!
    /// </remarks>
    [DebuggerStepThrough]
    public static void ArgumentNotNullOrEmpty([ValidatedNotNull] this string argumentValue, string argumentName = null)
    {
      ArgumentNotNull(argumentValue, argumentName);

      // Check string for string.Empty
      if (string.IsNullOrEmpty(argumentValue))
      {
        throw new ArgumentException("The provided String argument {0}must not be Null or Empty.".FormatText(UpdateArgumentName(argumentName)), argumentName);
      }
    }

    /// <summary>
    /// ValidateArgumentsOfPublicMethods
    ///   Category     : Microsoft.Design  (String)
    ///   CheckId      : CA1062  (String)
    ///   RuleFile     : Design Rules  (String)
    ///   Info         : All reference arguments passed to public methods should be tested against null,
    ///   as they can be provided by arbitrary callers.
    /// </summary>
    /// <typeparam name="T">
    /// The type
    /// </typeparam>
    /// <param name="argumentValue">
    /// The argument value to check.
    /// </param>
    /// <param name="argumentName">
    /// The name of the argument.
    /// </param>
    /// <remarks>
    /// It makes the code unreadable and looks strange especially, if the argument is null!
    ///   To get CA1062 ValidateArgumentsOfPublicMethods to stop firing  when you validate the argument using a method,
    ///   define an attribute called ValidatedNotNullAttribute in your assembly and apply it to EnsureNotNull's "value" parameter:
    ///   http://www.go4answers.com/Example/incorrect-firing-ca1062-4346.aspx
    /// </remarks>
    [DebuggerStepThrough]
    public static void ArgumentNotNull<T>([ValidatedNotNull] this T argumentValue, string argumentName = null)
      where T : class
    {
      if (argumentValue == null)
      {
        throw new ArgumentNullException("The provided argument {0}must not be Null.".FormatText(UpdateArgumentName(argumentName)), argumentName);
      }
    }

    /// <summary>
    /// Assert that the argument is true.
    /// </summary>
    /// <param name="argumentValue">
    /// if set to <c>true</c> [argument value].
    /// </param>
    /// <param name="internalMessage">
    /// The message.
    /// </param>
    [DebuggerStepThrough]
    public static void ArgumentTrue(this bool argumentValue, string internalMessage)
    {
      if (!argumentValue)
      {
        throw new ArgumentException(internalMessage);
      }
    }

    /// <summary>
    /// Assert that the argument is false.
    /// </summary>
    /// <param name="argumentValue">
    /// if set to <c>true</c> [argument value].
    /// </param>
    /// <param name="internalMessage">
    /// The message.
    /// </param>
    [DebuggerStepThrough]
    public static void ArgumentFalse(this bool argumentValue, string internalMessage)
    {
      if (argumentValue)
      {
        throw new ArgumentException(internalMessage);
      }
    }

    /// <summary>
    /// Verifies that the argument is between low and high.
    /// </summary>
    /// <param name="argumentValue">
    /// The argument value.
    /// </param>
    /// <param name="low">
    /// The lowest allowed value.
    /// </param>
    /// <param name="high">
    /// The highest allowed value.
    /// </param>
    public static void ArgumentInRange(this int argumentValue, int low, int high)
    {
      if (argumentValue < low)
      {
        throw new ArgumentOutOfRangeException("argumentValue");
      }

      if (argumentValue > high)
      {
        throw new ArgumentOutOfRangeException("argumentValue");
      }
    }

    /// <summary>
    /// Verifies that the argument is between low and high.
    /// </summary>
    /// <param name="argumentValue">
    /// The value.
    /// </param>
    /// <param name="low">
    /// The low.
    /// </param>
    /// <param name="high">
    /// The high.
    /// </param>
    public static void ArgumentInRange(this uint argumentValue, uint low, uint high)
    {
      if (argumentValue < low)
      {
        throw new ArgumentOutOfRangeException("argumentValue");
      }

      if (argumentValue > high)
      {
        throw new ArgumentOutOfRangeException("argumentValue");
      }
    }

    /// <summary>
    /// Verifies that the argument is between low and high.
    /// </summary>
    /// <param name="argumentValue">
    /// The value.
    /// </param>
    /// <param name="low">
    /// The low.
    /// </param>
    /// <param name="high">
    /// The high.
    /// </param>
    public static void ArgumentInRange(this ushort argumentValue, ushort low, ushort high)
    {
      if (argumentValue < low)
      {
        throw new ArgumentOutOfRangeException("argumentValue");
      }

      if (argumentValue > high)
      {
        throw new ArgumentOutOfRangeException("argumentValue");
      }
    }

    /// <summary>
    /// Verifies that the argument is between low and high.
    /// </summary>
    /// <param name="argumentValue">
    /// The value.
    /// </param>
    /// <param name="low">
    /// The low.
    /// </param>
    /// <param name="high">
    /// The high.
    /// </param>
    public static void ArgumentInRange(this byte argumentValue, byte low, byte high)
    {
      if (argumentValue < low)
      {
        throw new ArgumentOutOfRangeException("argumentValue");
      }

      if (argumentValue > high)
      {
        throw new ArgumentOutOfRangeException("argumentValue");
      }
    }

    /// <summary>
    /// Verifies that the argument is not null and contains the right amount of bytes.
    /// </summary>
    /// <param name="argumentValue">
    /// The argument value.
    /// </param>
    /// <param name="numberOfBytes">
    /// The expected number of bytes.
    /// </param>
    public static void ArgumentHasBytes(this byte[] argumentValue, int numberOfBytes)
    {
      argumentValue.ArgumentNotNull();

      if (argumentValue.Length != numberOfBytes)
      {
        throw new ArgumentOutOfRangeException("argumentValue");
      }
    }

    /// <summary>
    /// Throws an exception if the given list argument is empty. (The code does also check
    ///   for <c>null</c>.)
    /// </summary>
    /// <typeparam name="T">
    /// The type
    /// </typeparam>
    /// <param name="collection">
    /// The collection.
    /// </param>
    /// <param name="argumentName">
    /// Name of the argument.
    /// </param>
    [DebuggerStepThrough]
    public static void ArgumentCollectionNotEmpty<T>(this ICollection<T> collection, string argumentName = null)
    {
      ArgumentNotNull(collection, argumentName);

      if (collection.Count == 0)
      {
        throw new ArgumentException("The provided list argument {0}must not be empty.".FormatText(UpdateArgumentName(argumentName)), argumentName);
      }
    }

    /// <summary>
    /// Verifies that an argument type is assignable from the provided type (meaning
    ///   interfaces are implemented, or classes exist in the base class hierarchy).
    /// </summary>
    /// <param name="assignee">
    /// The argument type.
    /// </param>
    /// <param name="providedType">
    /// The type it must be assignable from.
    /// </param>
    /// <param name="argumentName">
    /// The argument name.
    /// </param>
    [DebuggerStepThrough]
    public static void TypeIsAssignableFromType(Type assignee, Type providedType, string argumentName)
    {
      ArgumentNotNull(providedType, "providedType");

      if (!providedType.IsAssignableFrom(assignee))
      {
        throw new ArgumentException("The provided type {0} is not compatible with {1}.".FormatText(assignee, providedType), argumentName);
      }
    }

    /// <summary>
    /// Verifies that the arguments are equal.
    /// </summary>
    /// <param name="object1">
    /// The object 1.
    /// </param>
    /// <param name="object2">
    /// The object 2.
    /// </param>
    [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "object",
      Justification = "OK in this case")]
    [DebuggerStepThrough]
    public static void ArgumentsAreEqual(object object1, object object2)
    {
      if (!Equals(object1, object2))
      {
        throw new ArgumentException("Not the same values.");
      }
    }

    /// <summary>
    /// The update argument name.
    /// </summary>
    /// <param name="argumentName">
    /// The argument name.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private static string UpdateArgumentName(string argumentName)
    {
      return (argumentName != null) ? "'{0}'".FormatText(argumentName) : null;
    }
  }
}