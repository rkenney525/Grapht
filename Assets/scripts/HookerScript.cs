using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Looks for a parent node to attach to
/// </summary>
[RequireComponent(typeof(TreeNodeScript))]
public class HookerScript : MonoBehaviour {

    /// <summary>
    /// The TreeNodeScript reference
    /// </summary>
    private TreeNodeScript node;

    /// <summary>
    /// List of nodes that are in queue for connection
    /// </summary>
    private IList<TreeNodeScript> connectables = new List<TreeNodeScript>();

    /// <summary>
    /// Load the TreeScriptNode reference and set other properties when this component is created
    /// </summary>
    void Awake() {
        node = this.GetComponent<TreeNodeScript>();
    }

    /// <summary>
    /// Start looking for a parent by detaching from the current one (if it exists) and storing a reference to it in case no parent is found
    /// </summary>
    public void BeginListening() {
        if (node.ParentNode != null) {
            connectables.Add(this.node.ParentNode);
            node.Detach();
        }
    }

    /// <summary>
    /// Add the triggered node to the list to be connected to
    /// </summary>
    /// <param name="other">The triggered node</param>
    void OnTriggerEnter2D(Collider2D other) {
        TreeNodeScript parent = other.GetComponent<TreeNodeScript>();
        if (!parent.HasParent(node) && parent.CanAcceptConnection()) {
            connectables.Add(parent);
        }
    }

    /// <summary>
    /// Remove the triggered node from the list to be connected to
    /// </summary>
    /// <param name="other">The triggered node</param>
    void OnTriggerExit2D(Collider2D other) {
        TreeNodeScript parent = other.GetComponent<TreeNodeScript>();
        connectables.Remove(parent);
    }

    /// <summary>
    /// Connect with the last node overlapped, if there is one
    /// </summary>
    public void DoConnect() {
        // If there was no parent initially, do nothing
        if (connectables.Count > 0) {
            connectables.Last().AddNewChild(node);
            connectables.Clear();
        }
    }
}
