using System;
using NUnit.Framework;

namespace Grapht.Arch {
    public class TupleTests {
        [Test]
        public void CreateNewTuple() {
            // Arrange
            string elem1 = "BofaDeez";
            string elem2 = "Nuts";

            // Act
            Tuple<string, string> res = Tuple.New(elem1, elem2);

            // Assert
            Assert.AreEqual(elem1, res._1, "The first parameter should be accessed via _1");
            Assert.AreEqual(elem2, res._2, "The second parameter should be accessed via _2");
        }
    }
}
