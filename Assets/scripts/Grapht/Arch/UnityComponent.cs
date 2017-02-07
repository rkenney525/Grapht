using UnityEngine;

namespace Grapht.Arch {
    /// <summary>
    /// The base class for Unity Components. Each piece of functionality that uses the Unity Engine in some may must be contained in a script that extends this class.
    /// </summary>
    public abstract class UnityComponent : MonoBehaviour {
        /// <summary>
        /// Initialize all external references - essentially the logic you would want in MonoBehavior.Awake, because that's when it will be called. Awake gets called once
        /// every GameObject has initialized.
        /// </summary>
        public abstract void OnAwake();
    }
}
