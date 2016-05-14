using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TreeNodeScript : MonoBehaviour {
    private IList<TreeNodeScript> nodes = new List<TreeNodeScript>();

    private const float LINE_WIDTH = 0.125f;
    private const float LINE_DISTANCE = 5;

    public TreeNodeScript ParentNode { get; private set; }

    private LineRenderer parentLine;

    private DistanceJoint2D joint;

    void Update() {
        if (this.parentLine != null) {
            this.parentLine.SetPosition(0, this.transform.position);
            this.parentLine.SetPosition(1, this.joint.connectedBody.position);
        }
    }

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
        this.joint.distance = LINE_DISTANCE;
        this.UpdateLine();
    }

    private void UpdateLine() {
        if (this.parentLine == null) {
            this.parentLine = this.gameObject.AddComponent<LineRenderer>();
        }
        this.parentLine.SetWidth(LINE_WIDTH, LINE_WIDTH);
        this.parentLine.SetColors(Color.black, Color.black);
    }

    public void LoseChild(TreeNodeScript childNode) {
        this.nodes.Remove(childNode);
    }
}
