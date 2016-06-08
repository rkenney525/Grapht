using UnityEngine;
using System.Collections.Generic;

namespace Grapht.Node {
    /// <summary>
    /// Link of Parent node to a child node
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(RelativeJoint2D))]
    public class Binding : MonoBehaviour {
        /// <summary>
        /// The line to draw from the parent to the child
        /// </summary>
        public LineRenderer Line { get; private set; }

        /// <summary>
        /// The joint that keeps the child tethered to the parent
        /// </summary>
        public RelativeJoint2D Joint { get; private set; }

        /// <summary>
        /// The width that lines should be drawn
        /// </summary>
        private const float LINE_WIDTH = 0.125f;

        /// <summary>
        /// Set up this binding to the parent node
        /// </summary>
        /// <param name="parent">The parent node to attach to</param>
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

        /// <summary>
        /// Update the position of the line per tick
        /// </summary>
        void Update() {
            this.Line.SetPosition(0, this.transform.position);
            this.Line.SetPosition(1, this.Joint.connectedBody.position);
        }

        /// <summary>
        /// Set some properties for the Line Renderer. These are only set once
        /// </summary>
        private void SetUpLine() {
            this.Line.SetWidth(LINE_WIDTH, LINE_WIDTH);
            this.Line.SetColors(Color.black, Color.black);
        }


        /// <summary>
        /// Set up some properties for the joint. These are only set once
        /// </summary>
        /// <param name="parent">The parent node the joint should be connected to</param>
        private void SetUpJoint(TreeNodeScript parent) {
            this.Joint.autoConfigureOffset = false;
            this.Joint.connectedBody = parent.GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Update the angle of the joint
        /// </summary>
        /// <param name="angle">The new angle to set</param>
        public void UpdateAngle(Vector2 angle) {
            this.Joint.linearOffset = angle;
        }

        /// <summary>
        /// Gets the x coordinate in the Joint's offset
        /// </summary>
        /// <returns>The x coordinate in the Joint's offset</returns>
        public float Offset() {
            return this.Joint.linearOffset.x;
        }
    }
}
