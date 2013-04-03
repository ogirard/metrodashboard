//----------------------------------------------------------------------------------------------------
// <copyright file="MergeStylesExtension.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Markup;

using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Extensions
{
  /// <summary>
  ///   Markup extension to allow the use of more than one style within XAML
  /// </summary>
  [MarkupExtensionReturnType(typeof(Style))]
  public class MergeStylesExtension : MarkupExtension
  {
    /// <summary>
    /// The _resource keys.
    /// </summary>
    private readonly string[] _resourceKeys;

    /// <summary>
    /// Initializes a new instance of the <see cref="MergeStylesExtension"/> class.
    /// </summary>
    /// <param name="inputResourceKeys">
    /// The input resource keys (comma separated list).
    /// </param>
    public MergeStylesExtension(string inputResourceKeys)
    {
      inputResourceKeys.ArgumentNotNull("inputResourceKeys");
      _resourceKeys = inputResourceKeys.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

      if (_resourceKeys.Length == 0)
      {
        throw new ArgumentException(@"No input resource keys specified.");
      }
    }

    /// <summary>
    /// Returns a style that merges all styles with the keys specified in the constructor.
    ///   The styles are applied from left to right, i.e. the right style overrides the left style
    /// </summary>
    /// <param name="serviceProvider">
    /// The service provider for this markup extension.
    /// </param>
    /// <returns>
    /// A style that merges all styles with the keys specified in the constructor.
    /// </returns>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      Style resultStyle = null;

      var provideValueTarget = serviceProvider as IProvideValueTarget;
      if (provideValueTarget != null)
      {
        var frameworkElement = provideValueTarget.TargetObject as FrameworkElement;
        var currentStyle = provideValueTarget.TargetProperty as Style;
        if (frameworkElement != null)
        {
          resultStyle = currentStyle;
        }
      }

      if (resultStyle == null)
      {
        resultStyle = new Style();
      }

      foreach (string currentResourceKey in _resourceKeys)
      {
        var currentStyle = new StaticResourceExtension(currentResourceKey).ProvideValue(serviceProvider) as Style;

        if (currentStyle == null)
        {
          throw new InvalidOperationException(@"Could not find style with resource key '" + currentResourceKey + "'.");
        }

        resultStyle.Merge(currentStyle);
      }

      return resultStyle;
    }
  }
}