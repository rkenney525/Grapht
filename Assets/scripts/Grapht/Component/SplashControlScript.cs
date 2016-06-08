using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Grapht.Component {
    /// <summary>
    /// Displays the Splash Screen and then loads the main menu
    /// </summary>
    public class SplashControlScript : MonoBehaviour {

        /// <summary>
        /// The time, in milliseconds, to wait before loading the main game
        /// </summary>
        private readonly int DISPLAY_TIME = 3;

        /// <summary>
        /// The name of the main game scene
        /// </summary>
        private const string MAIN_GAME_SCENE = "MainGameScene";

        /// <summary>
        /// Kick off the waiting coroutine for the splash screen
        /// </summary>
        void Start() {
            StartCoroutine(Wait());
        }

        /// <summary>
        /// Wait the specified time and then load the main menu
        /// </summary>
        /// <returns>The instruction to wait</returns>
        private IEnumerator Wait() {
            yield return new WaitForSeconds(DISPLAY_TIME);
            SceneManager.LoadScene(MAIN_GAME_SCENE);
        }
    }
}
