﻿using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Delegate for a victory condition on the root of a graph
/// </summary>
/// <param name="root">The root of the graph</param>
/// <returns>True if the victory condition is met, false otherwise</returns>
public delegate bool RootCondition(TreeNodeScript root);

/// <summary>
/// Delegate for a victory condition on some set of nodes in a graph
/// </summary>
/// <param name="nodes">The relevant nodes in the graph</param>
/// <returns>True if the victory condition is met, false otherwise</returns>
public delegate bool TreeCondition(IList<TreeNodeScript> nodes);

/// <summary>
/// A set of victory conditions and victory condition creators
/// </summary>
public class VictoryConditions {
    /// <summary>
    /// Return a Victory condition that the max depth cannot exceed the provided max depth.
    /// </summary>
    /// <param name="maxDepth">The max depth that root cannot exceed</param>
    /// <returns>The specified Victory condition</returns>
    public static RootCondition MaximumDepth(int maxDepth) {
        return delegate (TreeNodeScript root) {
            return root.Depth() <= maxDepth;
        };
    }

    /// <summary>
    /// Return a Victory condition that all branches must have the same sum
    /// </summary>
    public static TreeCondition SameSumBranch =
        delegate (IList<TreeNodeScript> leaves) {
            return leaves.Select(leaf => leaf.BranchValue()).Distinct().Count() == 1;
        };

    /// <summary>
    /// Return a Victory condition that all nodes must be on the same tree
    /// </summary>
    public static TreeCondition SingleTree =
        delegate (IList<TreeNodeScript> nodes) {
            return nodes.Select(node => node.Root()).Distinct().Count() == 1;
        };
}
