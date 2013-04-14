//----------------------------------------------------------------------------------------------------
// <copyright file="XmlExtensions.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Xml.Linq;

using ZTG.WPF.Dashboard.Shared.Data;
using ZTG.WPF.Dashboard.Shared.Utilities;

namespace ZTG.WPF.Dashboard.Shared.Extensions
{
  /// <summary>
  ///   The xml extensions.
  /// </summary>
  public static class XmlExtensions
  {
    /// <summary>
    /// Replaces the given element with the replacing elements.
    /// </summary>
    /// <param name="elementToReplace">
    /// The element to replace.
    /// </param>
    /// <param name="replacingElements">
    /// The replacing elements.
    /// </param>
    public static void ReplaceElement(this XElement elementToReplace, params XElement[] replacingElements)
    {
      elementToReplace.ArgumentNotNull("elementToReplace");

      if (replacingElements != null)
      {
        foreach (XElement replacingElement in replacingElements)
        {
          elementToReplace.AddBeforeSelf(replacingElement);
        }
      }

      elementToReplace.Remove();
    }

    #region    Element Handling

    /// <summary>
    /// Gets the value of the given child element.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="child">
    /// The child.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if child element with given name does not exist
    /// </exception>
    public static string GetElementValue(this XContainer element, XName child)
    {
      return GetElement(element, child).Value;
    }

    /// <summary>
    /// Gets the value of the given child element as <see cref="Int32"/>.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="child">
    /// The child.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if child element with given name does not exist or value is of wrong type
    /// </exception>
    public static int GetElementNumberValue(this XContainer element, XName child)
    {
      return DefaultStringConverter.ConvertToNumber(GetElementValue(element, child));
    }

    /// <summary>
    /// Gets the value of the given child element as <see cref="UInt32"/>.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="child">
    /// The child.
    /// </param>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if child element with given name does not exist or value is of wrong type
    /// </exception>
    [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "unsigned",
      Justification = "We want this name")]
    public static uint GetElementUnsignedNumberValue(this XContainer element, XName child)
    {
      return DefaultStringConverter.ConvertToUnsignedNumber(GetElementValue(element, child));
    }

    /// <summary>
    /// Gets the value of the given child element as <see cref="Int32"/> from hex string.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="child">
    /// The child.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if child element with given name does not exist or value is of wrong type
    /// </exception>
    public static int GetElementNumberFromHexValue(this XContainer element, XName child)
    {
      return DefaultStringConverter.ConvertToNumberFromHex(GetElementValue(element, child));
    }

    /// <summary>
    /// Gets the value of the given child element as <see cref="Double"/>.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="child">
    /// The child.
    /// </param>
    /// <returns>
    /// The <see cref="double"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if child element with given name does not exist or value is of wrong type
    /// </exception>
    public static double GetElementDoubleValue(this XContainer element, XName child)
    {
      return DefaultStringConverter.ConvertToDouble(GetElementValue(element, child));
    }

    /// <summary>
    /// Gets the value of the given child element as <see cref="Boolean"/>.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="child">
    /// The child.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if child element with given name does not exist or value is of wrong type
    /// </exception>
    public static bool GetElementBooleanValue(this XContainer element, XName child)
    {
      return DefaultStringConverter.ConvertToBoolean(GetElementValue(element, child));
    }

    /// <summary>
    /// Gets the value of the given child element as a bytearray.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="child">
    /// The child.
    /// </param>
    /// <returns>
    /// The byte array
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if child element with given name does not exist or value is of wrong type
    /// </exception>
    public static byte[] GetElementBinaryValue(this XContainer element, XName child)
    {
      return GetElementValue(element, child).Replace(" ", string.Empty).ParseHexBytes();
    }

    /// <summary>
    /// Gets the value of the given child element as a bytearray from base64 string.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="child">
    /// The child.
    /// </param>
    /// <returns>
    /// The byte array
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if child element with given name does not exist or value is of wrong type
    /// </exception>
    public static byte[] GetElementBase64Value(this XContainer element, XName child)
    {
      return DefaultStringConverter.ConvertToBinaryBase64(GetElementValue(element, child).Replace(" ", string.Empty));
    }

