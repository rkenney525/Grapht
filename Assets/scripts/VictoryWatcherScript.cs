using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VictoryWatcherScript : MonoBehaviour {

    private IList<TreeNodeScript> nodes = new List<TreeNodeScript>();

    // Use this for initialization
    void Start() {
        TreeNodeScript[] nodeArray = FindObjectsOfType<TreeNodeScript>();
        foreach (TreeNodeScript node in nodeArray) {
            nodes.Add(node);
        }
    }

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

    private IList<TreeNodeScript> Leaves() {
        IList<TreeNodeScript> leaves = new List<TreeNodeScript>();
        foreach (TreeNodeScript node in this.nodes) {
            if (node.IsLeaf()) {
                leaves.Add(node);
            }
        }
        return leaves;
    }

    private void HandleVictory() {
        // TODO do something here
        Debug.Log("WINNERRz");
    }
}
