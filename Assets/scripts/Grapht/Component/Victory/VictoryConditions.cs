using Grapht.Exception;
using Grapht.Node;
using SimpleJSON;
using System.Collections.Generic;
using System.Linq;

namespace Grapht.Component.Victory {
    /// <summary>
    /// A set of victory conditions and victory condition creators
    /// </summary>
    public class VictoryConditions {

        /// <summary>
        /// Filter to get the root of each tree
        /// </summary>
        private static VictoryCondition.Filter rootFilter = delegate(IList<TreeNodeScript> nodes) {
            return nodes.Select(node => node.Root()).Distinct().ToList();
        };

        /// <summary>
        /// Filter to get all unique branches, across trees
        /// </summary>
        private static VictoryCondition.Filter allBranchFilter = delegate(IList<TreeNodeScript> nodes) {
            return nodes.Where(node => node.IsLeaf()).ToList();
        };

        /// <summary>
        /// Filter to return all nodes
        /// </summary>
        private static VictoryCondition.Filter allNodesFilter = delegate(IList<TreeNodeScript> nodes) {
            return nodes;
        };

        /// <summary>
        /// Retrieve a Mapping of branches to their tree's root
        /// </summary>
        /// <param name="nodes">All nodes on the graph</param>
        /// <returns>A grouping of all leaves to their corresponding root</returns>
        private static IEnumerable<IGrouping<TreeNodeScript, TreeNodeScript>> allBranchesByTree(IList<TreeNodeScript> nodes) {
            return allBranchFilter(nodes).GroupBy(node => node.Root());
        }

        /// <summary>
        /// Return a Victory condition that the max depth cannot exceed the provided max depth.
        /// </summary>
        /// <param name="maxDepth">The max depth that root cannot exceed</param>
        /// <returns>The specified Victory condition</returns>
        private static VictoryCondition MaximumDepth(int maxDepth) {
            return new VictoryCondition(
                string.Format("Max depth of {0} nodes", maxDepth),
                delegate(IList<TreeNodeScript> root) {
                    return root.First().Depth() <= maxDepth;
                }, rootFilter);
        }

        /// <summary>
        /// Return a Victory condition that all branches must have the same sum
        /// </summary>
        private static VictoryCondition SameSumBranch() {
            return new VictoryCondition(
                "All branches have the same sum",
                delegate(IList<TreeNodeScript> leaves) {
                    return leaves.Select(leaf => leaf.BranchValue()).Distinct().Count() == 1;
                }, allBranchFilter);
        }

        /// <summary>
        /// Return a Victory condition that all nodes must be on the same tree
        /// </summary>
        private static VictoryCondition SingleTree() {
            return new VictoryCondition(
                "All nodes on a single tree",
                delegate(IList<TreeNodeScript> nodes) {
                    return nodes.Select(node => node.Root()).Distinct().Count() == 1;
                }, allNodesFilter);
        }

        /// <summary>
        /// Get the VictoryCondition for the associated JSON node
        /// </summary>
        /// <param name="json">The JSON node to parse</param>
        /// <returns>A VictoryCondition identified by the json's name</returns>
        public static VictoryCondition ParseVictoryCondition(JSONNode json) {
            switch (json["name"]) {
                case "MaxDepth":
                    return MaximumDepth(json["arg"].AsInt);
                case "SameSumBranch":
                    return SameSumBranch();
                case "SingleTree":
                    return SingleTree();
                default:
                    throw new GraphtParsingException();
            }
        }
    }
}
