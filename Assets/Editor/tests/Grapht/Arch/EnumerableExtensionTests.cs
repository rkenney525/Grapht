using System.Collections.Generic;

using NUnit.Framework;

namespace Grapht.Arch {
    public class EnumerableExtensionTests {
        [Test]
        public void ForEach() {
            // Arrange
            IList<int> list = new List<int>(new int[] { 1, 2, 3, 4, 5 });
            int expected = 15;
            int sum = 0;

            // Act
            list.ForEach(item => {
                sum += item;
            });

            // Assert
            Assert.AreEqual(expected, sum, "The summing implementation should increment the sum value to 15");
        }
    }
}
