using UnityEngine;
using System.Collections;

public class HookSeekingScript : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
        HookListeningScript hooker;
        if (other.name == "Node") {
            hooker = other.GetComponent<HookListeningScript>();
        } else {
            hooker = other.GetComponentInParent<HookListeningScript>();
        }
        hooker.MakeConnection(this.GetComponentInParent<TreeNodeScript>());
    }
}
