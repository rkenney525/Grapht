﻿using UnityEngine;
using System.Collections;

public class HookSeekingScript : MonoBehaviour {

    private TreeNodeScript replacement;

    void OnTriggerEnter2D(Collider2D other) {
        HookListeningScript hooker = this.HookerFromCollider2D(other);
        hooker.MakeConnection(this.GetComponentInParent<TreeNodeScript>());
    }

    void OnTriggerExit2D(Collider2D other) {
        HookListeningScript hooker = this.HookerFromCollider2D(other);
        hooker.ReleaseConnection(this.replacement);
        this.replacement = null;
    }

    private HookListeningScript HookerFromCollider2D(Collider2D coll) {
        if (coll.name == "Node") {
            return coll.GetComponent<HookListeningScript>();
        } else {
            return coll.GetComponentInParent<HookListeningScript>();
        }
    }
}
