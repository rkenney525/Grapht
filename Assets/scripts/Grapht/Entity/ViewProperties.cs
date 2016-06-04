using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Grapht.Entity {
    /// <summary>
    /// Static properties relating to how the game is viewed
    /// </summary>
    class ViewProperties {
        /// <summary>
        /// The largest orthographic size the Camera can attain
        /// </summary>
        public const float MAX_ZOOM = 15f;

        /// <summary>
        /// The smallest orthographic size the Camera can attain
        /// </summary>
        public const float MIN_ZOOM = 2f;

        /// <summary>
        /// The farthest up the camera can display, set to a fully zoomed out view
        /// </summary>
        public const float WORLD_LIMIT_TOP = MAX_ZOOM;

        /// <summary>
        /// The farthest down the camera can display, set to a fully zoomed out view
        /// </summary>
        public const float WORLD_LIMIT_BOTTOM = -MAX_ZOOM;

        /// <summary>
        /// The farthest left the camera can display, set to a fully zoomed out view
        /// </summary>
        public static float WORLD_LIMIT_LEFT;

        /// <summary>
        /// The farthest right the camera can display, set to a fully zoomed out view
        /// </summary>
        public static float WORLD_LIMIT_RIGHT;

        /// <summary>
        /// The rato of to width to height for the game
        /// </summary>
        public static float SCREEN_RATIO;

        /// <summary>
        /// Set dynamic static properties on class load
        /// </summary>
        static ViewProperties() {
            SCREEN_RATIO = (float)Screen.width / (float)Screen.height;
            WORLD_LIMIT_LEFT = WORLD_LIMIT_BOTTOM * SCREEN_RATIO;
            WORLD_LIMIT_RIGHT = WORLD_LIMIT_TOP * SCREEN_RATIO;
        }
    }
}
