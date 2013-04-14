// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslateExtension.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Markup;

namespace ZTG.WPF.Dashboard.Shared.Localization.WPF
{
  /// <summary>
  /// Markup extension to translate texts in XAML
  /// </summary>
  public class TranslateExtension : MarkupExtension
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="TranslateExtension"/> class.
    /// </summary>
    public TranslateExtension()
    {
    }

    /// <summary>
    /// Gets or sets the key.
    /// </summary>
    /// <value>The key.</value>
    [ConstructorArgument("Key")]
    public string Key { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TranslateExtension"/> class.
    /// </summary>
    /// <param name="key">The key.</param>
    public TranslateExtension(string key)
    {
      Key = key;
    }

    /// <summary>
    /// See <see cref="MarkupExtension.ProvideValue"/>
    /// </summary>
    /// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
    /// <returns>
    /// The object value to set on the property where the extension is applied.
    /// </returns>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      var binding = TranslateBindingExtension.GetTranslateBinding(Key);
      return binding.ProvideValue(serviceProvider);
    }
  }
}
