//----------------------------------------------------------------------------------------------------
// <copyright file="EventHandlerExtensions.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace ZTG.WPF.Dashboard.Shared.Extensions
{
  /// <summary>
  ///   Method extensions for generic and non generic event handlers.
  /// </summary>
  public static class EventHandlerExtensions
  {
    /// <summary>
    /// Notifies the listeners of a normal event handler.
    /// </summary>
    /// <param name="handler">
    /// The handler.
    /// </param>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The <see cref="System.EventArgs"/> instance containing the event data.
    /// </param>
    [DebuggerStepThrough]
    public static void Notify(this EventHandler handler, object sender, EventArgs args)
    {
      if (handler != null)
      {
        handler(sender, args);
      }
    }

    /// <summary>
    /// Notifies the listeners of a generic event handler.
    /// </summary>
    /// <typeparam name="TArgument">
    /// The type of the argument.
    /// </typeparam>
    /// <param name="handler">
    /// The handler.
    /// </param>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The event argument.
    /// </param>
    [DebuggerStepThrough]
    public static void Notify<TArgument>(this EventHandler<TArgument> handler, object sender, TArgument args)
      where TArgument : EventArgs
    {
      if (handler != null)
      {
        handler(sender, args);
      }
    }

    /// <summary>
    /// Changes a field and notifies all registered receivers if the field has changed.
    /// </summary>
    /// <typeparam name="TField">
    /// The type of the field.
    /// </typeparam>
    /// <param name="handler">
    /// The handler.
    /// </param>
    /// <param name="field">
    /// The field.
    /// </param>
    /// <param name="fieldValue">
    /// The field value.
    /// </param>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The <see cref="System.EventArgs"/> instance containing the event data.
    /// </param>
    [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#",
      Justification = "OK in this case")]
    public static void ChangeAndNotify<TField>(
      this EventHandler handler, ref TField field, TField fieldValue, object sender, EventArgs args)
    {
      if (!Equals(field, fieldValue))
      {
        field = fieldValue;
        handler.Notify(sender, args);
      }
    }

    /// <summary>
    /// Changes a field and notifies all registered receivers if the field has changed.
    /// </summary>
    /// <typeparam name="TField">
    /// The type of the field.
    /// </typeparam>
    /// <typeparam name="TArgs">
    /// The type of the args.
    /// </typeparam>
    /// <param name="handler">
    /// The handler.
    /// </param>
    /// <param name="field">
    /// The field.
    /// </param>
    /// <param name="fieldValue">
    /// The field value.
    /// </param>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The <see cref="System.EventArgs"/> instance containing the event data.
    /// </param>
    [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#",
      Justification = "OK in this case")]
    public static void ChangeAndNotify<TField, TArgs>(
      this EventHandler<TArgs> handler, ref TField field, TField fieldValue, object sender, TArgs args)
      where TArgs : EventArgs
    {
      if (!Equals(field, fieldValue))
      {
        field = fieldValue;
        handler.Notify(sender, args);
      }
    }
  }
}