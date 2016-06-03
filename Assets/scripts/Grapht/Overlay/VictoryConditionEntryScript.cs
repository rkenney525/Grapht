using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Grapht.Overlay {
    /// <summary>
    /// Controls a UI entry for a victory condition
    /// </summary>
    public class VictoryConditionEntryScript : MonoBehaviour {

        /// <summary>
        /// The condition description Text field
        /// </summary>
        private Text description;

        /// <summary>
        /// The Met/Unmet Text field
        /// </summary>
        private Text met;

        /// <summary>
        /// The text to display if the condition is met
        /// </summary>
        private const string MET_TEXT = "Met";

        /// <summary>
        ///  The text to display if the condition is not met
        /// </summary>
        private const string UNMET_TEXT = "Unmet";

        /// <summary>
        /// The color to display the met text
        /// </summary>
        private readonly Color MET_COLOR = Color.green;

        /// <summary>
        /// The color to display the unmet text
        /// </summary>
        private readonly Color UNMET_COLOR = Color.red;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position">The location this entry should go</param>
        /// <param name="display">The title text to display</param>
        /// <param name="isMet">If the condition is initially met or not</param>
        public void Load(Vector2 position, string display, bool isMet) {
            // Change the position
            GetComponent<RectTransform>().anchoredPosition = position;

            // Load the references
            description = transform.FindChild("Description").GetComponent<Text>();
            met = transform.FindChild("Met").GetComponent<Text>();

            // Set the initial state
            description.text = display;
            UpdateIsMet(isMet);
        }

        /// <summary>
        /// Update the display text based on if a condition has been met
        /// </summary>
        /// <param name="isMet">Whether the condition has been met</param>
        public void UpdateIsMet(bool isMet) {
            met.text = isMet ? MET_TEXT : UNMET_TEXT;
            met.color = isMet ? MET_COLOR : UNMET_COLOR;
        }
    }
}