    /// <summary>
    /// Gets the value of the given child element as <see cref="DateTime"/> (dd.MM.yyyy date expected).
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="child">
    /// The child.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if child element with given name does not exist or value is of wrong type
    /// </exception>
    public static DateTime GetElementDateValue(this XContainer element, XName child, string format = "dd.MM.yyyy")
    {
      return DefaultStringConverter.ConvertToDate(GetElementValue(element, child), format);
    }

    /// <summary>
    /// Gets the value of the given child element as <see cref="DateTime"/> (dd.MM.yyyy HH:mm:ss date time expected)
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="child">
    /// The child.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if child element with given name does not exist or value is of wrong type
    /// </exception>
    public static DateTime GetElementDateTimeValue(
      this XContainer element, XName child, string format = "dd.MM.yyyy HH:mm:ss")
    {
      return DefaultStringConverter.ConvertToDateTime(GetElementValue(element, child), format);
    }

    /// <summary>
    /// Gets the value of the given child element as <see cref="Guid"/>.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="child">
    /// The child.
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if child element with given name does not exist or value is of wrong type
    /// </exception>
    public static Guid GetElementGuidValue(this XContainer element, XName child)
    {
      return DefaultStringConverter.ConvertToGuid(GetElementValue(element, child));
    }

    /// <summary>
    /// Gets the value of the given child element as <see cref="Guid"/>.
    /// </summary>
    /// <typeparam name="TEnum">
    /// The type of the enum to be parsed.
    /// </typeparam>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="child">
    /// The child.
    /// </param>
    /// <returns>
    /// The <see cref="TEnum"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if child element with given name does not exist or value is of wrong type
    /// </exception>
    public static TEnum GetElementEnumValue<TEnum>(this XContainer element, XName child) where TEnum : struct
    {
      return DefaultStringConverter.ConvertToEnum<TEnum>(GetElementValue(element, child));
    }

    /// <summary>
    /// Gets the child element.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="child">
    /// The child.
    /// </param>
    /// <returns>
    /// The <see cref="XElement"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if no child with the given name exists
    /// </exception>
    public static XElement GetElement(this XContainer element, XName child)
    {
      element.ArgumentNotNull("element");
      child.ArgumentNotNull("child");

      XElement childElement = element.Element(child);

      if (childElement == null)
      {
        throw new ArgumentException(
          string.Format(CultureInfo.InvariantCulture, "Child element with name '{0}' does not exist.", child));
      }

      return childElement;
    }

    /// <summary>
    /// Checks if the given element has a child element with the given name.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="child">
    /// The child.
    /// </param>
    /// <returns>
    /// <c>true</c> if the specified element has element; otherwise, <c>false</c>.
    /// </returns>
    public static bool HasElement(this XContainer element, XName child)
    {
      element.ArgumentNotNull("element");
      child.ArgumentNotNull();
      return element.Element(child) != null;
    }

    #endregion Element Handling

    #region    Attribute Handling

    /// <summary>
    /// Gets the value of the given attribute.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="attributeName">
    /// The name of the attribute.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if attribute with given name does not exist
    /// </exception>
    public static string GetAttributeValue(this XElement element, XName attributeName)
    {
      return GetAttribute(element, attributeName).Value;
    }

    /// <summary>
    /// Gets the value of the given attribute as <see cref="Int32"/>.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="attributeName">
    /// The name of the attribute.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if attribute with given name does not exist or value is of wrong type
    /// </exception>
    public static int GetAttributeNumberValue(this XElement element, XName attributeName)
    {
      return DefaultStringConverter.ConvertToNumber(GetAttributeValue(element, attributeName));
    }

