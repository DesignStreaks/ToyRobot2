/*
 * DESIGNSTREAKS CONFIDENTIAL
 * __________________
 *
 *  Copyright © DesignStreaks - 2010 - 2020
 *  All Rights Reserved.
 *
 * NOTICE:  All information contained herein is, and remains
 * the property of DesignStreaks and its suppliers, if any.
 * The intellectual and technical concepts contained
 * herein are proprietary to DesignStreaks and its suppliers and may
 * be covered by Australian, U.S. and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from DesignStreaks.
 */

namespace System
{
    using System;
    using System.Diagnostics;

    public static class StringExtensions
    {
        /// <summary>Converts the string representation of the enumeration name or underlying value to an equivalent enumerated object.</summary>
        /// <typeparam name="T">The enumeration type to which to convert <paramref name="value" />.</typeparam>
        /// <param name="value">The string representation of the enumeration name or underlying value to convert.</param>
        /// <returns>The enumeration object matching the <paramref name="value" />.</returns>
        /// <exception cref="System.ArgumentException">T must be an Enumerable Type</exception>
        /// <exception cref="System.ArgumentException">Enum type '{T}' does not contain the requested value `{value}`</exception>
        /// <exception cref="System.NullReferenceException">Value is null or empty.</exception>
        [DebuggerHidden]
        public static T ToEnum<T>(this string value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an Enumerable Type");

            if (string.IsNullOrEmpty(value))
                throw new NullReferenceException("Value is null or empty.");

            T a;
            if (!Enum.TryParse<T>(value, true, out a))
                throw new ArgumentException($"Enum type '{typeof(T).Name}' does not contain the requested value `{value}`.");

            return a;
        }
    }
}