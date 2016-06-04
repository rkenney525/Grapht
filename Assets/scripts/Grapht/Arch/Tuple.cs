using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grapht.Arch {
    /// <summary>
    /// Equivalent of the .NET 4 Tuple class, made since Unity is stuck in .NET 3.5 presently.
    /// </summary>
    /// <typeparam name="Type1">The type of the first argument</typeparam>
    /// <typeparam name="Type2">The type of the second argument</typeparam>
    public class Tuple<Type1, Type2> {
        /// <summary>
        /// The first object in the tuple
        /// </summary>
        public Type1 _1 { get; private set; }

        /// <summary>
        /// The second object in the tuple
        /// </summary>
        public Type2 _2 { get; private set; }

        /// <summary>
        /// Create a Tuple instance
        /// </summary>
        /// <param name="_1">The first argument</param>
        /// <param name="_2">The second argument</param>
        internal Tuple(Type1 _1, Type2 _2) {
            this._1 = _1;
            this._2 = _2;
        }
    }

    /// <summary>
    /// Companion class with create functionality
    /// </summary>
    public static class Tuple {
        /// <summary>
        /// Create a new Tuple instance
        /// </summary>
        /// <typeparam name="Type1">The type of the first argument</typeparam>
        /// <typeparam name="Type2">The type of the second argument</typeparam>
        /// <param name="first">The first argument</param>
        /// <param name="second">The second argument</param>
        /// <returns>A Tuple containing the two passed in objects</returns>
        public static Tuple<Type1, Type2> New<Type1, Type2>(Type1 first, Type2 second) {
            return new Tuple<Type1, Type2>(first, second);
        }
    }
}
