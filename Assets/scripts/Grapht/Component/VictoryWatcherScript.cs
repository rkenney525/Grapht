using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Grapht.Config;
using System.Collections;
using Grapht.Node;
using Grapht.Component.Victory;

namespace Grapht.Component {
    /// <summary>
    /// Checks if the victory conditions for the game have been met, when invoked.
    /// </summary>
    public class VictoryWatcherScript : MonoBehaviour {

        /// <summary>
        /// All nodes in the Scene
        /// </summary>
        private IList<TreeNodeScript> nodes;

        /// <summary>
        /// Victory conditions for the level
        /// </summary>
        private IList<VictoryCondition> victoryConditions;

        /// <summary>
        /// The state manager for the game
        /// </summary>
        private StateManagerScript stateManager;

        /// <summary>
        /// Time to wait before switching to the victory state
        /// </summary>
        private const float VICTORY_WAIT_TIME = 0.4f;

        /// <summary>
        /// The overlay script for victory conditions
        /// </summary>
        private VictoryConditionOverlayScript overlay;

        /// <summary>
        /// Load references when the component is created
        /// </summary>
        void Awake() {
            stateManager = GameObject.Find("StateManager").GetComponent<StateManagerScript>();
            overlay = GameObject.Find("Rules").GetComponent<VictoryConditionOverlayScript>();
        }

        /// <summary>
        /// Load configuration for a particular level
        /// </summary>
        /// <param name="level">The level configuration to use</param>
        public void LoadLevel(LevelInfo level) {
            nodes = FindObjectsOfType<TreeNodeScript>();
            victoryConditions = level.Rules;
            overlay.LoadConditions(victoryConditions, nodes);
        }

        /// <summary>
        /// Check if all victory conditions for the level have been met
        /// </summary>
        public void CheckVictory() {
            // Update the UI and check for victory
            // Intentionally dont short circuit with All, so that the ui can get fully updated
            bool victory = true;
            victoryConditions.All(rule => {
                victory &= overlay.UpdateCondition(rule, rule.Apply(nodes));
                return true;
            });

            // Handle a victory
            if (victory) {
                StartCoroutine(HandleVictoryWithDelay());
            }
        }

        /// <summary>
        /// Delay before changing to the victory state
        /// </summary>
        /// <returns>Actions to be performed by a coroutine</returns>
        private IEnumerator HandleVictoryWithDelay() {
            yield return new WaitForSeconds(VICTORY_WAIT_TIME);
            stateManager.ChangeState(StateManagerScript.State.VICTORY);
        }
    }
}
