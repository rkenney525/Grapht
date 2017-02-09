using NUnit.Framework;
using Grapht.Exception;
using Grapht.Node;
using Grapht.Arch;
using SimpleJSON;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Grapht.Component.Victory {
    [TestFixture]
    public class VictoryConditionsTests {
        
        private static GameObject NodeRef = Resources.Load<GameObject>("prefabs/Moveable Node");

        private IList<TreeNodeScript> sum10Depth3Branch1Nodes;

        private IList<TreeNodeScript> sum10Depth2Branch2Nodes;

        private IList<TreeNodeScript> sumVariesDepth2Branch2Nodes;

        [TestFixtureSetUp]
        public void CreateNodeLists() {
            // Tree one - sum10Depth3Branch1Nodes
            //    0 --- 5 --- 5
            TreeNodeScript treeOneRoot = CreateTreeNodeScript();
            TreeNodeScript treeOneNode1 = CreateTreeNodeScript();
            TreeNodeScript treeOneNode2 = CreateTreeNodeScript();
            SetValue(treeOneRoot, 0);
            SetValue(treeOneNode1, 5);
            SetValue(treeOneNode2, 5);
            treeOneRoot.AddNewChild(treeOneNode1);
            treeOneNode1.AddNewChild(treeOneNode2);
            sum10Depth3Branch1Nodes = new List<TreeNodeScript>(new TreeNodeScript[] { treeOneRoot, treeOneNode1, treeOneNode2 });

            // Tree two - sumVariesDepth2Branch1Nodes
            //   1 --- 5
            //     \-- 4
            TreeNodeScript treeTwoRoot = CreateTreeNodeScript();
            TreeNodeScript treeTwoNode1 = CreateTreeNodeScript();
            TreeNodeScript treeTwoNode2 = CreateTreeNodeScript();
            SetValue(treeTwoRoot, 1);
            SetValue(treeTwoNode1, 5);
            SetValue(treeTwoNode2, 4);
            treeTwoRoot.AddNewChild(treeTwoNode1);
            treeTwoRoot.AddNewChild(treeTwoNode2);
            sumVariesDepth2Branch2Nodes = new List<TreeNodeScript>(new TreeNodeScript[] { treeTwoRoot, treeTwoNode1, treeTwoNode2 });

            // Tree three - sum10Depth2Branch2Nodes
            //   0 --- 10
            //     \-- 10
            TreeNodeScript treeThreeRoot = CreateTreeNodeScript();
            TreeNodeScript treeThreeNode1 = CreateTreeNodeScript();
            TreeNodeScript treeThreeNode2 = CreateTreeNodeScript();
            SetValue(treeThreeRoot, 0);
            SetValue(treeThreeNode1, 10);
            SetValue(treeThreeNode2, 10);
            treeThreeRoot.AddNewChild(treeThreeNode1);
            treeThreeRoot.AddNewChild(treeThreeNode2);
            sum10Depth2Branch2Nodes = new List<TreeNodeScript>(new TreeNodeScript[] { treeThreeRoot, treeThreeNode1, treeThreeNode2 });
        }

        [Test]
        [ExpectedException(typeof(GraphtParsingException))]
        public void ParseVictoryConditionUnexpectedValue() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""lolololhax""}");

            // Act
            VictoryConditions.ParseVictoryCondition(conditionNode);
        }

        [Test]
        public void ParseVictoryConditionMaxDepthWithSingleTreeBelowBar() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""MaxDepth"", ""arg"": 4}");

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.True(vc.Apply(sum10Depth3Branch1Nodes), "The condition is met since the max depth is 3 and the limit is 4");
        }

        [Test]
        public void ParseVictoryConditionMaxDepthWithSingleTreeAtBar() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""MaxDepth"", ""arg"": 3}");

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.True(vc.Apply(sum10Depth3Branch1Nodes), "The condition is met since the max depth is 3 and the limit is 3");
        }

        [Test]
        public void ParseVictoryConditionMaxDepthWithSingleTreeAboveBar() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""MaxDepth"", ""arg"": 1}");

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.False(vc.Apply(sum10Depth3Branch1Nodes), "The condition is not met since the max depth is 3 and the limit is 1");
        }

        [Test]
        public void ParseVictoryConditionMaxDepthWithMultiTreeBelowBar() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""MaxDepth"", ""arg"": 4}");
            IList<TreeNodeScript> nodes = sum10Depth3Branch1Nodes.Concat(sumVariesDepth2Branch2Nodes).ToList();

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.True(vc.Apply(nodes), "The condition is met since the max depth is 3 and the limit is 4");
        }

        [Test]
        public void ParseVictoryConditionMaxDepthWithMultiTreeAtBar() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""MaxDepth"", ""arg"": 3}");
            IList<TreeNodeScript> nodes = sum10Depth3Branch1Nodes.Concat(sumVariesDepth2Branch2Nodes).ToList();

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.True(vc.Apply(nodes), "The condition is met since the max depth is 3 and the limit is 3");
        }

        [Test]
        public void ParseVictoryConditionMaxDepthWithMultiTreeAboveBar() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""MaxDepth"", ""arg"": 1}");
            IList<TreeNodeScript> nodes = sum10Depth3Branch1Nodes.Concat(sumVariesDepth2Branch2Nodes).ToList();

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.False(vc.Apply(nodes), "The condition is not met since the max depth is 3 and the limit is 1");
        }

        [Test]
        public void ParseVictoryConditionSameSumBranchSingleTreeNotMet() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""SameSumBranch""}");

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.False(vc.Apply(sumVariesDepth2Branch2Nodes), "The condition is not met since the two branches have differing sums");
        }

        [Test]
        public void ParseVictoryConditionSameSumBranchSingleTreeMet() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""SameSumBranch""}");

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.True(vc.Apply(sum10Depth3Branch1Nodes), "The condition is met since the two branches have the same sum");
        }

        [Test]
        public void ParseVictoryConditionSameSumBranchMultiTreeNotMet() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""SameSumBranch""}");
            IList<TreeNodeScript> nodes = sum10Depth3Branch1Nodes.Concat(sumVariesDepth2Branch2Nodes).ToList();

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.False(vc.Apply(nodes), "The condition is not met since the three branches have differing sums");
        }

        [Test]
        public void ParseVictoryConditionSameSumBranchMultiTreeMet() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""SameSumBranch""}");
            IList<TreeNodeScript> nodes = sum10Depth3Branch1Nodes.Concat(sum10Depth2Branch2Nodes).ToList();

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.True(vc.Apply(nodes), "The condition is met since the three branches have the same sum");
        }

        /// <summary>
        /// Create a Moveable Node and return the TreeNodeScript from it
        /// </summary>
        /// <returns>A TreeNodeScript for a Moveable Node</returns>
        private TreeNodeScript CreateTreeNodeScript() {
            GameObject node = Object.Instantiate(NodeRef);
            node.GetComponent<UnityEntity>().Awake();
            return node.GetComponent<TreeNodeScript>();
        }

        /// <summary>
        /// Sets the branch value for the given TreeNodeScript
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        private void SetValue(TreeNodeScript node, int value) {
            node.GetComponent<NumericValueScript>().SetValue(value);
        }
    }
}
