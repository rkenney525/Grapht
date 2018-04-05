using NUnit.Framework;
using SimpleJSON;
using Grapht.Exception;

namespace Grapht.Component.Hint {
    public class HintFactoryTests {
        [Test]
        public void ParseHintBrancheSize() {
            // Arrange
            JSONNode hintNode = JSONNode.Parse(@"{""name"": ""BranchSize"", ""arg"": 3}");

            // Act
            string hintString = HintFactory.ParseHint(hintNode);

            // Assert
            Assert.AreEqual("Try a branch sum of 3", hintString);
        }

        [Test]
        public void ParseHintUnexpectedValue() {
            // Arrange
            JSONNode hintNode = JSONNode.Parse(@"{""name"": ""lolhax""}");

            // Act
            Assert.Throws<GraphtParsingException>(() => HintFactory.ParseHint(hintNode));
        }
    }
}
