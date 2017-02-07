using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Grapht.Arch {
    /// <summary>
    /// Manages all UnityComponents on a Prefab/GameObject. Each Prefab/GameObject should have a single script that extends this and manages all UnityComponents.
    /// </summary>
    public abstract class UnityEntity : MonoBehaviour {
        /// <summary>
        /// All UnityComponents in this GameObject/Prefab
        /// </summary>
        protected IEnumerable<UnityComponent> Components = new List<UnityComponent>();

        void Awake() {

        }
    }
}
