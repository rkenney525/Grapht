using UnityEngine;

namespace Grapht.Component.Hint {
    public class HintButtonScript : MonoBehaviour {

        private bool display = false;

        // Use this for initialization
        void Start() {
            // TODO create and place the hints
            // TODO hide the hints
        }

        // Update is called once per frame
        void Update() {
            if (display) {
                // TODO display
            }
        }

        /// <summary>
        /// Toggle the display of the level's hints on or off.
        /// </summary>
        public void ToggleDisplay() {
            display = !display;
        }
    }
}