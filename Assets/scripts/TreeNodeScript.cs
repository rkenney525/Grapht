using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TreeNodeScript : MonoBehaviour {
    private IList<TreeNodeScript> nodes = new List<TreeNodeScript>();

    public TreeNodeScript ParentNode { get; private set; }

    private DistanceJoint2D joint;

    public void AddNewChild(TreeNodeScript childNode) {
        this.nodes.Add(childNode);
        childNode.ChangeParent(this);
    }

    public void ChangeParent(TreeNodeScript parentNode) {
        if (this.ParentNode != null) {
            this.ParentNode.LoseChild(this);
        }
        this.ParentNode = parentNode;
        if (this.joint == null) {
            this.joint = this.gameObject.AddComponent<DistanceJoint2D>();
        }
        this.joint.connectedBody = this.ParentNode.GetComponent<Rigidbody2D>();
    }

    public void LoseChild(TreeNodeScript childNode) {
        this.nodes.Remove(childNode);
    }
}
