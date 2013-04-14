// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringBuilderExtensions.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Extensions
{
  /// <summary>
  /// Extension methods for <see cref="StringBuilder"/>.
  /// </summary>
  public static class StringBuilderExtensions
  {
    #region AppendLine with indent

    public static StringBuilder AppendLineWithIndent(this StringBuilder builder, int indent, string value)
    {
      builder.ArgumentNotNull("builder");
      return builder.AppendLineWithIndent(new string(' ', indent), value);
    }

    public static StringBuilder AppendLineWithIndent(this StringBuilder builder, string indent, string value)
    {
      builder.ArgumentNotNull("builder");
      builder.Append(indent);
      return builder.AppendLine(value);
    }

    #endregion

    #region AppendFormatLine

    public static StringBuilder AppendFormatLine(this StringBuilder builder, string format, object arg0)
    {
      builder.ArgumentNotNull("builder");
      builder.AppendFormat(format, arg0);
      return builder.AppendLine();
    }

    public static StringBuilder AppendFormatLine(this StringBuilder builder, string format, object arg0, object arg1)
    {
      builder.ArgumentNotNull("builder");
      builder.AppendFormat(format, arg0, arg1);
      return builder.AppendLine();
    }

    public static StringBuilder AppendFormatLine(this StringBuilder builder, string format, object arg0, object arg1, object arg2)
    {
      builder.ArgumentNotNull("builder");
      builder.AppendFormat(format, arg0, arg1, arg2);
      return builder.AppendLine();
    }

    [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Text.StringBuilder.AppendFormat(System.String,System.Object[])", Justification = "This is a library class. Extension methods for all original methods implemented")]
    public static StringBuilder AppendFormatLine(this StringBuilder builder, string format, params object[] args)
    {
      builder.ArgumentNotNull("builder");
      builder.AppendFormat(format, args);
      return builder.AppendLine();
    }

    public static StringBuilder AppendFormatLine(this StringBuilder builder, IFormatProvider provider, string format, params object[] args)
    {
      builder.ArgumentNotNull("builder");
      builder.AppendFormat(provider, format, args);
      return builder.AppendLine();
    }

    #endregion

    #region AppendFormatLine with indent

    public static StringBuilder AppendFormatLineWithIndent(this StringBuilder builder, int indent, string format, object arg0)
    {
      builder.ArgumentNotNull("builder");
      return builder.AppendFormatLineWithIndent(new string(' ', indent), format, arg0);
    }

    public static StringBuilder AppendFormatLineWithIndent(this StringBuilder builder, int indent, string format, object arg0, object arg1)
    {
      builder.ArgumentNotNull("builder");
      return builder.AppendFormatLineWithIndent(new string(' ', indent), format, arg0, arg1);
    }

    public static StringBuilder AppendFormatLineWithIndent(this StringBuilder builder, int indent, string format, object arg0, object arg1, object arg2)
    {
      builder.ArgumentNotNull("builder");
      return builder.AppendFormatLineWithIndent(new string(' ', indent), format, arg0, arg1, arg2);
    }

    public static StringBuilder AppendFormatLineWithIndent(this StringBuilder builder, int indent, string format, params object[] args)
    {
      builder.ArgumentNotNull("builder");
      return builder.AppendFormatLineWithIndent(new string(' ', indent), format, args);
    }

    public static StringBuilder AppendFormatLineWithIndent(this StringBuilder builder, int indent, IFormatProvider provider, string format, params object[] args)
    {
      builder.ArgumentNotNull("builder");
      return builder.AppendFormatLineWithIndent(new string(' ', indent), provider, format, args);
    }

    public static StringBuilder AppendFormatLineWithIndent(this StringBuilder builder, string indent, string format, object arg0)
    {
      builder.ArgumentNotNull("builder");
      builder.Append(indent);
      return builder.AppendFormatLine(format, arg0);
    }

    public static StringBuilder AppendFormatLineWithIndent(this StringBuilder builder, string indent, string format, object arg0, object arg1)
    {
      builder.ArgumentNotNull("builder");
      builder.Append(indent);
      return builder.AppendFormatLine(format, arg0, arg1);
    }

    public static StringBuilder AppendFormatLineWithIndent(this StringBuilder builder, string indent, string format, object arg0, object arg1, object arg2)
    {
      builder.ArgumentNotNull("builder");
      builder.Append(indent);
      return builder.AppendFormatLine(format, arg0, arg1, arg2);
    }

    public static StringBuilder AppendFormatLineWithIndent(this StringBuilder builder, string indent, string format, params object[] args)
    {
      builder.ArgumentNotNull("builder");
      builder.Append(indent);
      return builder.AppendFormatLine(format, args);
    }

    public static StringBuilder AppendFormatLineWithIndent(this StringBuilder builder, string indent, IFormatProvider provider, string format, params object[] args)
    {
      builder.ArgumentNotNull("builder");
      builder.Append(indent);
      return builder.AppendFormatLine(provider, format, args);
    }

    #endregion
  }
}
