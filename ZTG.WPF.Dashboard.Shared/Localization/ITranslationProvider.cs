//----------------------------------------------------------------------------------------------------
// <copyright file="ITranslationProvider.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Globalization;

namespace ZTG.WPF.Dashboard.Shared.Localization
{
  /// <summary>
  ///   See <see cref="ITranslationProvider" />
  /// </summary>
  public interface ITranslationProvider
  {
    /// <summary>
    ///   Gets the supported languages.
    /// </summary>
    /// <value>The supported languages.</value>
    IEnumerable<CultureInfo> SupportedLanguages { get; }

    /// <summary>
    /// Translates the specified key.
    /// </summary>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    object Translate(string key);

    /// <summary>
    /// Loads the language.
    /// </summary>
    /// <param name="language">
    /// The language.
    /// </param>
    void LoadLanguage(CultureInfo language);
  }
}