//----------------------------------------------------------------------------------------------------
// <copyright file="TranslationManager.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

using ZTG.WPF.Dashboard.Shared.Extensions;
using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Localization
{
  /// <summary>
  ///   Manages translations
  /// </summary>
  public static class TranslationManager
  {
    public static string DefaultLanguage = "en";

    #region Private Members

    /// <summary>
    /// The translation providers.
    /// </summary>
    private static readonly IList<ITranslationProvider> TranslationProviders = new List<ITranslationProvider>();

    #endregion

    #region Public Events and Properties

    /// <summary>
    ///   Gets or sets the current language.
    /// </summary>
    /// <value>The language.</value>
    public static CultureInfo Language
    {
      get
      {
        return Thread.CurrentThread.CurrentUICulture;
      }

      set
      {
        if (value != null && !Thread.CurrentThread.CurrentUICulture.Equals(value))
        {
          Thread.CurrentThread.CurrentUICulture = value;
          OnCurrentLanguageChanged();
        }
      }
    }

    /// <summary>
    ///   Gets the supported languages.
    /// </summary>
    /// <value>The supported languages.</value>
    public static IEnumerable<CultureInfo> SupportedLanguages
    {
      get
      {
        IEnumerable<CultureInfo> supportedLanguages = from provider in TranslationProviders
                                                      from language in provider.SupportedLanguages
                                                      select language;
        return supportedLanguages.Distinct();
      }
    }

    /// <summary>
    ///   Occurs when the language changed
    /// </summary>
    public static event EventHandler LanguageChanged;

    /// <summary>
    ///   Initalize the TranslationManager.
    /// </summary>
    public static void InitTranslationManager()
    {
      try
      {
        // Load the Language for the user.
        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en"); // Default

        string languageDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Language";

        if (Directory.Exists(languageDirectory))
        {
          foreach (string path in Directory.GetFiles(languageDirectory))
          {
            var translationProvider = new XmlTranslationProvider(path);
            RegisterTranslationProvider(translationProvider);
          }
        }
      }
      catch (CultureNotFoundException)
      {
        // Load default culture
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(DefaultLanguage);
      }
    }

    /// <summary>
    /// Registers the translation provider.
    /// </summary>
    /// <param name="provider">
    /// The provider.
    /// </param>
    public static void RegisterTranslationProvider(ITranslationProvider provider)
    {
      if (TranslationProviders.Contains(provider) == false)
      {
        TranslationProviders.Add(provider);
      }
    }

    /// <summary>
    /// Gets the translation.
    /// </summary>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    public static object Translate(string key)
    {
      foreach (ITranslationProvider provider in TranslationProviders)
      {
        object translatedValue = provider.Translate(key);
        if (translatedValue != null)
        {
          var translatedString = translatedValue as string;
          if (translatedString != null)
          {
            return translatedString.FormatQuotes();
          }

          return translatedValue;
        }
      }

      return string.Concat("!", key, "!");
    }

    /// <summary>
    /// Checks a previously translated string for marks that indicate an unsuccessful translation.
    /// </summary>
    /// <param name="translatedValue">
    /// The translated string.
    /// </param>
    /// <returns>
    /// True if translation seems successful, otherwise false.
    /// </returns>
    public static bool WasTranslationSuccessful(string translatedValue)
    {
      return !string.IsNullOrEmpty(translatedValue) && !translatedValue.StartsWith("!", StringComparison.Ordinal)
             && !translatedValue.EndsWith("!", StringComparison.Ordinal);
    }

    /// <summary>
    /// Gets the translation as <see cref="string"/> of the specified key.
    /// </summary>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <param name="args">
    /// The arguments to be passed to <see cref="string.Format(string,object)"/>. This will insert replace the {x}-tags in the translated text
    /// </param>
    /// <returns>
    /// The translated text
    /// </returns>
    public static string TranslateText(this string key, params object[] args)
    {
      if (args != null && args.Length > 0)
      {
        return string.Format(CultureInfo.CurrentCulture, Translate<string>(key), args);
      }

      return Translate<string>(key);
    }

    /// <summary>
    /// Appends the translated line.
    /// </summary>
    /// <param name="builder">
    /// The builder.
    /// </param>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    public static void AppendTranslatedLine(this StringBuilder builder, string key, params object[] args)
    {
      builder.ArgumentNotNull("builder");

      builder.AppendLine(key.TranslateText(args));
    }

    /// <summary>
    /// Appends the translated.
    /// </summary>
    /// <param name="builder">
    /// The builder.
    /// </param>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    public static void AppendTranslated(this StringBuilder builder, string key, params object[] args)
    {
      builder.ArgumentNotNull("builder");

      builder.Append(key.TranslateText(args));
    }

    /// <summary>
    /// Gets the typed translation of the specified key.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <returns>
    /// The localized resource matching the given key and type, or <c>default(T)</c> if not found or not of the correct type
    /// </returns>
    public static T Translate<T>(string key)
    {
      object translation = Translate(key);

      if (translation is T)
      {
        return (T)translation;
      }

      return default(T);
    }

    /// <summary>
    /// Gets the log message.
    /// </summary>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <param name="args">
    /// The arguments to be passed to <see cref="string.Format(string,object)"/>
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetLogMessage(string key, params object[] args)
    {
      return TranslateText(key, args);
    }

    #endregion

    #region Private Helpers

    /// <summary>
    ///   Called when [current language changed].
    /// </summary>
    private static void OnCurrentLanguageChanged()
    {
      // Load the new language dictionary
      LoadResources(CultureInfo.CurrentUICulture);

      if (LanguageChanged != null)
      {
        LanguageChanged(null, null);
      }
    }

    /// <summary>
    /// Loads the resources.
    /// </summary>
    /// <param name="language">
    /// The language.
    /// </param>
    private static void LoadResources(CultureInfo language)
    {
      foreach (ITranslationProvider provider in TranslationProviders)
      {
        provider.LoadLanguage(language);
      }
    }

    #endregion
  }
}