using NUnit.Framework;
using Grapht.Exception;
using SimpleJSON;

namespace Grapht.Component.Victory {
    public class VictoryConditionsTests {
        [Test]
        [ExpectedException(typeof(GraphtParsingException))]
        public void ParseVictoryConditionUnexpectedValue() {
            // Arrange
            JSONNode hintNode = JSONNode.Parse(@"{""name"": ""lolololhax""}");

            // Act
            VictoryConditions.ParseVictoryCondition(hintNode);
        }

        // TODO finish writing tests, need to address MonoBehavior mocking
        
        public void ParseVictoryConditionMaxDepth() {
            // Arrange
            JSONNode hintNode = JSONNode.Parse(@"{""name"": ""MaxDepth""}");
            // TODO make a bunch of theseTreeNodeScript t = new TreeNodeScript();

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(hintNode);

            // Assert

        }
    }
}
