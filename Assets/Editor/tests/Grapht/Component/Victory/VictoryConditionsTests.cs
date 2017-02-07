using NUnit.Framework;
using Grapht.Exception;
using Grapht.Node;
using Grapht.Arch;
using SimpleJSON;
using UnityEngine;
using System.Collections.Generic;

namespace Grapht.Component.Victory {
    public class VictoryConditionsTests {
        
        private static GameObject NodeRef = Resources.Load<GameObject>("prefabs/Moveable Node");

        [Test]
        [ExpectedException(typeof(GraphtParsingException))]
        public void ParseVictoryConditionUnexpectedValue() {
            // Arrange
            JSONNode hintNode = JSONNode.Parse(@"{""name"": ""lolololhax""}");

            // Act
            VictoryConditions.ParseVictoryCondition(hintNode);
        }

        // TODO finish writing tests, need to address MonoBehavior mocking
        
        [Test]
        public void ParseVictoryConditionMaxDepthWithSingleTreeBelowBar() {
            // Arrange
            JSONNode hintNode = JSONNode.Parse(@"{""name"": ""MaxDepth"", ""arg"": 3}");
            TreeNodeScript t1 = CreateTreeNodeScript();
            TreeNodeScript t2 = CreateTreeNodeScript();
            t1.AddNewChild(t2);
            IList<TreeNodeScript> nodes = new List<TreeNodeScript>(new TreeNodeScript[] { t1, t2 });

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(hintNode);

            // Assert
            Assert.True(vc.Apply(nodes), "The condition is met since the max depth is 2 and the max is 3");
        }

        private TreeNodeScript CreateTreeNodeScript() {
            GameObject node = Object.Instantiate(NodeRef);
            node.GetComponent<UnityEntity>().Awake();
            return node.GetComponent<TreeNodeScript>();
        }
    }
}
