//----------------------------------------------------------------------------------------------------
// <copyright file="ValueConverterMarkupExtensionBase.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Windows.Markup;

namespace ZTG.WPF.Dashboard.Shared.Converter
{
  /// <summary>
  /// Base class for Value converters which are also a <see cref="MarkupExtension"/> returning themself as 
  /// value.
  /// Usage example:
  /// <![CDATA[
  /// <Button Visibility={Binding Converter={z:XYConverter}} />
  /// ]]>
  /// </summary>
  public abstract class ValueConverterMarkupExtensionBase : MarkupExtension
  {
    /// <summary>
    ///   <see cref="MarkupExtension.ProvideValue"/>
    /// </summary>
    /// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
    /// <returns>
    /// The object value to set on the property where the extension is applied.
    /// </returns>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      return this;
    }
  }
}