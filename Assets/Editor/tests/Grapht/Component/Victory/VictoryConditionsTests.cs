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
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""lolololhax""}");

            // Act
            VictoryConditions.ParseVictoryCondition(conditionNode);
        }

        [Test]
        public void ParseVictoryConditionMaxDepthWithSingleTreeBelowBar() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""MaxDepth"", ""arg"": 3}");
            TreeNodeScript t1 = CreateTreeNodeScript();
            TreeNodeScript t2 = CreateTreeNodeScript();
            t1.AddNewChild(t2);
            IList<TreeNodeScript> nodes = new List<TreeNodeScript>(new TreeNodeScript[] { t1, t2 });

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.True(vc.Apply(nodes), "The condition is met since the max depth is 2 and the limit is 3");
        }

        [Test]
        public void ParseVictoryConditionMaxDepthWithSingleTreeAtBar() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""MaxDepth"", ""arg"": 2}");
            TreeNodeScript t1 = CreateTreeNodeScript();
            TreeNodeScript t2 = CreateTreeNodeScript();
            t1.AddNewChild(t2);
            IList<TreeNodeScript> nodes = new List<TreeNodeScript>(new TreeNodeScript[] { t1, t2 });

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.True(vc.Apply(nodes), "The condition is met since the max depth is 2 and the limit is 2");
        }

        [Test]
        public void ParseVictoryConditionMaxDepthWithSingleTreeAboveBar() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""MaxDepth"", ""arg"": 1}");
            TreeNodeScript t1 = CreateTreeNodeScript();
            TreeNodeScript t2 = CreateTreeNodeScript();
            t1.AddNewChild(t2);
            IList<TreeNodeScript> nodes = new List<TreeNodeScript>(new TreeNodeScript[] { t1, t2 });

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.False(vc.Apply(nodes), "The condition is not met since the max depth is 2 and the limit is 1");
        }

        [Test]
        public void ParseVictoryConditionMaxDepthWithMultiTreeBelowBar() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""MaxDepth"", ""arg"": 3}");
            TreeNodeScript t1 = CreateTreeNodeScript();
            TreeNodeScript t2 = CreateTreeNodeScript();
            TreeNodeScript t3 = CreateTreeNodeScript();
            t1.AddNewChild(t2);
            IList<TreeNodeScript> nodes = new List<TreeNodeScript>(new TreeNodeScript[] { t1, t2, t3 });

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.True(vc.Apply(nodes), "The condition is met since the max depth is 2 and the limit is 3");
        }

        [Test]
        public void ParseVictoryConditionMaxDepthWithMultiTreeAtBar() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""MaxDepth"", ""arg"": 2}");
            TreeNodeScript t1 = CreateTreeNodeScript();
            TreeNodeScript t2 = CreateTreeNodeScript();
            TreeNodeScript t3 = CreateTreeNodeScript();
            t1.AddNewChild(t2);
            IList<TreeNodeScript> nodes = new List<TreeNodeScript>(new TreeNodeScript[] { t1, t2, t3 });

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.True(vc.Apply(nodes), "The condition is met since the max depth is 2 and the limit is 2");
        }

        [Test]
        public void ParseVictoryConditionMaxDepthWithMultiTreeAboveBar() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""MaxDepth"", ""arg"": 1}");
            TreeNodeScript t1 = CreateTreeNodeScript();
            TreeNodeScript t2 = CreateTreeNodeScript();
            TreeNodeScript t3 = CreateTreeNodeScript();
            t1.AddNewChild(t2);
            IList<TreeNodeScript> nodes = new List<TreeNodeScript>(new TreeNodeScript[] { t1, t2, t3 });

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.False(vc.Apply(nodes), "The condition is not met since the max depth is 2 and the limit is 1");
        }

        [Test]
        public void ParseVictoryConditionSameSumBranchSingleTreeNotMet() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""SameSumBranch""}");
            TreeNodeScript t1 = CreateTreeNodeScript();
            TreeNodeScript t2 = CreateTreeNodeScript();
            TreeNodeScript t3 = CreateTreeNodeScript();
            SetValue(t2, 4);
            SetValue(t3, 5);
            t1.AddNewChild(t2);
            t1.AddNewChild(t3);
            IList<TreeNodeScript> nodes = new List<TreeNodeScript>(new TreeNodeScript[] { t1, t2, t3 });

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.False(vc.Apply(nodes), "The condition is not met since the two branches have differing sums");
        }

        [Test]
        public void ParseVictoryConditionSameSumBranchSingleTreeMet() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""SameSumBranch""}");
            TreeNodeScript t1 = CreateTreeNodeScript();
            TreeNodeScript t2 = CreateTreeNodeScript();
            TreeNodeScript t3 = CreateTreeNodeScript();
            SetValue(t2, 5);
            SetValue(t3, 5);
            t1.AddNewChild(t2);
            t1.AddNewChild(t3);
            IList<TreeNodeScript> nodes = new List<TreeNodeScript>(new TreeNodeScript[] { t1, t2, t3 });

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.True(vc.Apply(nodes), "The condition is met since the two branches have the same sum");
        }

        [Test]
        public void ParseVictoryConditionSameSumBranchMultiTreeNotMet() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""SameSumBranch""}");
            TreeNodeScript t1 = CreateTreeNodeScript();
            TreeNodeScript t2 = CreateTreeNodeScript();
            TreeNodeScript t3 = CreateTreeNodeScript();
            TreeNodeScript t4 = CreateTreeNodeScript();
            SetValue(t2, 4);
            SetValue(t3, 5);
            SetValue(t4, 5);
            t1.AddNewChild(t2);
            t1.AddNewChild(t3);
            IList<TreeNodeScript> nodes = new List<TreeNodeScript>(new TreeNodeScript[] { t1, t2, t3 });

            // Act
            VictoryCondition vc = VictoryConditions.ParseVictoryCondition(conditionNode);

            // Assert
            Assert.False(vc.Apply(nodes), "The condition is not met since the three branches have differing sums");
        }

        [Test]
        public void ParseVictoryConditionSameSumBranchMultiTreeMet() {
            // Arrange
            JSONNode conditionNode = JSONNode.Parse(@"{""name"": ""SameSumBranch""}");
            TreeNodeScript t1 = CreateTreeNodeScript();
            TreeNodeScript t2 = CreateTreeNodeScript();
            TreeNodeScript t3 = CreateTreeNodeScript();
            TreeNodeScript t4 = CreateTreeNodeScript();
            SetValue(t1, 0);
            SetValue(t2, 5);
            SetValue(t3, 5);
            SetValue(t4, 5);
            t1.AddNewChild(t2);
            t1.AddNewChild(t3);
            IList<TreeNodeScript> nodes = new List<TreeNodeScript>(new TreeNodeScript[] { t1, t2, t3 });

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
