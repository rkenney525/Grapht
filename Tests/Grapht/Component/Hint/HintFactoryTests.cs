using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleJSON;
using Grapht.Exception;

namespace Grapht.Component.Hint {
    [TestClass]
    public class HintFactoryTests {
        [TestMethod]
        public void ParseHintBrancheSize() {
            // Arrange
            JSONNode hintNode = JSONNode.Parse(@"{""name"": ""BranchSize"", ""arg"": 3}");

            // Act
            string hintString = HintFactory.ParseHint(hintNode);

            // Assert
            Assert.AreEqual("Try a branch sum of 3", hintString);
        }

        [TestMethod]
        [ExpectedException(typeof(GraphtParsingException))]
        public void ParseHintUnexpectedValue() {
            // Arrange
            JSONNode hintNode = JSONNode.Parse(@"{""name"": ""lolhax""}");

            // Act
            string hintString = HintFactory.ParseHint(hintNode);
        }
    }
}