    /// <summary>
    /// Gets the value of the given attribute as <see cref="Int32"/> from hex value.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="attributeName">
    /// The name of the attribute.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if attribute with given name does not exist or value is of wrong type
    /// </exception>
    public static int GetAttributeNumberFromHexValue(this XElement element, XName attributeName)
    {
      return DefaultStringConverter.ConvertToNumberFromHex(GetAttributeValue(element, attributeName));
    }

    /// <summary>
    /// Gets the value of the given attribute as <see cref="Double"/>.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="attributeName">
    /// The name of the attribute.
    /// </param>
    /// <returns>
    /// The <see cref="double"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if attribute with given name does not exist or value is of wrong type
    /// </exception>
    public static double GetAttributeDoubleValue(this XElement element, XName attributeName)
    {
      return DefaultStringConverter.ConvertToDouble(GetAttributeValue(element, attributeName));
    }

    /// <summary>
    /// Gets the value of the given attribute as <see cref="Boolean"/>.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="attributeName">
    /// The name of the attribute.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if attribute with given name does not exist or value is of wrong type
    /// </exception>
    public static bool GetAttributeBooleanValue(this XElement element, XName attributeName)
    {
      return DefaultStringConverter.ConvertToBoolean(GetAttributeValue(element, attributeName));
    }

    /// <summary>
    /// Gets the value of the given attribute as <see cref="DateTime"/>.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="attributeName">
    /// The name of the attribute.
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if attribute with given name does not exist or value is of wrong type
    /// </exception>
    public static DateTime GetAttributeDateTimeValue(this XElement element, XName attributeName)
    {
      return DefaultStringConverter.ConvertToDateTime(GetAttributeValue(element, attributeName));
    }

    /// <summary>
    /// Gets the value of the given attribute as <see cref="Guid"/>.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="attributeName">
    /// The name of the attribute.
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if attribute with given name does not exist or value is of wrong type
    /// </exception>
    public static Guid GetAttributeGuidValue(this XElement element, XName attributeName)
    {
      return DefaultStringConverter.ConvertToGuid(GetAttributeValue(element, attributeName));
    }

    /// <summary>
    /// Gets the value of the given attribute as <see cref="TEnum"/>.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="attributeName">
    /// The name of the attribute.
    /// </param>
    /// <returns>
    /// The <see cref="TEnum"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if attribute with given name does not exist or value is of wrong type
    /// </exception>
    public static TEnum GetAttributeEnumValue<TEnum>(this XElement element, XName attributeName) where TEnum : struct
    {
      return DefaultStringConverter.ConvertToEnum<TEnum>(GetAttributeValue(element, attributeName));
    }

    /// <summary>
    /// Gets the attribute.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="attributeName">
    /// The name of the attribute.
    /// </param>
    /// <returns>
    /// The <see cref="XAttribute"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if no attribute with the given name exists
    /// </exception>
    public static XAttribute GetAttribute(this XElement element, XName attributeName)
    {
      element.ArgumentNotNull("element");
      attributeName.ArgumentNotNull();

      XAttribute attribute = element.Attribute(attributeName);
      if (attribute == null)
      {
        throw new ArgumentException(
          string.Format(CultureInfo.InvariantCulture, "Attribute with name {0} does not exist.", attributeName));
      }

      return attribute;
    }

    /// <summary>
    /// Checks if the given element has an attribute with the given name.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="attributeName">
    /// The name of the attribute.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool HasAttribute(this XElement element, string attributeName)
    {
      element.ArgumentNotNull("element");
      attributeName.ArgumentNotNullOrEmpty("attributeName");

      XAttribute attribute = element.Attribute(attributeName);
      return attribute != null;
    }

    #endregion Attribute Handling

    #region    Naming

    /// <summary>
    /// Converts the given name to a element name with lower character at start
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string AsXmlElementName(this string name)
    {
      if (string.IsNullOrEmpty(name))
      {
        return name;
      }

      char firstChar = char.ToLowerInvariant(name[0]);

      return name.Length > 1 ? firstChar + name.Substring(1) : firstChar.ToString(CultureInfo.InvariantCulture);
    }

    #endregion Naming
  }
}