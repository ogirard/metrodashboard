//----------------------------------------------------------------------------------------------------
// <copyright file="DelegateCommand.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.WPF
{
  /// <summary>
  ///   DelegateCommand to be used without parameter <see cref="ICommand" />
  /// </summary>
  public class DelegateCommand : ICommand
  {
    /// <summary>
    /// The _can execute method.
    /// </summary>
    private readonly Func<bool> _canExecuteMethod;

    /// <summary>
    /// The _execute method.
    /// </summary>
    private readonly Action _executeMethod;

    /// <summary>
    /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
    /// </summary>
    /// <param name="executeMethod">
    /// The execute method.
    /// </param>
    public DelegateCommand(Action executeMethod)
      : this(executeMethod, () => true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
    /// </summary>
    /// <param name="executeMethod">
    /// The execute method.
    /// </param>
    /// <param name="canExecuteMethod">
    /// The can execute method.
    /// </param>
    public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
    {
      executeMethod.ArgumentNotNull("executeMethod");
      canExecuteMethod.ArgumentNotNull("canExecuteMethod");

      _executeMethod = executeMethod;
      _canExecuteMethod = canExecuteMethod;
    }

    /// <summary>
    ///   Gets a value indicating whether this command is executing (<see cref="Execute" />).
    /// </summary>
    public bool IsExecuting { get; private set; }

    /// <summary>
    /// Defines the method to be called when the command is invoked.
    /// </summary>
    /// <param name="parameter">
    /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
    /// </param>
    public void Execute(object parameter)
    {
      IsExecuting = true;
      _executeMethod();
      IsExecuting = false;
      RaiseExecuted();
    }

    /// <summary>
    /// Defines the method that determines whether the command can execute in its current state.
    /// </summary>
    /// <returns>
    /// true if this command can be executed; otherwise, false.
    /// </returns>
    /// <param name="parameter">
    /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
    /// </param>
    public bool CanExecute(object parameter)
    {
      return _canExecuteMethod();
    }

    /// <summary>
    ///   Occurs when changes occur that affect whether or not the command should execute.
    /// </summary>
    public event EventHandler CanExecuteChanged;

    /// <summary>
    ///   Occurs when the command is executed.
    /// </summary>
    public event EventHandler Executed;

    /// <summary>
    ///   Raises <see cref="Executed" />
    /// </summary>
    private void RaiseExecuted()
    {
      if (Executed != null)
      {
        Executed(this, EventArgs.Empty);
      }
    }

    /// <summary>
    ///   Raises <see cref="CanExecuteChanged" />
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", 
      Justification = "Not appropriate in this place")]
    public void RaiseCanExecuteChanged()
    {
      if (CanExecuteChanged != null)
      {
        CanExecuteChanged(this, EventArgs.Empty);
      }
    }
  }

  /// <summary>
  /// DelegateCommand to be used as generic <see cref="ICommand"/>
  /// </summary>
  /// <typeparam name="T">
  /// </typeparam>
  [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", 
    Justification = "Is OK here")]
  public class DelegateCommand<T> : ICommand
  {
    /// <summary>
    /// The _can execute method.
    /// </summary>
    private readonly Func<T, bool> _canExecuteMethod;

    /// <summary>
    /// The _execute method.
    /// </summary>
    private readonly Action<T> _executeMethod;

    /// <summary>
    /// Initializes a new instance of the <see cref="DelegateCommand{T}"/> class.
    /// </summary>
    /// <param name="executeMethod">
    /// The execute method.
    /// </param>
    public DelegateCommand(Action<T> executeMethod)
      : this(executeMethod, o => true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DelegateCommand{T}"/> class.
    /// </summary>
    /// <param name="executeMethod">
    /// The execute method.
    /// </param>
    /// <param name="canExecuteMethod">
    /// The can execute method.
    /// </param>
    public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
    {
      executeMethod.ArgumentNotNull("executeMethod");
      canExecuteMethod.ArgumentNotNull("canExecuteMethod");

      _executeMethod = executeMethod;
      _canExecuteMethod = canExecuteMethod;
    }

    /// <summary>
    ///   Gets a value indicating whether this command is executing (<see cref="Execute" />).
    /// </summary>
    public bool IsExecuting { get; private set; }

    /// <summary>
    /// Defines the method to be called when the command is invoked.
    /// </summary>
    /// <param name="parameter">
    /// Data used by the command.  If the command does not require data to be passed, this object can be set to null.
    /// </param>
    public void Execute(object parameter)
    {
      IsExecuting = true;
      var typedParam = (T)parameter;
      _executeMethod(typedParam);
      IsExecuting = false;
      RaiseExecuted(typedParam);
    }

    /// <summary>
    /// Defines the method that determines whether the command can execute in its current state.
    /// </summary>
    /// <returns>
    /// true if this command can be executed; otherwise, false.
    /// </returns>
    /// <param name="parameter">
    /// Data used by the command.  If the command does not require data to be passed, this object can be set to null.
    /// </param>
    public bool CanExecute(object parameter)
    {
      return _canExecuteMethod((T)parameter);
    }

    /// <summary>
    ///   Occurs when changes occur that affect whether or not the command should execute.
    /// </summary>
    public event EventHandler CanExecuteChanged;

    /// <summary>
    ///   Occurs when the command is executed.
    /// </summary>
    public event EventHandler<EventArgs<T>> Executed;

    /// <summary>
    /// Raises <see cref="Executed"/>
    /// </summary>
    /// <param name="parameter">
    /// The parameter.
    /// </param>
    private void RaiseExecuted(T parameter)
    {
      if (Executed != null)
      {
        Executed(this, new EventArgs<T>(parameter));
      }
    }

    /// <summary>
    ///   Raises <see cref="CanExecuteChanged" />
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", 
      Justification = "Not appropriate in this place")]
    public void RaiseCanExecuteChanged()
    {
      if (CanExecuteChanged != null)
      {
        CanExecuteChanged(this, EventArgs.Empty);
      }
    }
  }
}