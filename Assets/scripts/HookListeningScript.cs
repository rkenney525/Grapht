using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TreeNodeScript))]
public class HookListeningScript : MonoBehaviour {

    private TreeNodeScript node;

    private TreeNodeScript connectingNode;

    void Start() {
        this.node = this.GetComponent<TreeNodeScript>();
    }

    public TreeNodeScript MakeConnection(TreeNodeScript node) {
        TreeNodeScript old = this.connectingNode;
        this.connectingNode = node;
        return old;
    }

    public void ReleaseConnection(TreeNodeScript node) {
        this.connectingNode = node;
    }

    public void BeginListening() {
        this.connectingNode = this.node.ParentNode;
        this.node.Detach();
    }

    public void EndAndConnect() {
        if (this.connectingNode != null) {
            this.connectingNode.AddNewChild(this.node);
            this.connectingNode = null;
        }
    }
}
