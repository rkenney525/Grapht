using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Grapht.Exception;
using SimpleJSON;
using Grapht.Node;

namespace Grapht.Component.Victory {
    [TestClass]
    public class VictoryConditionsTests {
        [TestMethod]
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
