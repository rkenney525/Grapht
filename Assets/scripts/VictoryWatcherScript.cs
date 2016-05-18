using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VictoryWatcherScript : MonoBehaviour {

    private IList<TreeNodeScript> nodes = new List<TreeNodeScript>();

	// Use this for initialization
	void Start () {
        // TODO see if c# can do any better than this
        TreeNodeScript[] nodeArray = FindObjectsOfType<TreeNodeScript>();
        for (int i = 0; i < nodeArray.Length; i++) {
            nodes.Add(nodeArray[i]);
        }
	}

    public void CheckVictory() {
        IList<TreeNodeScript> leaves = this.Leaves();
        
    }
    
    private IList<TreeNodeScript> Leaves() {
        IList<TreeNodeScript> leaves = new List<TreeNodeScript>();
        // TODO again, see if we can do something more functional
        for (int i = 0; i < this.nodes.Count; i++) {
            if (this.nodes[i].IsLeaf()) {
                leaves.Add(this.nodes[i]);
            }
        }
        return leaves;
    }

    private void HandleVictory() {
        // TODO do something here
        Debug.Log("WINNERRz");
    }
}
