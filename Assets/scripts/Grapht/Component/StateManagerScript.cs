using UnityEngine;
using System.Collections;

/// <summary>
/// Manage interactivity of the game via states
/// </summary>
public class StateManagerScript : MonoBehaviour {

    /// <summary>
    /// Represents the interactivity state the game is in
    /// </summary>
    public enum State { MAIN_MENU, GAME, VICTORY, QUITTING }

    /// <summary>
    /// The current state of the game
    /// </summary>
    private State currentState = State.MAIN_MENU;

    /// <summary>
    /// The camera controls for zooming and panning
    /// </summary>
    private CameraControlScript cameraControls;

    /// <summary>
    /// The Level loading script
    /// </summary>
    private LevelLoaderScript loader;

    /// <summary>
    /// Canvas for the main menu
    /// </summary>
    private Canvas menuCanvas;


    private Canvas victoryCanvas;

    /// <summary>
    /// Apply the starting state and load needed components
    /// </summary>
    void Start() {
        cameraControls = FindObjectOfType<CameraControlScript>();
        menuCanvas = GameObject.Find("Menu").GetComponent<Canvas>();
        victoryCanvas = GameObject.Find("Victory").GetComponent<Canvas>();
        loader = FindObjectOfType<LevelLoaderScript>();
        ApplyState();
    }

    /// <summary>
    /// Change the state and immediately apply it
    /// </summary>
    /// <param name="state">The new State</param>
    public void ChangeState(State state) {
        currentState = state;
        ApplyState();
    }

    /// <summary>
    /// Changes state by an integer equivalent to the State's enum value, so that Unity's event system can call it directly
    /// </summary>
    /// <param name="stateValue">The int value of the state in the enum</param>
    public void ChangeState(int stateValue) {
        ChangeState((State) stateValue);
    }

    /// <summary>
    /// Apply the logic of the current state
    /// </summary>
    private void ApplyState() {
        // TODO better encapsulate these configurations
        switch (currentState) {
            case State.MAIN_MENU:
                loader.ClearStage();
                victoryCanvas.enabled = false;
                cameraControls.enabled = false;
                menuCanvas.enabled = true;
                break;
            case State.GAME:
                loader.LoadCurrentLevel();
                victoryCanvas.enabled = false;
                cameraControls.enabled = true;
                menuCanvas.enabled = false;
                break;
            case State.VICTORY:
                victoryCanvas.enabled = true;
                cameraControls.enabled = false;
                menuCanvas.enabled = false;
                break;
            case State.QUITTING:
                // TODO save any progress
                Application.Quit();
                break;
        }
    }
}
