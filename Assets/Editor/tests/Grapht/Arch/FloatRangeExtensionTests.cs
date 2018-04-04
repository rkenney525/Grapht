using System.Linq;
using System.Collections.Generic;
using Grapht.Exception;

using NUnit.Framework;

namespace Grapht.Arch {
    public class FloatRangeExtensionTests {
        [Test]
        public void SmallToLargeRangeWithPositiveStep() {
            // Arrange
            Tuple<float, float> range = 1.0f.To(2.0f);
            ICollection<float> expected = new List<float> { 1.0f, 1.5f, 2.0f };

            // Act
            IEnumerable<float> steps = range.Step(0.5f);

            // Assert
            CollectionAssert.AreEqual(expected.ToList(), steps.ToList(), "The contents should be 1, 1.5, 2");
        }

        [Test]
        public void LargeToSmallRangeWithNegativeStep() {
            // Arrange
            Tuple<float, float> range = 2.0f.To(1.0f);
            ICollection<float> expected = new List<float> { 2.0f, 1.5f, 1.0f };

            // Act
            IEnumerable<float> steps = range.Step(-0.5f);

            // Assert
            CollectionAssert.AreEqual(expected.ToList(), steps.ToList(), "The contents should be 2, 1.5, 1");
        }

        [Test]
        [ExpectedException(typeof(BadRangeException))]
        public void LargeToSmallRangeWithPositiveStep() {
            // Arrange
            Tuple<float, float> range = 2.0f.To(1.0f);

            // Act
            IEnumerable<float> steps = range.Step(0.5f);
            // Because of how 'yield return' works, an exception won't be thrown until the collection is acted on
            steps.Count();
        }

        [Test]
        [ExpectedException(typeof(BadRangeException))]
        public void SmallToLargeRangeWithNegativeStep() {
            // Arrange
            Tuple<float, float> range = 1.0f.To(2.0f);

            // Act
            IEnumerable<float> steps = range.Step(-0.5f);
            // Because of how 'yield return' works, an exception won't be thrown until the collection is acted on
            steps.Count();
        }

        [Test]
        [ExpectedException(typeof(BadRangeException))]
        public void RangeWithZeroStep() {
            // Arrange
            Tuple<float, float> range = 1.0f.To(2.0f);

            // Act
            IEnumerable<float> steps = range.Step(0);
            // Because of how 'yield return' works, an exception won't be thrown until the collection is acted on
            steps.Count();
        }
    }
}
