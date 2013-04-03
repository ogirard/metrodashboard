//----------------------------------------------------------------------------------------------------
// <copyright file="FileSystemHelper.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ZTG.WPF.Dashboard.Shared.Utilities
{
  /// <summary>
  ///   Provides helpers for filesystem interaction
  /// </summary>
  public static class FileSystemHelper
  {
    /// <summary>
    ///   Gets the current location (of the executing assembly).
    /// </summary>
    public static string CurrentLocation
    {
      get
      {
        return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      }
    }

    /// <summary>
    ///   Gets the desktop folder of the currently logged in user.
    /// </summary>
    /// <returns></returns>
    public static string DesktopFolder
    {
      get
      {
        return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
      }
    }

    /// <summary>
    /// Cleans the given filename.
    /// </summary>
    /// <param name="fileName">
    /// Filename to be cleaned.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string CleanFileName(string fileName)
    {
      return Path.GetInvalidFileNameChars()
                 .Aggregate(fileName, (current, invalidChar) => current.Replace(invalidChar, '_'));
    }
  }
}