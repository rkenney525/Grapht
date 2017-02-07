using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Grapht.Arch;

namespace Grapht.Node {
    /// <summary>
    /// The total branch value to be displayed under a node
    /// </summary>
    public class ValueDisplayScript : UnityComponent {

        /// <summary>
        /// The Text field containing the branch value
        /// </summary>
        private Text text;

        /// <summary>
        /// Get the Text field when this component is created
        /// </summary>
        public override void OnAwake() {
            text = GetComponentInChildren<Text>();
        }

        /// <summary>
        /// Display the specified value
        /// </summary>
        /// <param name="sum">The value to display</param>
        public void Display(int sum) {
            SetValue(sum);
            Enable();
        }

        /// <summary>
        /// Display the branch value
        /// </summary>
        public void Enable() {
            text.enabled = true;
        }

        /// <summary>
        /// Stop displaying the branch value
        /// </summary>
        public void Disable() {
            text.enabled = false;
        }

        /// <summary>
        /// Set the numeric value
        /// </summary>
        /// <param name="value">The value to set</param>
        public void SetValue(int value) {
            text.text = value.ToString();
        }
    }
}
