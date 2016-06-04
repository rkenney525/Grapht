using UnityEngine;
using System.Collections;

/// <summary>
/// Captures the logic for moving a node
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HookerScript))]
public class MoveableScript : MonoBehaviour {

    /// <summary>
    /// True if the node is being dragged, false otherwise
    /// </summary>
    private bool selected;

    /// <summary>
    /// The RigidBody2D component associated with this node
    /// </summary>
    private Rigidbody2D rigidBody;

    /// <summary>
    /// The HookChildScript componentn associated with this node. Used to look for a parent node
    /// </summary>
    private HookerScript hooker;

    /// <summary>
    /// The global VictoryWatcherScript. Invoked when the watcher should check the victory conditions
    /// </summary>
    private VictoryWatcherScript watcher;

    /// <summary>
    /// Load all internal and external references when this component is created
    /// </summary>
	void Awake () {
        this.selected = false;
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.hooker = this.GetComponent<HookerScript>();
        this.watcher = FindObjectOfType<VictoryWatcherScript>();
	}
	
    /// <summary>
    /// If the node is being moved, update its position each tick
    /// </summary>
	void Update () {
        if (this.selected) {
            this.rigidBody.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
	}

    /// <summary>
    /// When the mouse button is released, try to connect the node and check for victory
    /// </summary>
    void OnMouseUp() {
        this.selected = false;
        this.hooker.DoConnect();
        this.watcher.CheckVictory();
    }

    /// <summary>
    /// When the mouse button is clicked, select the node and start looking for a parent
    /// </summary>
    void OnMouseDown() {
        this.selected = true;
        this.hooker.BeginListening();
    }
}
