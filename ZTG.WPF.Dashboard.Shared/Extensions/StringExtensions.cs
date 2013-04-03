//----------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;

using ZTG.WPF.Dashboard.Shared.Data;
using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Extensions
{
  /// <summary>
  ///   Extension methods for <see cref="string" />.
  /// </summary>
  public static class StringExtensions
  {
    /// <summary>
    ///   The quote start tag.
    /// </summary>
    private const string QuoteStartTag = "$[";

    /// <summary>
    ///   The quote end tag.
    /// </summary>
    private const string QuoteEndTag = "]$";

    /// <summary>
    ///   The default quote start.
    /// </summary>
    private const string DefaultQuoteStart = "'";

    /// <summary>
    ///   The default quote end.
    /// </summary>
    private const string DefaultQuoteEnd = "'";

    /// <summary>
    /// Determines whether the specified string is a number.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsNumber(this string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return false;
      }

      return value.All(char.IsNumber);
    }

    /// <summary>
    /// Converts a <see cref="string"/> into an <see cref="int"/>. Returns 0 if not possible.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "integer",
      Justification = "OK in this case")]
    [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults",
      MessageId = "System.Int32.TryParse(System.String,System.Int32@)", Justification = "OK in this case")]
    public static int ToInteger(this string value)
    {
      int result = 0;
      if (!value.IsNumber())
      {
        return result;
      }

      int.TryParse(value, out result);
      return result;
    }

    /// <summary>
    /// Removes all leading and trailing white-space characters from the current not null <see cref="string"/> object.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="assignTrimmedValue">
    /// The assign trimmed value action.
    /// </param>
    /// <exexample>StringObject.TrimAll(t =&gt; StringObject = t) </exexample>
    public static void TrimAll(this string value, Action<string> assignTrimmedValue)
    {
      assignTrimmedValue.ArgumentNotNull("assignTrimmedValue");

      if (value != null)
      {
        assignTrimmedValue(value.Trim());
      }
    }

    /// <summary>
    /// Trims the the prefix off the given string.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="prefixToTrim">
    /// The prefix to trim.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string TrimStart(this string value, string prefixToTrim)
    {
      if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(prefixToTrim)
          && value.StartsWith(prefixToTrim, StringComparison.CurrentCulture))
      {
        return value.Substring(prefixToTrim.Length);
      }

      return value;
    }

    /// <summary>
    /// Formats the quotes.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="quoteStart">
    /// The quote start.
    /// </param>
    /// <param name="quoteEnd">
    /// The quote end.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatQuotes(this string value, string quoteStart = DefaultQuoteStart, string quoteEnd = DefaultQuoteEnd)
    {
      value.TrimAll(t => value = t);

      if (string.IsNullOrWhiteSpace(value))
      {
        return null;
      }

      quoteStart = string.IsNullOrWhiteSpace(quoteStart) ? DefaultQuoteStart : quoteStart;
      quoteEnd = string.IsNullOrWhiteSpace(quoteEnd) ? DefaultQuoteEnd : quoteEnd;

      return value.Replace(QuoteStartTag, quoteStart).Replace(QuoteEndTag, quoteEnd);
    }

    public static IEnumerable<string> SplitIntoLines(this string text)
    {
      if (string.IsNullOrEmpty(text))
      {
        yield break;
      }

      var preparedText = text.Replace(Environment.NewLine, "\n").Replace('\r', '\n');
      foreach (var line in preparedText.Split('\n'))
      {
        yield return line;
      }
    }

    public static string FormatText(this string text, params object[] args)
    {
      if (string.IsNullOrEmpty(text))
      {
        return text;
      }

      return string.Format(CultureInfo.CurrentCulture, text, args);
    }

    public static string CleanIdentifier(this string identifier)
    {
      if (string.IsNullOrEmpty(identifier))
      {
        return identifier;
      }

      return identifier.Where(char.IsLetterOrDigit).Aggregate(string.Empty, (current, c) => current + c);
    }

    public static string Part(this string text, int partIndex, params char[] separators)
    {
      if (string.IsNullOrEmpty(text) || partIndex < 0)
      {
        return null;
      }

      if (separators == null || separators.Length == 0)
      {
        return partIndex == 0 ? text : null;
      }

      var parts = text.Split(separators);
      if (parts.Length > partIndex)
      {
        return parts[partIndex];
      }

      return null;
    }

    public static string TrimToFirstLineOffset(this string text)
    {
      text.ArgumentNotNull("text");

      var lines = text.SplitIntoLines().ToList();
      var firstLine = lines.FirstOrDefault();

      if (string.IsNullOrEmpty(firstLine) || !firstLine.StartsWith(" ", StringComparison.OrdinalIgnoreCase))
      {
        return text;
      }

      var hasEndingLineBreak = text.EndsWith("\n", StringComparison.OrdinalIgnoreCase) || text.EndsWith("\r", StringComparison.OrdinalIgnoreCase);

      var diff = text.Length - text.TrimStart(' ').Length;
      var trimmedText = new StringBuilder();
      foreach (var line in lines)
      {
        if (string.IsNullOrEmpty(line) || !line.StartsWith(" ", StringComparison.OrdinalIgnoreCase))
        {
          trimmedText.AppendLine(line);
          continue;
        }

        var trimmedLine = line.Length > diff ? line.Substring(0, diff).TrimStart(' ') + line.Substring(diff) : line.TrimStart(' ');
        trimmedText.AppendLine(trimmedLine);
      }

      return !hasEndingLineBreak ? trimmedText.ToString().TrimEnd('\n', '\r') : trimmedText.ToString();
    }

    /// <summary>
    /// Increases the specified number text by given inc and returns the result as string.
    /// </summary>
    /// <param name="numberText">The number text.</param>
    /// <param name="inc">The inc.</param>
    /// <returns></returns>
    public static string Increase(this string numberText, int inc = 1)
    {
      var number = DefaultStringConverter.ConvertToNumber(numberText);
      return DefaultStringConverter.FormatNumber(number + inc);
    }

    /// <summary>
    /// Decreases the specified number text by given inc and returns the result as string.
    /// </summary>
    /// <param name="numberText">The number text.</param>
    /// <param name="dec">The dec.</param>
    /// <returns></returns>
    public static string Decrease(this string numberText, int dec = 1)
    {
      return Increase(numberText, -dec);
    }
  }
}