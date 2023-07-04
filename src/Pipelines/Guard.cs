using System;
using System.Collections.Generic;
using System.Linq;

namespace Flight.Extensions.Primitives
{
    /// <summary>
    /// Defines argument validation based utility methods.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Ensures specified argument is not null.
        /// </summary>
        /// <typeparam name="T">The type of argument to validate.</typeparam>
        /// <param name="value">The argument value.</param>
        /// <param name="name">The argument name.</param>
        /// <returns>The argument value.</returns>
        public static T ArgumentNotNull<T>(T value, string name) where T:class
        {
            return value ?? throw new ArgumentNullException(name);
        }

        /// <summary>
        /// Ensures specified argument is not null or empty.
        /// </summary>
        /// <typeparam name="T">The element type of argument collection to validate.</typeparam>
        /// <param name="value">The argument value.</param>
        /// <param name="name">The argument name.</param>
        /// <returns>The argument value.</returns>
        public static IEnumerable<T> ArgumentNotNullOrEmpty<T>(IEnumerable<T> value, string name)
        {
            ArgumentNotNull(value, name);
            if (!value.Any())
            {
                throw new ArgumentException($"Argument value cannot be an empty collection", name);
            }
            return value;
        }

        /// <summary>
        /// Ensures specified argument is not null or white space.
        /// </summary>
        /// <param name="value">The argument value.</param>
        /// <param name="name">The argument name.</param>
        /// <returns>The argument value.</returns>
        public static string ArgumentNotNullOrWhiteSpace(string value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Argument value cannot be null or a white space string.", name);
            }
            return value;
        }
    }
}
