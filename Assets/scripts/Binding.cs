using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(RelativeJoint2D))]
public class Binding : MonoBehaviour, IComparer<Binding> {
    public LineRenderer Line {get; private set;}

    public RelativeJoint2D Joint { get; private set; }

    private const float LINE_WIDTH = 0.125f;

    public void SetUp(TreeNodeScript parent) {
        // Correct the transform
        this.transform.position = this.transform.parent.transform.position;

        // Get component references
        this.Line = this.GetComponent<LineRenderer>();
        this.Joint = this.GetComponent<RelativeJoint2D>();

        // Configure components
        this.SetUpLine();
        this.SetUpJoint(parent);
    }

    void Update() {
        this.Line.SetPosition(0, this.transform.position);
        this.Line.SetPosition(1, this.Joint.connectedBody.position);
    }

    private void SetUpLine() {
        this.Line.SetWidth(LINE_WIDTH, LINE_WIDTH);
        this.Line.SetColors(Color.black, Color.black);
    }

    private void SetUpJoint(TreeNodeScript parent) {
        this.Joint.autoConfigureOffset = false;
        this.Joint.connectedBody = parent.GetComponent<Rigidbody2D>();
    }

    public void UpdateAngle(Vector2 angle) {
        this.Joint.linearOffset = angle;
    }

    public int Compare(Binding a, Binding b) {
        return a.Joint.linearOffset.x.CompareTo(b.Joint.linearOffset.x);
    }
}
