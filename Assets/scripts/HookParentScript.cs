﻿using UnityEngine;
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
        TreeNodeScript parentNode = this.GetComponent<TreeNodeScript>();
        if (hooker != null && hooker.Selected && parentNode.CanAcceptConnection()) {
            TreeNodeScript childNode = hooker.GetComponent<TreeNodeScript>();
            if (!parentNode.HasParent(childNode)) {
                Debug.Log("Connecting " + childNode.BranchValue() + " to " + parentNode.BranchValue() + " supplanting " + this.replacement);
                this.replacement = hooker.MakeConnection(parentNode);
            }
        }
    }

    /// <summary>
    /// Pass back the original parent to the departing child node
    /// </summary>
    /// <param name="other">The Collider2D, presumably a node being dragged</param>
    void OnTriggerExit2D(Collider2D other) {
        HookChildScript hooker = this.HookerFromCollider2D(other);
        TreeNodeScript parentNode = this.GetComponent<TreeNodeScript>();
        if (hooker != null && hooker.Selected) {
            TreeNodeScript childNode = hooker.GetComponent<TreeNodeScript>();
            if (!parentNode.HasParent(childNode)) {
                Debug.Log("Releasing " + childNode.BranchValue() + " to " + parentNode.BranchValue() + " restoring " + this.replacement);
                hooker.ReleaseConnection(this.replacement);
            }
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
