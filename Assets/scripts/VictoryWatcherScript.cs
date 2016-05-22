using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Grapht.Config;

/// <summary>
/// Checks if the victory conditions for the game have been met, when invoked.
/// </summary>
public class VictoryWatcherScript : MonoBehaviour {

    /// <summary>
    /// All nodes in the Scene
    /// </summary>
    private IList<TreeNodeScript> nodes;

    /// <summary>
    /// Victory conditions for the root of the graph
    /// </summary>
    private IList<RootCondition> rootConditions;

    /// <summary>
    /// Victory conditions for all branches in the graph
    /// </summary>
    private IList<TreeCondition> branchConditions;

    /// <summary>
    /// Victory conditions for all nodes in the graph
    /// </summary>
    private IList<TreeCondition> globalConditions;

    /// <summary>
    /// Load configuration for a particular level
    /// </summary>
    /// <param name="level">The level configuration to use</param>
    public void LoadLevel(Level level) {
        nodes = FindObjectsOfType<TreeNodeScript>();
        rootConditions = level.RootConditions;
        branchConditions = level.BranchConditions;
        globalConditions = level.GlobalConditions;
    }

    /// <summary>
    /// Check if all victory conditions have been met. All nodes must be in a single tree, and all branches in that tree must have the same sum
    /// </summary>
    public void CheckVictory() {
        // First check the global conditions
        if (globalConditions.All(check => check(nodes))) {
            // Then check branch conditions
            if (branchConditions.All(check => check(nodes.Where(node => node.IsLeaf()).ToList()))) {
                // And finally handle the root
                if (rootConditions.All(check => check(nodes.First().Root()))) {
                    HandleVictory();
                }
            }
        }
    }

    /// <summary>
    /// Handle a victory. Currently does nothing
    /// </summary>
    private void HandleVictory() {
        // TODO do something here
        Debug.Log("WINNERRz");
    }
}
