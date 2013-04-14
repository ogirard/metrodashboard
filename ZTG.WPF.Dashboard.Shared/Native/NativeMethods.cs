// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Windows;

namespace ZTG.WPF.Dashboard.Shared.Native
{
  /// <summary>
  ///   Provides p/invoke access to native methods
  /// </summary>
  public static class NativeMethods
  {
    [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STYLE", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "GWL", Justification = "P/Invoke (reviewed: ys)")]
    public const int GWL_STYLE = -16;

    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SYSCOMMAND", Justification = "P/Invoke")]
    [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "P/Invoke")]
    public const int WM_SYSCOMMAND = 0x112;

    [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SYSMENU", Justification = "P/Invoke (reviewed: ys)")]
    public const int WS_SYSMENU = 0x80000;

    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "GETMINMAXINFO", Justification = "P/Invoke")]
    [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "P/Invoke (reviewed: ys)")]
    public const int WM_GETMINMAXINFO = 0x0024;

    [SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "P/Invoke")]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RECT", Justification = "P/Invoke (reviewed: ys)")]
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct RECT
    {
      public static readonly RECT Empty = new RECT();

      [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
      public int Left;

      [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
      public int Top;

      [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
      public int Right;

      [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "P/Invoke")]
      public int Bottom;

      public int Width
      {
        get
        {
          return Math.Abs(Right - Left);
        }
      }

      public int Height
      {
        get
        {
          return Bottom - Top;
        }
      }

      public RECT(int left, int top, int right, int bottom)
      {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
      }

      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Src", Justification = "P/Invoke")]
      public RECT(RECT rcSrc)
      {
        Left = rcSrc.Left;
        Top = rcSrc.Top;
        Right = rcSrc.Right;
        Bottom = rcSrc.Bottom;
      }

      public bool IsEmpty
      {
        get
        {
          return Left >= Right || Top >= Bottom;
        }
      }

      public override string ToString()
      {
        if (this == Empty)
        {
          return "RECT {Empty}";
        }

        return "RECT { left : " + Left + " / top : " + Top + " / right : " + Right + " / bottom : " + Bottom + " }";
      }

      public override bool Equals(object obj)
      {
        return obj is RECT && this == (RECT)obj;
      }

      public override int GetHashCode()
      {
        return Left.GetHashCode() + Top.GetHashCode() + Right.GetHashCode() + Bottom.GetHashCode();
      }

      public static bool operator ==(RECT rect1, RECT rect2)
      {
        return rect1.Left == rect2.Left && rect1.Top == rect2.Top && rect1.Right == rect2.Right && rect1.Bottom == rect2.Bottom;
      }

      public static bool operator !=(RECT rect1, RECT rect2)
      {
        return !(rect1 == rect2);
      }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT
    {
      public int x;

      public int y;

      [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "P/Invoke")]
      public POINT(int x, int y)
      {
        this.x = x;
        this.y = y;
      }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MINMAXINFO
    {
      public POINT ptReserved;
      public POINT ptMaxSize;
      public POINT ptMaxPosition;
      public POINT ptMinTrackSize;
      public POINT ptMaxTrackSize;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct MONITORINFO
    {
      public int cbSize;
      public RECT rcMonitor;
      public RECT rcWork;
      public uint dwFlags;

      public void Init()
      {
        cbSize = Marshal.SizeOf(typeof(MONITORINFO));
        rcMonitor = new RECT();
        rcWork = new RECT();
        dwFlags = 0;
      }
    }

    [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible", Justification = "It's a util project (reviewed: ys)")]
    [DllImport("kernel32.dll")]
    public static extern int GetCurrentThreadId();

    [SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible", Justification = "It's a util project (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "h", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Wnd", Justification = "P/Invoke (reviewed: ys)")]
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool EnableWindow(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bEnable);

    [SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible", Justification = "It's a util project (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "h", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "f", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Wnd", Justification = "P/Invoke (reviewed: ys)")]
    [DllImport("user32.dll", SetLastError = true)]
    public static extern void SwitchToThisWindow(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool fAltTab);

    [SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible", Justification = "It's a util project (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "h", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Wnd", Justification = "P/Invoke (reviewed: ys)")]
    [DllImport("user32.dll", SetLastError = true)]
    public static extern int SetForegroundWindow(IntPtr hWnd);

    [SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible", Justification = "It's a util project (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "h", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Wnd", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "n", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "CmdShow", Justification = "P/Invoke (reviewed: ys)")]
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommand nCmdShow);

    [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Keep all native type and method definitions in a single place (reviewed: ys)")]
    public enum ShowWindowCommand
    {
      /// <summary>
      ///   Windows 2000/XP: Minimizes a window, even if the thread that owns the window is not responding.
      ///   This flag should only be used when minimizing windows from a different thread.
      /// </summary>
      [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SW", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "FORCEMINIMIZE", Justification = "P/Invoke (reviewed: ys)")]
      SW_FORCEMINIMIZE = 11,

      /// <summary>
      ///   Hides the window and activates another window.
      /// </summary>
      [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SW", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HIDE", Justification = "P/Invoke (reviewed: ys)")]
      SW_HIDE = 0,

      /// <summary>
      ///   Maximizes the specified window.
      /// </summary>
      [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SW", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "MAXIMIZE", Justification = "P/Invoke (reviewed: ys)")]
      SW_MAXIMIZE = 3,

      /// <summary>
      ///   Minimizes the specified window and activates the next top-level window in the Z order.
      /// </summary>
      [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SW", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "MINIMIZE", Justification = "P/Invoke (reviewed: ys)")]
      SW_MINIMIZE = 6,

      /// <summary>
      ///   Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position.
      ///   An application should specify this flag when restoring a minimized window.
      /// </summary>
      [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SW", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RESTORE", Justification = "P/Invoke (reviewed: ys)")]
      SW_RESTORE = 9,

      /// <summary>
      ///   Activates the window and displays it in its current size and position.
      /// </summary>
      [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SW", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SHOW", Justification = "P/Invoke (reviewed: ys)")]
      SW_SHOW = 5,

      /// <summary>
      ///   Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess
      ///   function by the program that started the application.
      /// </summary>
      [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SW", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SHOWDEFAULT", Justification = "P/Invoke (reviewed: ys)")]
      SW_SHOWDEFAULT = 10,

      /// <summary>
      ///   Activates the window and displays it as a maximized window.
      /// </summary>
      [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SW", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SHOWMAXIMIZED", Justification = "P/Invoke (reviewed: ys)")]
      SW_SHOWMAXIMIZED = 3,

      /// <summary>
      ///   Activates the window and displays it as a minimized window.
      /// </summary>
      [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SW", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SHOWMINIMIZED", Justification = "P/Invoke (reviewed: ys)")]
      SW_SHOWMINIMIZED = 2,

      /// <summary>
      ///   Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, except the window is not activated.
      /// </summary>
      [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SW", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SHOWMINNOACTIVE", Justification = "P/Invoke (reviewed: ys)")]
      SW_SHOWMINNOACTIVE = 7,

      /// <summary>
      ///   Displays the window in its current size and position. This value is similar to SW_SHOW, except the window is not activated.
      /// </summary>
      [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SW", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SHOWNA", Justification = "P/Invoke (reviewed: ys)")]
      SW_SHOWNA = 8,

      /// <summary>
      ///   Displays a window in its most recent size and position. This value is similar to SW_SHOWNORMAL, except the window is not actived.
      /// </summary>
      [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "P/Invokev")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SW", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SHOWNOACTIVATE", Justification = "P/Invoke (reviewed: ys)")]
      SW_SHOWNOACTIVATE = 4,

      /// <summary>
      ///   Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position.
      ///   An application should specify this flag when displaying the window for the first time.
      /// </summary>
      [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SW", Justification = "P/Invoke (reviewed: ys)")]
      [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SHOWNORMAL", Justification = "P/Invoke (reviewed: ys)")]
      SW_SHOWNORMAL = 1,
    }

    [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "It's a util project (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "w", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "l", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Param", Justification = "P/Invoke (reviewed: ys)")]
    public delegate int CenterMessageCallBackDelegate(int message, int wParam, int lParam);

    [SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible", Justification = "It's a util project (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "h", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "dw", Justification = "P/Invoke (reviewed: ys)")]
    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr SetWindowsHookEx(int hook, CenterMessageCallBackDelegate callback, IntPtr hMod, int dwThreadId);

    [SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible", Justification = "It's a util project (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "hhk", Justification = "P/Invokev")]
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool UnhookWindowsHookEx(int hhk);

    [SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible", Justification = "It's a util project (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "long", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "n", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "h", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Wnd", Justification = "P/Invoke (reviewed: ys)")]
    [DllImport("user32.dll", SetLastError = true)]
    public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible", Justification = "It's a util project (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "long", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "n", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "h", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "dw", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Wnd", Justification = "P/Invoke (reviewed: ys)")]
    [DllImport("user32.dll", SetLastError = true)]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible", Justification = "It's a util project (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "u", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "h", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "cx", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Wnd", Justification = "P/Invoke (reviewed: ys)")]
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetWindowPos(int hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);

    [SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible", Justification = "It's a util project (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "h", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Wnd", Justification = "P/Invoke (reviewed: ys)")]
    [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification = "P/Invoke (reviewed: ys)")]
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Wnd", Justification = "P/Invoke")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "w", Justification = "P/Invoke")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Param", Justification = "P/Invoke")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "l", Justification = "P/Invoke")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "h", Justification = "P/Invoke")]
    [SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible", Justification = "P/Invoke")]
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    [SuppressMessage("Microsoft.Interoperability", "CA1414:MarkBooleanPInvokeArgumentsWithMarshalAs", Justification = "P/Invoke")]
    [DllImport("user32")]
    internal static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

    [DllImport("User32")]
    internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Wm", Justification = "P/Invoke")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Param", Justification = "P/Invoke")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "l", Justification = "P/Invoke")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "hwnd", Justification = "P/Invoke")]
    public static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam, Size minimumSize)
    {
      var mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

      // Adjust the maximized size and position to fit the work area of the correct monitor
      const int MONITOR_DEFAULTTONEAREST = 0x00000002;
      var monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

      if (monitor != IntPtr.Zero)
      {
        var monitorInfo = new MONITORINFO();
        monitorInfo.Init();
        GetMonitorInfo(monitor, ref monitorInfo);
        var rcWorkArea = monitorInfo.rcWork;
        var rcMonitorArea = monitorInfo.rcMonitor;
        mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.Left - rcMonitorArea.Left);
        mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.Top - rcMonitorArea.Top);
        mmi.ptMaxSize.x = Math.Abs(rcWorkArea.Right - rcWorkArea.Left);
        mmi.ptMaxSize.y = Math.Abs(rcWorkArea.Bottom - rcWorkArea.Top);
        mmi.ptMinTrackSize.x = (int)minimumSize.Width;
        mmi.ptMinTrackSize.y = (int)minimumSize.Height;
      }

      Marshal.StructureToPtr(mmi, lParam, true);
    }
  }
}