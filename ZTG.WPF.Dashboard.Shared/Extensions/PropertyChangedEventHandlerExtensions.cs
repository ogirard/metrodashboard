//----------------------------------------------------------------------------------------------------
// <copyright file="PropertyChangedEventHandlerExtensions.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace ZTG.WPF.Dashboard.Shared.Extensions
{
  /// <summary>
  ///   Extensions to the delegate type <see cref="PropertyChangedEventHandler" />.
  /// </summary>
  public static class PropertyChangedEventHandlerExtensions
  {
    /// <summary>
    /// Changes a field and notifies all registered receivers about a changed property,
    ///   if the value of its field has changed.
    /// </summary>
    /// <param name="handler">
    /// The handler.
    /// </param>
    /// <param name="field">
    /// The field.
    /// </param>
    /// <param name="fieldValue">
    /// The field Value.
    /// </param>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="propertyName">
    /// The property Name.
    /// </param>
    /// <returns>
    /// True, if the field has changed and therefore the value has been updated, false otherwise
    /// </returns>
    [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", 
      Justification = "Not possible in this place")]
    public static bool ChangeAndNotify<TField>(
      this PropertyChangedEventHandler handler, ref TField field, TField fieldValue, object sender, string propertyName)
    {
      if (!Equals(field, fieldValue))
      {
        field = fieldValue;
        handler.Notify(sender, propertyName);
        return true;
      }

      return false;
    }

    /// <summary>
    /// Notifies all receivers of a given <see cref="PropertyChangedEventHandler"/> handler.
    ///   The property name provided as event argument will be an empty string.
    /// </summary>
    /// <param name="handler">
    /// The handler.
    /// </param>
    /// <param name="sender">
    /// The sender.
    /// </param>
    [DebuggerStepThrough]
    public static void Notify(this PropertyChangedEventHandler handler, object sender)
    {
      Notify(handler, sender, string.Empty);
    }

    /// <summary>
    /// Notifies all receivers of a given <see cref="PropertyChangedEventHandler"/> handler
    ///   that a property with specific name has changed.
    /// </summary>
    /// <param name="handler">
    /// The handler.
    /// </param>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="propertyName">
    /// The property Name.
    /// </param>
    [DebuggerStepThrough]
    public static void Notify(this PropertyChangedEventHandler handler, object sender, string propertyName)
    {
      if (handler != null)
      {
        handler(sender, new PropertyChangedEventArgs(propertyName));
      }
    }
  }
}