//----------------------------------------------------------------------------------------------------
// <copyright file="XmlTranslationProvider.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Localization
{
  /// <summary>
  ///   See <see cref="ITranslationProvider" />
  /// </summary>
  public class XmlTranslationProvider : ITranslationProvider
  {
    #region Private Members

    /// <summary>
    /// The localize item string.
    /// </summary>
    private const string LocalizeItemString = "LocalizeItem";

    /// <summary>
    /// The _valid languages.
    /// </summary>
    private static HashSet<string> _validLanguages;

    /// <summary>
    /// The _resource set dictionary.
    /// </summary>
    private readonly Dictionary<string, Dictionary<string, string>> _resourceSetDictionary =
      new Dictionary<string, Dictionary<string, string>>(StringComparer.CurrentCultureIgnoreCase);

    #endregion

    #region Construction

    /// <summary>
    /// Initializes a new instance of the <see cref="XmlTranslationProvider"/> class.
    /// </summary>
    /// <param name="fileName">
    /// Name of the file.
    /// </param>
    public XmlTranslationProvider(string fileName)
    {
      LoadLocalizedStringsFromXml(fileName);
      LoadLanguage(CultureInfo.CurrentUICulture);
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets the valid languages.
    /// </summary>
    /// <value>The valid languages.</value>
    private static HashSet<string> ValidLanguages
    {
      get
      {
        if (_validLanguages == null)
        {
          _validLanguages = new HashSet<string>(StringComparer.CurrentCultureIgnoreCase);

          foreach (CultureInfo cultureInfo in CultureInfo.GetCultures(CultureTypes.NeutralCultures))
          {
            _validLanguages.Add(cultureInfo.TwoLetterISOLanguageName);
          }
        }

        return _validLanguages;
      }
    }

    /// <summary>
    ///   Gets or sets the current resource set.
    /// </summary>
    /// <value>The current resource set.</value>
    private Dictionary<string, string> CurrentResourceSet { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Loads the localized strings from XML.
    /// </summary>
    /// <param name="fileName">
    /// Name of the file.
    /// </param>
    private void LoadLocalizedStringsFromXml(string fileName)
    {
      if (!File.Exists(fileName))
      {
        return;
      }

      _resourceSetDictionary.Clear();

      // Get the Two Letter Language
      var xmlFileInfo = new FileInfo(fileName);
      string[] fileNameSplits = xmlFileInfo.Name.Split('.');

      var language = TranslationManager.DefaultLanguage;

      if (fileNameSplits.Length > 1)
      {
        language = fileNameSplits[fileNameSplits.Length - 2];

        if (language.Length > 2)
        {
          // No valid two letter language -> use English
          language = TranslationManager.DefaultLanguage;
        }
      }

      if (!ValidLanguages.Contains(language))
      {
        return;
      }

      // Open stream and reader
      using (var streamReader = new StreamReader(fileName))
      {
        var reader = new XmlTextReader(streamReader) { WhitespaceHandling = WhitespaceHandling.None };

        // Read xml content
        reader.MoveToContent();
        if (reader.ReadToDescendant(LocalizeItemString))
        {
          do
          {
            string id = reader.GetAttribute("id");
            string text = reader.ReadString();

            if (!string.IsNullOrEmpty(id))
            {
              if (!string.IsNullOrEmpty(language))
              {
                if (!string.IsNullOrEmpty(text))
                {
                  // Found translated text -> add it to dictionary
                  Dictionary<string, string> resourceSet;
                  if (!_resourceSetDictionary.TryGetValue(language, out resourceSet))
                  {
                    resourceSet = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
                    _resourceSetDictionary.Add(language, resourceSet);
                  }

                  string dummyText;
                  if (!resourceSet.TryGetValue(id, out dummyText))
                  {
                    resourceSet.Add(id, text);
                  }
                }
              }
            }
          }
          while (reader.ReadToNextSibling(LocalizeItemString));
        }
      }
    }

    #endregion

    #region ITranslationProvider Members

    /// <summary>
    ///   Gets the supported languages.
    /// </summary>
    /// <value>The supported languages.</value>
    public IEnumerable<CultureInfo> SupportedLanguages
    {
      get
      {
        return _resourceSetDictionary.Keys.Select(lang => new CultureInfo(lang));
      }
    }

    /// <summary>
    /// Loads the language.
    /// </summary>
    /// <param name="language">
    /// The language.
    /// </param>
    public void LoadLanguage(CultureInfo language)
    {
      language.ArgumentNotNull("language");

      Dictionary<string, string> resourceSet;
      _resourceSetDictionary.TryGetValue(language.TwoLetterISOLanguageName, out resourceSet);
      CurrentResourceSet = resourceSet;
    }

    /// <summary>
    /// Translates the specified key.
    /// </summary>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    public object Translate(string key)
    {
      if (CurrentResourceSet == null)
      {
        return null;
      }

      string text;
      CurrentResourceSet.TryGetValue(key, out text);
      return text;
    }

    #endregion
  }
}