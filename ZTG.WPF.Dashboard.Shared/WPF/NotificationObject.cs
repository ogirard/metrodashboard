//----------------------------------------------------------------------------------------------------
// <copyright file="NotificationObject.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

using ZTG.WPF.Dashboard.Shared.Extensions;

namespace ZTG.WPF.Dashboard.Shared.WPF
{
  /// <summary>
  ///   NotificationObject
  ///   Wrapps the functionality of the extension class <see cref="PropertyChangedEventHandlerExtensions" />
  /// </summary>
  [Serializable]
  public class NotificationObject : INotifyPropertyChanged
  {
    /// <summary>
    /// Changes a field and notifies all receivers of a given <see cref="PropertyChangedEventHandler"/> handler,
    ///   if the value of its field has changed.
    /// </summary>
    /// <param name="field">
    /// The field.
    /// </param>
    /// <param name="fieldValue">
    /// The field Value.
    /// </param>
    /// <param name="propertyName">
    /// The property Name.
    /// </param>
    /// <returns>
    /// True, if the field has changed and therefore the value has been updated, false otherwise
    /// </returns>
    [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", 
      Justification = "OK in this case")]
    public bool ChangeAndNotify<TField>(ref TField field, TField fieldValue, string propertyName)
    {
      return PropertyChanged.ChangeAndNotify(ref field, fieldValue, this, propertyName);
    }

    /// <summary>
    ///   Notifies all receivers of a given <see cref="PropertyChangedEventHandler" /> handler.
    ///   The property name provided as event argument will be an empty string.
    /// </summary>
    public void Notify()
    {
      PropertyChanged.Notify(this);
    }

    /// <summary>
    /// Notifies all receivers of a given <see cref="PropertyChangedEventHandler"/> handler
    ///   that a property with specific name has changed.
    /// </summary>
    /// <param name="propertyName">
    /// The property Name.
    /// </param>
    public void Notify(string propertyName)
    {
      PropertyChanged.Notify(this, propertyName);
    }

    #region INotifyPropertyChanged Members

    /// <summary>
    ///   Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}