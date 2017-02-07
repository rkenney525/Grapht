﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Grapht.Arch {
    /// <summary>
    /// Contains extension methods for IEnumerable and friends
    /// </summary>
    public static class EnumerableExtension {
        /// <summary>
        /// Perform some action on all items in an enumerable.
        /// </summary>
        /// <typeparam name="T">The type of iteratee</typeparam>
        /// <param name="enumerable">The IEnumerable to iterate over</param>
        /// <param name="action">The action to perform on each iteratee</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) {
            enumerable.All(item => {
                action.Invoke(item);
                return true;
            });
        }
    }
}
