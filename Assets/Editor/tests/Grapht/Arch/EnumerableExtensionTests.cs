using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace Grapht.Arch {
    public class EnumerableExtensionTests {
        [Test]
        public void ForEach() {
            // Arrange
            IList<int> list = new List<int>(new int[] { 1, 2, 3, 4, 5 });
            int expected = list.Aggregate((agg, item) => agg + item);
            int sum = 0;

            // Act
            list.ForEach(item => {
                sum += item;
            });

            // Assert
            Assert.AreEqual(expected, sum, "The aggregate implementation should increment the sum value to 15");
        }
    }
}
