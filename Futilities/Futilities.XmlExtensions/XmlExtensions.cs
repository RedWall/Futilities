﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Futilities.XmlExtensions
{
    public static class XmlExtensions
    {
        /// <summary>
        /// Safely get the string value from an XElement. Handles the null checking for you.
        /// </summary>
        /// <param name="element">this element</param>
        /// <param name="defaultValue">Value to return if element is null (default: null)</param>
        /// <returns>string value of XElement</returns>
        public static string GetValue(this XElement element, string defaultValue = null) => element != null ? element.Value : defaultValue;

        /// <summary>
        /// Safely get the string value from an XAttribute.Handles the null checking for you.
        /// </summary>
        /// <param name="element">this attribute</param>
        /// <param name="defaultValue">Value to return if element is null (default: null)</param>
        /// <returns>string value of XAttribute</returns>
        public static string GetValue(this XAttribute attribute, string defaultValue = null) => attribute != null ? attribute.Value : defaultValue;

        /// <summary>
        /// Safely get the value of an XElement. Handles the null checking for you.
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="element">this element</param>
        /// <param name="func">transform function for element</param>
        /// <param name="defaultValue">Value to return if element is null (default: default(T))</param>
        /// <returns>T value of XElement</returns>
        public static T GetValue<T>(this XElement element, Func<XElement, T> func, T defaultValue = default) => element != null ? func(element) : defaultValue;

        /// <summary>
        /// Safely get the value of an XAttribute. Handles the null checking for you.
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="element">this attribute</param>
        /// <param name="func">transform function for attribute</param>
        /// <param name="defaultValue">Value to return if attribute is null (default: default(T))</param>
        /// <returns>T value XAttribute</returns>
        public static T GetValue<T>(this XAttribute attribute, Func<XAttribute, T> func, T defaultValue = default) => attribute != null ? func(attribute) : defaultValue;

        /// <summary>
        /// Gets the value of the first text node within the XElement
        /// </summary>
        /// <param name="element">this attribute</param>
        /// <returns>The first text value or null if there are none</returns>
        public static string GetText(this XElement element)
        {
            return GetAllText(element).FirstOrDefault();
        }

        /// <summary>
        /// Gets the value of all text nodes within the XElement
        /// </summary>
        /// <param name="element">this attribute</param>
        /// <returns>Collection of text values</returns>
        public static IEnumerable<string> GetAllText(this XElement element)
        {
            foreach (var node in element.Nodes().Where(n => n.NodeType == System.Xml.XmlNodeType.Text))
            {
                yield return (node as XText).Value;
            }
        }
    }
}
