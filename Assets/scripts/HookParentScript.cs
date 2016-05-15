using UnityEngine;
using System.Collections;

public class HookParentScript : MonoBehaviour {

    private TreeNodeScript replacement;

    void OnTriggerEnter2D(Collider2D other) {
        HookChildScript hooker = this.HookerFromCollider2D(other);
        TreeNodeScript parentNode = this.GetComponentInParent<TreeNodeScript>();
        if (hooker != null && parentNode.CanAcceptConnection()) {
            this.replacement = hooker.MakeConnection(parentNode);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        HookChildScript hooker = this.HookerFromCollider2D(other);
        if (hooker != null) {
            hooker.ReleaseConnection(this.replacement);
        }
        this.replacement = null;
    }

    private HookChildScript HookerFromCollider2D(Collider2D coll) {
        return coll.GetComponent<HookChildScript>();
    }
}
