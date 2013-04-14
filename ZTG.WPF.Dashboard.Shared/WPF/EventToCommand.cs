//----------------------------------------------------------------------------------------------------
// <copyright file="EventToCommand.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
// <summary>
//   Helper to bind commands to events in XAML.
//   <h5>Usage:</h5>
//   <code> [...]
//   xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity"
//   [...]
//   &lt;i:Interaction.Triggers&gt;
//   &lt;i:EventTrigger EventName="Click"&gt;
//   &lt;lib:EventToCommand Command="{Binding ...}" /&gt;
//   &lt;/i:EventTrigger&gt;
//   &lt;/i:Interaction.Triggers&gt;
//   </code>
// </summary>
//----------------------------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace ZTG.WPF.Dashboard.Shared.WPF
{
  /// <summary>
  ///   Helper to bind commands to events in XAML.
  ///   <h5>Usage:</h5>
  ///   <code> [...]
  /// xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity"
  /// [...]
  /// &lt;i:Interaction.Triggers&gt;
  ///   &lt;i:EventTrigger EventName="Click"&gt;
  ///     &lt;lib:EventToCommand Command="{Binding ...}" /&gt;
  ///   &lt;/i:EventTrigger&gt;
  /// &lt;/i:Interaction.Triggers&gt;
  /// </code>
  /// </summary>
  public class EventToCommand : TriggerAction<FrameworkElement>
  {
    /// <summary>
    ///   Identifies the <see cref="Command" /> dependency property
    /// </summary>
    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
      "Command",
      typeof(ICommand),
      typeof(EventToCommand),
      new PropertyMetadata(null, (s, e) => OnCommandChanged(s as EventToCommand, e)));

    /// <summary>
    ///   Identifies the <see cref="CommandParameter" /> dependency property
    /// </summary>
    public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
      "CommandParameter",
      typeof(object),
      typeof(EventToCommand),
      new PropertyMetadata(null, (s, e) => OnCommandParameterChanged(s as EventToCommand)));

    /// <summary>
    ///   Identifies the <see cref="MustToggleIsEnabled" /> dependency property
    /// </summary>
    public static readonly DependencyProperty MustToggleIsEnabledProperty =
      DependencyProperty.Register(
        "MustToggleIsEnabled",
        typeof(bool),
        typeof(EventToCommand),
        new PropertyMetadata(false, (s, e) => OnMustToggleIsEnabledChanged(s as EventToCommand)));

    /// <summary>
    /// The _command parameter value.
    /// </summary>
    private object _commandParameterValue;

    /// <summary>
    /// The _must toggle value.
    /// </summary>
    private bool? _mustToggleValue;

    /// <summary>
    ///   Gets or sets the ICommand that this trigger is bound to. This is a DependencyProperty.
    /// </summary>
    public ICommand Command
    {
      get
      {
        return (ICommand)GetValue(CommandProperty);
      }

      set
      {
        SetValue(CommandProperty, value);
      }
    }

    /// <summary>
    ///   Gets or sets an object that will be passed to the <see cref="Command" /> attached to this trigger. This is a DependencyProperty.
    /// </summary>
    public object CommandParameter
    {
      get
      {
        return GetValue(CommandParameterProperty);
      }

      set
      {
        SetValue(CommandParameterProperty, value);
      }
    }

    /// <summary>
    ///   Gets or sets a value indicating whether the attached element must be disabled when the <see cref="Command" /> property's CanExecuteChanged event fires. If this property is true, and the command's CanExecute method returns false, the element will be disabled. If this property is false, the element will not be disabled when the command's CanExecute method changes. This is a DependencyProperty.
    /// </summary>
    public bool MustToggleIsEnabled
    {
      get
      {
        return (bool)GetValue(MustToggleIsEnabledProperty);
      }

      set
      {
        SetValue(MustToggleIsEnabledProperty, value);
      }
    }

    /// <summary>
    ///   Gets or sets an object that will be passed to the <see cref="Command" /> attached to this trigger. This property is here for compatibility with the Silverlight version. This is NOT a DependencyProperty. For databinding, use the
    ///   <see
    ///     cref="CommandParameter" />
    ///   property.
    /// </summary>
    public object CommandParameterValue
    {
      get
      {
        return _commandParameterValue ?? CommandParameter;
      }

      set
      {
        _commandParameterValue = value;
        EnableDisableElement();
      }
    }

    /// <summary>
    ///   Gets or sets a value indicating whether the attached element must be disabled when the <see cref="Command" /> property's CanExecuteChanged event fires. If this property is true, and the command's CanExecute method returns false, the element will be disabled. This property is here for compatibility with the Silverlight version. This is NOT a DependencyProperty. For databinding, use the
    ///   <see
    ///     cref="MustToggleIsEnabled" />
    ///   property.
    /// </summary>
    public bool MustToggleIsEnabledValue
    {
      get
      {
        return _mustToggleValue == null ? MustToggleIsEnabled : _mustToggleValue.Value;
      }

      set
      {
        _mustToggleValue = value;
        EnableDisableElement();
      }
    }

    /// <summary>
    ///   Gets or sets a value indicating whether the EventArgs passed to the event handler will be forwarded to the ICommand's Execute method when the event is fired (if the bound ICommand accepts an argument of type EventArgs).
    ///   <para>
    ///     For example, use a RelayCommand&lt;MouseEventArgs&gt; to get
    ///     the arguments of a MouseMove event.
    ///   </para>
    /// </summary>
    public bool PassEventArgsToCommand { get; set; }

    /// <summary>
    /// The on command changed.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private static void OnCommandChanged(EventToCommand element, DependencyPropertyChangedEventArgs e)
    {
      if (element == null)
      {
        return;
      }

      if (e.OldValue != null)
      {
        ((ICommand)e.OldValue).CanExecuteChanged -= element.OnCommandCanExecuteChanged;
      }

      var command = (ICommand)e.NewValue;

      if (command != null)
      {
        command.CanExecuteChanged += element.OnCommandCanExecuteChanged;
      }

      element.EnableDisableElement();
    }

    /// <summary>
    /// The on command parameter changed.
    /// </summary>
    /// <param name="eventToCommand">
    /// The event to command.
    /// </param>
    private static void OnCommandParameterChanged(EventToCommand eventToCommand)
    {
      if (eventToCommand == null)
      {
        return;
      }

      if (eventToCommand.AssociatedObject == null)
      {
        return;
      }

      eventToCommand.EnableDisableElement();
    }

    /// <summary>
    /// The on must toggle is enabled changed.
    /// </summary>
    /// <param name="eventToCommand">
    /// The event to command.
    /// </param>
    private static void OnMustToggleIsEnabledChanged(EventToCommand eventToCommand)
    {
      if (eventToCommand == null)
      {
        return;
      }

      if (eventToCommand.AssociatedObject == null)
      {
        return;
      }

      eventToCommand.EnableDisableElement();
    }

    /// <summary>
    ///   Called when this trigger is attached to a FrameworkElement.
    /// </summary>
    protected override void OnAttached()
    {
      base.OnAttached();
      EnableDisableElement();
    }

    /// <summary>
    ///   This method is here for compatibility with the Silverlight version.
    /// </summary>
    /// <returns> The FrameworkElement to which this trigger is attached. </returns>
    private FrameworkElement GetAssociatedObject()
    {
      return AssociatedObject;
    }

    /// <summary>
    ///   This method is here for compatibility with the Silverlight version.
    /// </summary>
    /// <returns> The command that must be executed when this trigger is invoked. </returns>
    private ICommand GetCommand()
    {
      return Command;
    }

    /// <summary>
    ///   Provides a simple way to invoke this trigger programatically without any EventArgs.
    /// </summary>
    public void Invoke()
    {
      Invoke(null);
    }

    /// <summary>
    /// Executes the trigger.
    ///   <para>
    /// To access the EventArgs of the fired event, use a RelayCommand&lt;EventArgs&gt;
    ///     and leave the CommandParameter and CommandParameterValue empty!
    ///   </para>
    /// </summary>
    /// <param name="parameter">
    /// The EventArgs of the fired event. 
    /// </param>
    protected override void Invoke(object parameter)
    {
      if (AssociatedElementIsDisabled())
      {
        return;
      }

      ICommand command = GetCommand();
      object commandParameter = CommandParameterValue;

      if (commandParameter == null && PassEventArgsToCommand)
      {
        commandParameter = parameter;
      }

      if (command != null && command.CanExecute(commandParameter))
      {
        command.Execute(commandParameter);
      }
    }

    /// <summary>
    /// The associated element is disabled.
    /// </summary>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private bool AssociatedElementIsDisabled()
    {
      FrameworkElement element = GetAssociatedObject();

      return element != null && !element.IsEnabled;
    }

    /// <summary>
    /// The enable disable element.
    /// </summary>
    private void EnableDisableElement()
    {
      FrameworkElement element = GetAssociatedObject();

      if (element == null)
      {
        return;
      }

      ICommand command = GetCommand();

      if (MustToggleIsEnabledValue && command != null)
      {
        element.IsEnabled = command.CanExecute(CommandParameterValue);
      }
    }

    /// <summary>
    /// The on command can execute changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void OnCommandCanExecuteChanged(object sender, EventArgs e)
    {
      EnableDisableElement();
    }
  }
}