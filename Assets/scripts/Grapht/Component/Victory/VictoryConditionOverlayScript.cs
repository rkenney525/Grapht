using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using Grapht.Node;

namespace Grapht.Component.Victory {
    /// <summary>
    /// Control the placement and display of all rules
    /// </summary>
    public class VictoryConditionOverlayScript : MonoBehaviour {

        /// <summary>
        /// The VictoryConditionEntry prefab
        /// </summary>
        public GameObject VictoryConditionEntryRef;

        /// <summary>
        /// The height of a particular entry. Easier to hardcode this than try to get at runtime
        /// </summary>
        private float ENTRY_HEIGHT = 40.0f;

        /// <summary>
        /// The place to start placing entries
        /// </summary>
        private readonly Vector2 START = new Vector2(-290, 280);

        /// <summary>
        /// A Dictionary of rules and their corresponding UI entries
        /// </summary>
        private IDictionary<VictoryCondition, VictoryConditionEntryScript> rules =
            new Dictionary<VictoryCondition, VictoryConditionEntryScript>();

        /// <summary>
        /// Update the overlay to display the current set of victory conditions
        /// </summary>
        /// <param name="conditions">The current set of victory conditions in the game</param>
        /// <param name="nodes">The current list of nodes in the world</param>
        public void LoadConditions(IList<VictoryCondition> conditions, IList<TreeNodeScript> nodes) {
            // Empty the current dictionary
            Clear();

            // Add the new conditions
            int entryNumber = 0;
            conditions.All(condition => {
                // Create the entry
                Vector2 position = START - new Vector2(0, ENTRY_HEIGHT * entryNumber++);
                GameObject entryObj = Instantiate(VictoryConditionEntryRef);
                entryObj.transform.SetParent(this.transform, false);

                // Get the script reference and set it up
                VictoryConditionEntryScript entry = entryObj.GetComponent<VictoryConditionEntryScript>();
                entry.Load(position, condition.Title, condition.Apply(nodes));

                // Store it in the dictionary
                rules.Add(condition, entry);
                return true;
            });
        }

        /// <summary>
        /// Update the specified victory condition to display whether or not it has been met
        /// </summary>
        /// <param name="condition">The VictoryCondition to update</param>
        /// <param name="isMet">Whether the condition is met or not</param>
        /// <returns>The value of isMet, to facilitate the functional approach</returns>
        public bool UpdateCondition(VictoryCondition condition, bool isMet) {
            rules.Where(set => set.Key.Equals(condition)).Select(set => set.Value).First().UpdateIsMet(isMet);
            return isMet;
        }

        /// <summary>
        /// Remove display entities and clear the dictionary
        /// </summary>
        private void Clear() {
            rules.Select(set => set.Value).All(txt => {
                GameObject.Destroy(txt.gameObject);
                return true;
            });
            rules.Clear();
        }
    }
}
