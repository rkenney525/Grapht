using UnityEngine.UI;
using Grapht.Arch;

namespace Grapht.Node {
    /// <summary>
    /// Captures the numeric value of a single node
    /// </summary>
    public class NumericValueScript : UnityComponent {

        /// <summary>
        /// The Text field containing the numeric value
        /// </summary>
        private Text text;

        /// <summary>
        /// Get the Text field when this component is created
        /// </summary>
        public override void OnAwake() {
            text = GetComponentInChildren<Text>();
        }

        /// <summary>
        /// Get the numeric value of this node
        /// </summary>
        /// <returns>The numeric value stored in the Text field</returns>
        public int Value() {
            return int.Parse(text.text);
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
