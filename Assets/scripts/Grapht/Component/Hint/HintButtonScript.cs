using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using Grapht.Arch;

namespace Grapht.Component.Hint {
    public class HintButtonScript : UnityComponent {

        /// <summary>
        /// The HintEntry prefab
        /// </summary>
        private GameObject HintEntryRef;

        /// <summary>
        /// The height of a particular entry. Easier to hardcode this than try to get at runtime
        /// </summary>
        private float ENTRY_HEIGHT = 40.0f;

        /// <summary>
        /// The distance between the hint button and the first hint
        /// </summary>
        private float INITIAL_OFFSET = -80.0f;

        /// <summary>
        /// List of HintEntry objects on screen
        /// </summary>
        private IList<GameObject> entries = new List<GameObject>();

        /// <summary>
        /// Whether to display the hints or not
        /// </summary>
        private bool display = false;

        /// <summary>
        /// Load external references
        /// </summary>
        public override void OnAwake() {
            HintEntryRef = Resources.Load<GameObject>("prefabs/HintEntry");
        }

        /// <summary>
        /// Create displayable hints, hidden by default
        /// </summary>
        public void LoadHintEntries(IList<string> hints) {
            // Remove any existing hints
            Clear();

            // Create the HintEntries
            int entryNumber = 0;
            entries = hints.Select(hint => {
                Vector2 position = new Vector2(0, INITIAL_OFFSET - (ENTRY_HEIGHT * entryNumber++));
                GameObject entryObj = Instantiate(HintEntryRef);
                entryObj.transform.SetParent(this.transform, false);
                entryObj.GetComponent<RectTransform>().anchoredPosition = position;
                entryObj.GetComponent<Text>().text = hint;
                entryObj.SetActive(false);
                return entryObj;
            }).ToList();
        }

        /// <summary>
        /// Toggle the display of the level's hints on or off.
        /// </summary>
        public void ToggleDisplay() {
            display = !display;
            entries.ForEach(entry => {
                entry.SetActive(display);
            });
        }

        /// <summary>
        /// Destroy all HintEntry objects on screen
        /// </summary>
        private void Clear() {
            entries.ForEach(hintEntry => {
                Destroy(hintEntry);
            });
            display = false;
        }
    }
}