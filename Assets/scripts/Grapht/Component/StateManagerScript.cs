using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Grapht.Component;

namespace Grapht.Component {
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

        /// <summary>
        /// Background sprite for the main menu
        /// </summary>
        private SpriteRenderer menuBackground;

        /// <summary>
        /// Canvas for the victory overlay
        /// </summary>
        private Canvas victoryCanvas;

        /// <summary>
        /// Canvas for the game overlay
        /// </summary>
        private Canvas gameOverlayCanvas;

        /// <summary>
        /// Overlay script that draws grid squares
        /// </summary>
        private GridOverlayScript gridOverlayScript;

        /// <summary>
        /// The button for the next level on the victory overlay
        /// </summary>
        private Button nextLevelButton;

        /// <summary>
        /// Apply the starting state and load needed components
        /// </summary>
        void Start() {
            cameraControls = FindObjectOfType<CameraControlScript>();
            menuCanvas = GameObject.Find("Menu").GetComponent<Canvas>();
            menuBackground = GameObject.Find("MenuBackground").GetComponent<SpriteRenderer>();
            victoryCanvas = GameObject.Find("Victory").GetComponent<Canvas>();
            gridOverlayScript = FindObjectOfType<GridOverlayScript>();
            gameOverlayCanvas = GameObject.Find("Rules").GetComponent<Canvas>();
            nextLevelButton = GameObject.Find("Next Level Button").GetComponent<Button>();
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
            ChangeState((State)stateValue);
        }

        /// <summary>
        /// Apply the logic of the current state
        /// </summary>
        private void ApplyState() {
            // TODO better encapsulate these configurations
            switch (currentState) {
                case State.MAIN_MENU:
                    loader.ClearStage();
                    gridOverlayScript.EraseGrid();
                    gameOverlayCanvas.enabled = false;
                    victoryCanvas.enabled = false;
                    cameraControls.enabled = false;
                    menuCanvas.enabled = true;
                    menuBackground.enabled = true;
                    break;
                case State.GAME:
                    loader.LoadCurrentLevel();
                    gridOverlayScript.DrawGrid();
                    gameOverlayCanvas.enabled = true;
                    victoryCanvas.enabled = false;
                    cameraControls.enabled = true;
                    menuCanvas.enabled = false;
                    menuBackground.enabled = false;
                    break;
                case State.VICTORY:
                    loader.ClearStage();
                    gridOverlayScript.EraseGrid();
                    gameOverlayCanvas.enabled = false;
                    victoryCanvas.enabled = true;
                    cameraControls.enabled = false;
                    menuCanvas.enabled = false;
                    menuBackground.enabled = false;
                    nextLevelButton.interactable = loader.HasNextLevel();
                    break;
                case State.QUITTING:
                    // TODO save any progress
                    Application.Quit();
                    break;
            }
        }

        public void NextLevel() {
            loader.CurrentLevel++;
            ChangeState(State.GAME);
        }
    }
}
