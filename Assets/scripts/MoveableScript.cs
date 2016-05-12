using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HookListeningScript))]
public class MoveableScript : MonoBehaviour {

    private bool selected;

    private Rigidbody2D rigidBody;

    private HookListeningScript hooker;

	void Start () {
        this.selected = false;
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.hooker = this.GetComponent<HookListeningScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.selected) {
            this.rigidBody.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
	}

    void OnMouseUp() {
        this.selected = false;
        this.hooker.EndAndConnect();
    }

    void OnMouseDown() {
        this.selected = true;
        this.hooker.BeginListening();
    }
}
