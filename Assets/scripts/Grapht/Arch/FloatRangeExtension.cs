using Grapht.Exception;
using System;
using System.Collections.Generic;

namespace Grapht.Arch {
    /// <summary>
    /// Extension methods for floats, used primarily for creating ranges
    /// </summary>
    public static class FloatRangeExtension {
        /// <summary>
        /// Create a basic range of two floats stored in a tuple
        /// </summary>
        /// <param name="num">The invoking float, and min</param>
        /// <param name="other">The passed in float, and max</param>
        /// <returns>A Tuple representing a range of the two floats</returns>
        public static Tuple<float, float> To(this float num, float other) {
            return Tuple.New(num, other);
        }

        /// <summary>
        /// Create the sequence of floats in a specified range offset by step
        /// </summary>
        /// <param name="range">The range of floats to enumerate</param>
        /// <param name="step">The number to skip for each float</param>
        /// <returns>A sequence of floats in the specified range</returns>
        public static IEnumerable<float> Step(this Tuple<float, float> range, float step) {
            // The range is bad if the step is 0 or if it wont increment towards the second number
            if (step == 0 || !((range._1 - range._2 >= 0) ^ (step >= 0))) {
                throw new BadRangeException();
            }
            for (float num = range._1; (range._1 < range._2) ? num <= range._2 : num >= range._2; num += step) {
                yield return num;
            }
        }
    }
}
