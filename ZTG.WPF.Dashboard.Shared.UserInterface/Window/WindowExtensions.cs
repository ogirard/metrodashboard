// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowExtensions.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Interop;

using ZTG.WPF.Dashboard.Shared.Native;
using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.UserInterface.Window
{
  /// <summary>
  /// Provides extension for <see cref="Window"/>
  /// </summary>
  public static class WindowExtensions
  {
    /////// <summary>
    /////// Opens the dialog.
    /////// </summary>
    /////// <param name="window">The window.</param>
    /////// <param name="navigationInfo">The navigation info or <tt>null</tt> if not available.</param>
    /////// <param name="showCloseButton">if set to <c>true</c> will show the title bar's close button.</param>
    ////[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "TCG.Common.Utilities.WPF.NativeMethods.SetForegroundWindow(System.IntPtr)", Justification = "We don't need the handle. (reviewed: ys)")]
    ////public static void OpenDialog(this System.Windows.Window window, INavigationInfo navigationInfo, bool showCloseButton = false)
    ////{
    ////  window.ArgumentNotNull("window");

    ////  window.ShowInTaskbar = false;

    ////  var owner = navigationInfo != null && navigationInfo.CurrentStep != null ? navigationInfo.CurrentStep.OwnerWindowHandle : IntPtr.Zero;

    ////  RoutedEventHandler windowLoadedHandler = null;

    ////  if (owner != IntPtr.Zero)
    ////  {
    ////    // set owner
    ////    var wiop = new WindowInteropHelper(window);
    ////    wiop.Owner = owner;

    ////    // disable owner
    ////    NativeMethods.EnableWindow(owner, false);

    ////    if (window.WindowStartupLocation == WindowStartupLocation.CenterOwner)
    ////    {
    ////      windowLoadedHandler = new RoutedEventHandler((s, e) => WindowLoadedHandler(owner, window));
    ////      window.Loaded += windowLoadedHandler;
    ////    }
    ////  }
    ////  else
    ////  {
    ////    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
    ////    window.ShowInTaskbar = true;
    ////  }

    ////  // push new top window
    ////  if (navigationInfo != null)
    ////  {
    ////    navigationInfo.PushStep(new NavigationStep(window.GetHandle()));
    ////  }

    ////  // show dialog window
    ////  window.ShowDialog();

    ////  // pop closed window
    ////  if (navigationInfo != null)
    ////  {
    ////    navigationInfo.PopStep();
    ////  }

    ////  if (owner != IntPtr.Zero)
    ////  {
    ////    if (windowLoadedHandler != null)
    ////    {
    ////      window.Loaded -= windowLoadedHandler;
    ////    }

    ////    // re-enable owner
    ////    NativeMethods.EnableWindow(owner, true);
    ////    NativeMethods.SetForegroundWindow(owner);
    ////  }
    ////}

    /// <summary>
    /// Gets the window handle of the given window.
    /// </summary>
    /// <param name="window">The window.</param>
    /// <returns></returns>
    public static IntPtr GetHandle(this System.Windows.Window window)
    {
      var windowInterop = new WindowInteropHelper(window);
      windowInterop.EnsureHandle();
      return windowInterop.Handle;
    }

    private static void WindowLoadedHandler(IntPtr ownerWindow, System.Windows.Window window)
    {
      if ((ownerWindow != IntPtr.Zero) && (window != null))
      {
        NativeMethods.RECT ownerRect;
        NativeMethods.GetWindowRect(ownerWindow, out ownerRect);

        window.WindowStartupLocation = WindowStartupLocation.Manual;
        window.Left = ownerRect.Left + ((ownerRect.Width - window.Width) / 2);
        window.Top = Math.Max(0, ownerRect.Top + ((ownerRect.Height - window.Height) / 2));   // Always show the top point of the window
      }
    }
  }
}