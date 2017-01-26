using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Grapht.Node {
    /// <summary>
    /// Looks for a parent node to attach to
    /// </summary>
    [RequireComponent(typeof(TreeNodeScript))]
    public class HookerScript : MonoBehaviour {

        /// <summary>
        /// The name of the GameObject that contains the highlight image mask
        /// </summary>
        private const string HIGHLIGHT_MASK_NAME = "NodeMask";

        /// <summary>
        /// The TreeNodeScript reference
        /// </summary>
        private TreeNodeScript node;

        /// <summary>
        /// List of nodes that are in queue for connection
        /// </summary>
        private IList<TreeNodeScript> connectables = new List<TreeNodeScript>();

        /// <summary>
        /// Whether this node is selected and being dragged around
        /// </summary>
        private bool listening = false;

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
                node.Detach();
            }
            listening = true;
        }

        /// <summary>
        /// Add the triggered node to the list to be connected to
        /// </summary>
        /// <param name="other">The triggered node</param>
        void OnTriggerEnter2D(Collider2D other) {
            if (listening) {
                TreeNodeScript parent = other.GetComponent<TreeNodeScript>();
                if (!parent.HasParent(node) && parent.CanAcceptConnection()) {
                    EnableNodeHighlight(other);
                    connectables.Add(parent);
                }
            }
        }

        /// <summary>
        /// Remove the triggered node from the list to be connected to
        /// </summary>
        /// <param name="other">The triggered node</param>
        void OnTriggerExit2D(Collider2D other) {
            if (listening) {
                TreeNodeScript parent = other.GetComponent<TreeNodeScript>();
                DisableNodeHighlight(other);
                connectables.Remove(parent);
            }
        }

        /// <summary>
        /// Connect with the last node overlapped, if there is one
        /// </summary>
        public void DoConnect() {
            // If there was no parent initially, do nothing
            if (listening && connectables.Count > 0) {
                TreeNodeScript newParent = connectables.Last();
                DisableNodeHighlight(newParent.GetComponent<Collider2D>());
                newParent.AddNewChild(node);
                connectables.Clear();
            }
            listening = false;
        }

        /// <summary>
        /// Change the overlay state for a given node
        /// </summary>
        /// <param name="other">The node being acted on</param>
        /// <param name="state">Whether the overlay should be active</param>
        private void ToggleNodeHighlight(Collider2D other, bool state) {
            other.transform.Find(HIGHLIGHT_MASK_NAME).gameObject.SetActive(state);
        }

        /// <summary>
        /// Highlight the node to show its being hovered over
        /// </summary>
        /// <param name="other">The node being hovered over</param>
        private void EnableNodeHighlight(Collider2D other) {
            ToggleNodeHighlight(other, true);
        }

        /// <summary>
        /// Stop highlighting a node that is out of range
        /// </summary>
        /// <param name="other">The node no longer being hovered over</param>
        private void DisableNodeHighlight(Collider2D other) {
            ToggleNodeHighlight(other, false);
        }
    }
}
