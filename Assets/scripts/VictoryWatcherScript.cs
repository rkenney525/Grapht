using UnityEngine;
using System.Collections;
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
    /// Load all nodes in the Scene
    /// </summary>
    void Start() {
        TreeNodeScript[] nodeArray = FindObjectsOfType<TreeNodeScript>();
        foreach (TreeNodeScript node in nodeArray) {
            nodes.Add(node);
        }
    }

    /// <summary>
    /// Check if all victory conditions have been met. All nodes must be in a single tree, and all branches in that tree must have the same sum
    /// </summary>
    public void CheckVictory() {
        IList<TreeNodeScript> leaves = this.Leaves();
        // Get the sum of the first leaf
        int sum = leaves[0].BranchValue();
        TreeNodeScript root = leaves[0].Root();
        foreach (TreeNodeScript leaf in leaves) {
            // If any node is not equal or doesnt have the same root, then terminate the check early
            if (leaf.BranchValue() != sum ||
                !leaf.Root().Equals(root)) {
                return;
            }
        }
        this.HandleVictory();
    }

    /// <summary>
    /// Get all leaf nodes in the Scene
    /// </summary>
    /// <returns>All Leaf nodes in the Scene</returns>
    private IList<TreeNodeScript> Leaves() {
        IList<TreeNodeScript> leaves = new List<TreeNodeScript>();
        foreach (TreeNodeScript node in this.nodes) {
            if (node.IsLeaf()) {
                leaves.Add(node);
            }
        }
        return leaves;
    }

    /// <summary>
    /// Handle a victory. Currently does nothing
    /// </summary>
    private void HandleVictory() {
        // TODO do something here
        Debug.Log("WINNERRz");
    }
}
