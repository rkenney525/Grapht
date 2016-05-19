using UnityEngine;
using System.Collections;

/// <summary>
/// Looks for a child to attach to and lets the child node know when it is in and out of range
/// </summary>
public class HookParentScript : MonoBehaviour {

    /// <summary>
    /// The parent TreeNodeScript reference of the prospective child that this could replace
    /// </summary>
    private TreeNodeScript replacement;

    /// <summary>
    /// When the trigger space is entered, prepare to make a connection with the prospective child node.
    /// </summary>
    /// <param name="other">The Collider2D, presumably a node being dragged</param>
    void OnTriggerEnter2D(Collider2D other) {
        HookChildScript hooker = this.HookerFromCollider2D(other);
        TreeNodeScript parentNode = this.GetComponentInParent<TreeNodeScript>();
        if (hooker != null && parentNode.CanAcceptConnection()) {
            this.replacement = hooker.MakeConnection(parentNode);
        }
    }

    /// <summary>
    /// Pass back the original parent to the departing child node
    /// </summary>
    /// <param name="other">The Collider2D, presumably a node being dragged</param>
    void OnTriggerExit2D(Collider2D other) {
        HookChildScript hooker = this.HookerFromCollider2D(other);
        if (hooker != null) {
            hooker.ReleaseConnection(this.replacement);
        }
        this.replacement = null;
    }

    /// <summary>
    /// Get a TreeNodeScript reference from a Collider2D
    /// </summary>
    /// <param name="coll">The Collider2D reference</param>
    /// <returns>The associated TreeNodeScript reference</returns>
    private HookChildScript HookerFromCollider2D(Collider2D coll) {
        return coll.GetComponent<HookChildScript>();
    }
}
