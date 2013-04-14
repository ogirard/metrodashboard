// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DynamicTranslateExtension.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

using ZTG.WPF.Dashboard.Shared.Localization.WPF.Internal;

namespace ZTG.WPF.Dashboard.Shared.Localization.WPF
{
  /// <summary>
  /// Markup extension providing dynamic translation support.
  /// </summary>
  [ContentProperty("Path")]
  public class DynamicTranslateExtension : MarkupExtension
  {
    #region Construction

    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicTranslateExtension"/> class.
    /// </summary>
    /// <param name="path">The path.</param>
    public DynamicTranslateExtension(string path)
    {
      Path = path;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicTranslateExtension"/> class.
    /// </summary>
    public DynamicTranslateExtension()
      : this(null)
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the path to get the object used as value.
    /// </summary>
    /// <remarks>
    /// Property only used, if <see cref="ValueBinding"/> is not set.
    /// </remarks>
    /// <value>The path.</value>
    public string Path { get; set; }

    /// <summary>
    /// Gets or sets the value binding to get the object used value.
    /// </summary>
    /// <remarks>
    /// If this property is set, the <see cref="Path"/> property is not taken into account.
    /// </remarks>
    /// <value>The value binding.</value>
    public Binding ValueBinding { get; set; }

    /// <summary>
    /// Gets or sets the localization namespace which is used to construct the translation key.
    /// If any of the properties <see cref="KeyConverter"/>, <see cref="LocalizationNamespaceBinding"/> or <see cref="KeyConverterBinding"/> are set, this property is not used.
    /// The localization key is constructed like this: "{0}{1}" where {0} is this LocalizationNamespace and {1} is the ToString() 
    /// representatitive of the object.
    /// </summary>
    /// <value>The localization namespace.</value>
    public string LocalizationNamespace { get; set; }

    /// <summary>
    /// Gets or sets the localization namespace binding used to construct the translation key.
    /// </summary>
    /// <remarks>
    /// Property only used, if <see cref="KeyConverter"/> and <see cref="KeyConverterBinding"/>  are not set.
    /// </remarks>
    /// <value>The localization namespace binding.</value>
    public Binding LocalizationNamespaceBinding { get; set; }

    /// <summary>
    /// Gets or sets the key converter.
    /// If this property is set, the converter is called with the key object. 
    /// A string is expected to be returned which will be used as localization key.
    /// If this key converter also implements <see cref="ITranslatedStringFormatter.FormatString"/>, the translated string 
    /// will be routed through the method <see cref="ITranslatedStringFormatter"/>.
    /// </summary>
    /// <remarks>
    /// Property only used, if <see cref="ITranslatedStringFormatter"/> is not set.
    /// </remarks>
    /// <value>The key converter.</value>
    public IValueConverter KeyConverter { get; set; }

    /// <summary>
    /// Gets or sets the key converter binding used to get the key converter based on the key object.
    /// A string is expected to be returned by the converter which will be used as localization key.
    /// If the key converter also implements <see cref="ITranslatedStringFormatter"/>, the translated string 
    /// will be routed through the method <see cref="ITranslatedStringFormatter.FormatString"/>.
    /// </summary>
    /// <value>The key converter binding.</value>
    public Binding KeyConverterBinding { get; set; }

    #endregion

    #region Overrides

    /// <summary>
    /// See <see cref="MarkupExtension.ProvideValue"/>
    /// </summary>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      Binding valueBinding = ValueBinding ?? new Binding { Path = new PropertyPath(Path) };
      Binding localizationBinding = LocalizationNamespaceBinding ?? new Binding { Source = LocalizationNamespace };
      Binding keyConverterBinding = KeyConverterBinding ?? new Binding { Source = KeyConverter };

      return BindingProvider.CreateDynamicTranslationBinding(
          valueBinding,
          localizationBinding,
          keyConverterBinding).ProvideValue(serviceProvider);
    }

    #endregion
  }
}
