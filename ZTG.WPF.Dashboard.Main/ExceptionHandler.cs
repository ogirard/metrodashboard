// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionHandler.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

using ZTG.WPF.Dashboard.Shared.Extensions;
using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Main
{
  public class ExceptionHandler : IDisposable
  {
    private readonly Window _window;

    public ExceptionHandler(Window window)
    {
      _window = window;
      Dispatcher.CurrentDispatcher.UnhandledException += CurrentDispatcherUnhandledException;
      AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;
      TaskScheduler.UnobservedTaskException += TaskSchedulerUnobservedTaskException;
    }

    private void TaskSchedulerUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
      ReportErrorAndShutdownApplication(e.Exception);
    }

    private void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      var exception = e.ExceptionObject as Exception;

      ReportErrorAndShutdownApplication(exception);
    }

    private void CurrentDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
      ReportErrorAndShutdownApplication(e.Exception);
    }

    private void ReportErrorAndShutdownApplication(Exception exception)
    {
      exception.ArgumentNotNull("exception");

      try
      {
        // Because we show a Messagebox, we need to dispatch it
        // It is possible, that this methode is called by any thread
        if (_window != null && !_window.Dispatcher.CheckAccess())
        {
          _window.Dispatcher.Invoke(DispatcherPriority.Send, (Action)(() => ReportError(exception)));
        }
        else
        {
          ReportError(exception);
        }
      }
      finally
      {
        Environment.Exit(-1);
      }
    }

    private void ReportError(Exception exception)
    {
      // 1.) Get the error message
      var message = GetExceptionMessage(exception);

      // 2.) Show error message
      var details = GetInnerExceptionMessage(exception);
      if (details == null)
      {
        details = exception.StackTrace.ToString(CultureInfo.InvariantCulture);
      }

      ShowErrorMessage(message, details, _window);
    }

    public static void ShowErrorMessage(string message, string details, Window mainWindow)
    {
      message.ArgumentNotNullOrEmpty("message");
      //// var messageInfo = GetMessageInfo(message, details);
      //// MessageBoxProvider.ShowMessageBox(messageInfo);
    }

    ////private static MessageInfo GetMessageInfo(string message, string details)
    ////{
    ////  var messageInfo = new MessageInfo(message)
    ////  {
    ////    Caption = "ZTG.WPF.Dashboard.Main.ExceptionHandler.Error".TranslateText(),
    ////    Description = "ZTG.WPF.Dashboard.Main.ExceptionHandler.Description".TranslateText(),
    ////    Details = details,
    ////    Buttons = MessageBoxButton.OK,
    ////    Image = MessageBoxImage.Error
    ////  };

    ////  return messageInfo;
    ////}

    private static string GetExceptionMessage(Exception exception)
    {
      exception.ArgumentNotNull("exception");

      var aggregateException = exception as AggregateException;
      if (aggregateException != null)
      {
        return String.Join(", ", aggregateException.InnerExceptions.Select(e => e.Message));
      }

      return exception.Message;
    }

    private static string GetInnerExceptionMessage(Exception exception)
    {
      if ((exception == null) || (exception.InnerException == null))
      {
        return null;
      }

      var aggregateException = exception as AggregateException;
      if (aggregateException != null)
      {
        return FormatExceptionMessage(aggregateException.InnerExceptions);
      }

      aggregateException = exception.InnerException as AggregateException;
      if (aggregateException != null)
      {
        var sb = new StringBuilder(aggregateException.Message);
        sb.AppendLine();
        sb.AppendLine();
        sb.AppendLine(FormatExceptionMessage(aggregateException.InnerExceptions));
        return sb.ToString();
      }

      return FormatExceptionMessage(new List<Exception> { exception.InnerException });
    }

    private static string FormatExceptionMessage(IEnumerable<Exception> innerExceptions)
    {
      var sb = new StringBuilder();

      if (innerExceptions != null)
      {
        foreach (var innerException in innerExceptions)
        {
          sb.AppendLine("Exception message:");
          sb.AppendLineWithIndent(3, innerException.Message.Trim());

          sb.AppendLine();
          sb.AppendLine(innerException.StackTrace.Trim());
          sb.AppendLine();

          sb.AppendLine();
        }
      }

      return sb.ToString();
    }

    /// <summary>
    /// Dispose data containers
    /// </summary>
    protected virtual void Dispose(bool unmanaged)
    {
      Dispatcher.CurrentDispatcher.UnhandledException -= CurrentDispatcherUnhandledException;
      AppDomain.CurrentDomain.UnhandledException -= CurrentDomainUnhandledException;
      TaskScheduler.UnobservedTaskException -= TaskSchedulerUnobservedTaskException;
    }

    /// <summary>
    /// Dispose resources.
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }
}