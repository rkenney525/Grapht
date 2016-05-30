using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Grapht.Config;
using System.Collections;
using Grapht.Entity;

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
    /// Load references when the component is created
    /// </summary>
    void Start() {
        stateManager = GameObject.Find("StateManager").GetComponent<StateManagerScript>();
    }

    /// <summary>
    /// Load configuration for a particular level
    /// </summary>
    /// <param name="level">The level configuration to use</param>
    public void LoadLevel(Level level) {
        nodes = FindObjectsOfType<TreeNodeScript>();
        victoryConditions = level.Rules;
    }

    /// <summary>
    /// Check if all victory conditions for the level have been met
    /// </summary>
    public void CheckVictory() {
        if (victoryConditions.All(rule => rule.Apply(nodes))) {
            StartCoroutine(HandleVictoryWithDelay());
        }
    }

    private IEnumerator HandleVictoryWithDelay() {
        yield return new WaitForSeconds(VICTORY_WAIT_TIME);
        stateManager.ChangeState(StateManagerScript.State.VICTORY);
    }
}
