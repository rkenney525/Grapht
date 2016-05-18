using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HookChildScript))]
public class MoveableScript : MonoBehaviour {

    private bool selected;

    private Rigidbody2D rigidBody;

    private HookChildScript hooker;

    private VictoryWatcherScript watcher;

	void Start () {
        this.selected = false;
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.hooker = this.GetComponent<HookChildScript>();
        this.watcher = FindObjectOfType<VictoryWatcherScript>();
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
        this.watcher.CheckVictory();
    }

    void OnMouseDown() {
        this.selected = true;
        this.hooker.BeginListening();
    }
}
