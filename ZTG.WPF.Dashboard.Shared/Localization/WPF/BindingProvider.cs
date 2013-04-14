// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BindingProvider.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Data;

using ZTG.WPF.Dashboard.Shared.Localization.WPF.Internal;
using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Localization.WPF
{
  /// <summary>
  /// Class providing functionality to create a <see cref="Binding"/> for a specific translation key.
  /// </summary>
  public static class BindingProvider
  {
    private static readonly PropertyPath PropertyPath = new PropertyPath("TranslatedValue");

    /// <summary>
    /// Creates a translation binding with given manager and key.
    /// </summary>
    public static Binding CreateTranslationBinding(string key)
    {
      return CreateTranslationBinding(key, null);
    }

    /// <summary>
    /// Creates a translation binding with given manager and key.
    /// </summary>
    public static Binding CreateTranslationBinding(string key, IValueConverter valueConverter)
    {
      key.ArgumentNotNull("key");
      var binding = new Binding
      {
        Source = new TranslationSource(key),
        Mode = BindingMode.OneWay,
        Path = PropertyPath
      };
      if (valueConverter != null)
      {
        binding.Converter = valueConverter;
      }

      return binding;
    }

    /// <summary>
    /// Creates the dynamic translation binding.
    /// </summary>
    public static MultiBinding CreateDynamicTranslationBinding(
        Binding valueBinding,
        Binding localizationNamespaceBinding,
        Binding keyConverterBinding)
    {
      valueBinding.ArgumentNotNull("valueBinding");
      localizationNamespaceBinding.ArgumentNotNull("localizationNamespaceBinding");
      keyConverterBinding.ArgumentNotNull("keyConverterBinding");

      // The translation source is only used as trigger for a language change
      var source = new TranslationSource(null);

      var multiBinding = new MultiBinding { Mode = BindingMode.OneWay };

      multiBinding.Bindings.Add(valueBinding);
      multiBinding.Bindings.Add(localizationNamespaceBinding);
      multiBinding.Bindings.Add(keyConverterBinding);

      // This binding is only used as trigger for language change
      multiBinding.Bindings.Add(new Binding { Path = PropertyPath, Source = source });

      multiBinding.Converter = new DynamicObjectToTranslationConverter();

      return multiBinding;
    }
  }
}
