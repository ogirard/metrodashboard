//----------------------------------------------------------------------------------------------------
// <copyright file="PropertySupport.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace ZTG.WPF.Dashboard.Shared.Utilities
{
  /// <summary>
  ///   Provides support for extracting property information based on a property expression.
  ///   Borrowed from Prism source code.
  /// </summary>
  public static class PropertySupport
  {
    /// <summary>
    /// Extracts the name of the property from a property expression.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <param name="propertyExpression">
    /// The property expression.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
      Justification = "By design")]
    public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
    {
      if (propertyExpression == null)
      {
        throw new ArgumentNullException("propertyExpression");
      }

      var memberExpression = propertyExpression.Body as MemberExpression;
      if (memberExpression == null)
      {
        throw new ArgumentException("The expression is not a member access expression", "propertyExpression");
      }

      var property = memberExpression.Member as PropertyInfo;
      if (property == null)
      {
        throw new ArgumentException("The member access expression does not access a property.", "propertyExpression");
      }

      MethodInfo getMethod = property.GetGetMethod(true);
      if (getMethod.IsStatic)
      {
        throw new ArgumentException("The referenced property is a static property.", "propertyExpression");
      }

      return memberExpression.Member.Name;
    }
  }
}