using UnityEngine;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Checks if the victory conditions for the game have been met, when invoked.
/// </summary>
public class VictoryWatcherScript : MonoBehaviour {

    /// <summary>
    /// All nodes in the Scene
    /// </summary>
    private IList<TreeNodeScript> nodes = new List<TreeNodeScript>();

    /// <summary>
    /// Victory conditions for the root of the graph
    /// </summary>
    private IList<RootCondition> rootConditions = new List<RootCondition>();

    /// <summary>
    /// Victory conditions for all branches in the graph
    /// </summary>
    private IList<TreeCondition> branchConditions = new List<TreeCondition>();

    /// <summary>
    /// Victory conditions for all nodes in the graph
    /// </summary>
    private IList<TreeCondition> globalConditions = new List<TreeCondition>();

    /// <summary>
    /// Load all nodes in the Scene
    /// </summary>
    void Start() {
        nodes = FindObjectsOfType<TreeNodeScript>();

        // TODO Configure rules per level
        rootConditions.Add(VictoryConditions.MaximumDepth(3));
        branchConditions.Add(VictoryConditions.SameSumBranch);
        globalConditions.Add(VictoryConditions.SingleTree);
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
