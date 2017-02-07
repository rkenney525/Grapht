using System.Collections.Generic;
using UnityEngine;

namespace Grapht.Arch {
    /// <summary>
    /// Manages all UnityComponents on a Prefab/GameObject. Each Prefab/GameObject should have a single script that extends this and manages all UnityComponents. Extensions
    /// of this class should provide the interface for talking to other UnityEntity implementations
    /// </summary>
    public abstract class UnityEntity : MonoBehaviour {
        /// <summary>
        /// All UnityComponents in this GameObject/Prefab
        /// </summary>
        protected IEnumerable<UnityComponent> Components = new List<UnityComponent>();

        /// <summary>
        /// Called when all GameObjects have initializaed
        /// </summary>
        void Awake() {
            Components.ForEach(component => component.OnAwake());
        }
    }
}
