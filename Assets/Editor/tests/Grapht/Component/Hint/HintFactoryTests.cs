using NUnit.Framework;
using SimpleJSON;
using Grapht.Exception;

namespace Grapht.Component.Hint {
    public class HintFactoryTests {
        [Test]
        public void ParseHintBranchSize() {
            // Arrange
            JSONNode hintNode = JSONNode.Parse(@"{""name"": ""BranchSize"", ""arg"": 3}");

            // Act
            string hintString = HintFactory.ParseHint(hintNode);

            // Assert
            Assert.AreEqual("Try a branch sum of 3", hintString);
        }

        [Test]
        [ExpectedException(typeof(GraphtParsingException))]
        public void ParseHintUnexpectedValue() {
            // Arrange
            JSONNode hintNode = JSONNode.Parse(@"{""name"": ""lolhax""}");

            // Act
            HintFactory.ParseHint(hintNode);
        }
    }
}
