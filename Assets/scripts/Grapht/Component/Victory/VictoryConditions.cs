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
        private static VictoryCondition.Filter rootFilter = delegate (IList<TreeNodeScript> nodes) {
            return nodes.Select(node => node.Root()).Distinct().ToList();
        };

        /// <summary>
        /// Filter to get all unique branches, across trees
        /// </summary>
        private static VictoryCondition.Filter allBranchFilter = delegate (IList<TreeNodeScript> nodes) {
            return nodes.Where(node => node.IsLeaf()).ToList();
        };

        /// <summary>
        /// Filter to return all nodes
        /// </summary>
        private static VictoryCondition.Filter allNodesFilter = delegate (IList<TreeNodeScript> nodes) {
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
                delegate (IList<TreeNodeScript> leaves) {
                    return leaves.Select(leaf => leaf.Root()).Distinct()
                        .All(root => root.Depth() <= maxDepth);
                }, allBranchFilter);
        }

        /// <summary>
        /// Return a VictoryCondition that says all branches on the same tree have to have the same sum.
        /// </summary>
        /// <returns>The specified Victory condition</returns>
        private static VictoryCondition SameSumBranchPerTree() {
            return new VictoryCondition(
                "All branches on the same tree have the same sum",
                delegate (IList<TreeNodeScript> nodes) {
                    return allBranchesByTree(nodes)
                    .All(group => group.Select(leaf => leaf.BranchValue()).Distinct().Count() == 1);
                }, allNodesFilter);
        }

        /// <summary>
        /// Return a Victory condition that all branches must have the same sum
        /// </summary>
        private static VictoryCondition SameSumBranch() {
            return new VictoryCondition(
                "All branches have the same sum",
                delegate (IList<TreeNodeScript> leaves) {
                    return leaves.Select(leaf => leaf.BranchValue()).Distinct().Count() == 1;
                }, allBranchFilter);
        }

        /// <summary>
        /// Return a Victory condition that all nodes must be on the same tree
        /// </summary>
        private static VictoryCondition SingleTree() {
            return new VictoryCondition(
                "All nodes on a single tree",
                delegate (IList<TreeNodeScript> nodes) {
                    return nodes.Distinct().Count() == 1;
                }, rootFilter);
        }

        /// <summary>
        /// Return a VictoryCondition that makes sure there are no more than the specific number of trees
        /// </summary>
        /// <param name="num">The max number of tree</param>
        /// <returns>The specified Victory condition</returns>
        private static VictoryCondition NoMoreThanTrees(int num) {
            return new VictoryCondition(
                string.Format("No more than {0} trees", num),
                delegate (IList<TreeNodeScript> nodes) {
                    return nodes.Distinct().Count() <= num;
                }, rootFilter);
        }

        /// <summary>
        /// Return a VictoryCondition that makes sure there are no less than the specific number of trees
        /// </summary>
        /// <param name="num">The min number of tree</param>
        /// <returns>The specified Victory condition</returns>
        private static VictoryCondition NoLessThanTrees(int num) {
            return new VictoryCondition(
                string.Format("No fewer than {0} trees", num),
                delegate (IList<TreeNodeScript> nodes) {
                    return nodes.Distinct().Count() >= num;
                }, rootFilter);
        }

        /// <summary>
        /// Return a VictoryCondition that makes sure there are no more than the specific number of branches across all trees
        /// </summary>
        /// <param name="num">The max number of branches to allow</param>
        /// <returns>The specified Victory condition</returns>
        private static VictoryCondition NoMoreThanTotalBranches(int num) {
            return new VictoryCondition(
                string.Format("No more than {0} branches across all trees", num),
                delegate (IList<TreeNodeScript> nodes) {
                    return nodes.Count() <= num;
                }, allBranchFilter);
        }

        /// <summary>
        /// Return a VictoryCondition that makes sure there are no less than the specific number of branches across all trees
        /// </summary>
        /// <param name="num">The min number of branches to allow</param>
        /// <returns>The specified Victory condition</returns>
        private static VictoryCondition NoLessThanTotalBranches(int num) {
            return new VictoryCondition(
                string.Format("No fewer than {0} branches across all trees", num),
                delegate (IList<TreeNodeScript> nodes) {
                    return nodes.Count() >= num;
                }, allBranchFilter);
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
                case "SameSumBranchPerTree":
                    return SameSumBranchPerTree();
                case "NoMoreThanTrees":
                    return NoMoreThanTrees(json["arg"].AsInt);
                case "NoLessThanTrees":
                    return NoLessThanTrees(json["arg"].AsInt);
                case "NoMoreThanTotalBranches":
                    return NoMoreThanTotalBranches(json["arg"].AsInt);
                case "NoLessThanTotalBranches":
                    return NoLessThanTotalBranches(json["arg"].AsInt);
                default:
                    throw new GraphtParsingException();
            }
        }
    }
}
