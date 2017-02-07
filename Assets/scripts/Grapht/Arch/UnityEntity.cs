using System.Collections.Generic;
using UnityEngine;

namespace Grapht.Arch {
    /// <summary>
    /// Manages all UnityComponents on a Prefab/GameObject. Each Prefab/GameObject should have a single instance (or extension) of this.
    /// </summary>
    public class UnityEntity : MonoBehaviour {
        /// <summary>
        /// Called when all GameObjects have initializaed
        /// </summary>
        public void Awake() {
            GetComponentsInChildren<UnityComponent>().ForEach(component => component.OnAwake());
        }
    }
}
