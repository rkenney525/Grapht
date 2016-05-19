using UnityEngine;
using System.Collections;

/// <summary>
/// Looks for a parent node to attach to
/// </summary>
[RequireComponent(typeof(TreeNodeScript))]
public class HookChildScript : MonoBehaviour {

    /// <summary>
    /// The TreeNodeScript reference
    /// </summary>
    private TreeNodeScript node;

    /// <summary>
    /// The node to connect to on a mouse button release
    /// </summary>
    private TreeNodeScript connectingNode;

    /// <summary>
    /// Load the TreeScriptNode reference when this component is created
    /// </summary>
    void Start() {
        this.node = this.GetComponent<TreeNodeScript>();
    }

    /// <summary>
    /// Prepare to connect to "node".
    /// </summary>
    /// <param name="node">The prospective parent node</param>
    /// <returns>The previously connected parent node</returns>
    public TreeNodeScript MakeConnection(TreeNodeScript node) {
        TreeNodeScript old = this.connectingNode;
        this.connectingNode = node;
        return old;
    }

    /// <summary>
    /// Drop the connection to the prospective parent
    /// </summary>
    /// <param name="node">The node to fall back to as parent</param>
    public void ReleaseConnection(TreeNodeScript node) {
        this.connectingNode = node;
    }

    /// <summary>
    /// Start looking for a parent by detaching from the current one (if it exists) and storing a reference to it in case no parent is found
    /// </summary>
    public void BeginListening() {
        this.connectingNode = this.node.ParentNode;
        this.node.Detach();
    }

    /// <summary>
    /// Connect to the specified parent, if one exisst. It would either be a new parent or the previous one if none were found
    /// </summary>
    public void EndAndConnect() {
        if (this.connectingNode != null) {
            this.connectingNode.AddNewChild(this.node);
            this.connectingNode = null;
        }
    }
}
