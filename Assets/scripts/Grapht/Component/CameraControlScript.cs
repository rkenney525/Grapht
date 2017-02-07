using UnityEngine;
using Grapht.Arch;

namespace Grapht.Component {
    /// <summary>
    /// Controls zooming and panning while in the main game mode
    /// </summary>
    public class CameraControlScript : UnityComponent {
        /// <summary>
        /// The change in orthagraphic size per zoom action
        /// </summary>
        private const float ZOOM_CHANGE = 0.2f;

        /// <summary>
        /// The mouse button used to control panning
        /// </summary>
        private const int PANNING_BUTTON = 1;

        /// <summary>
        /// Reference to the main Camera in the Scene
        /// </summary>
        private Camera cam;

        /// <summary>
        /// The location the mouse was located during the update call
        /// </summary>
        private Vector3 previousMousePosition;

        /// <summary>
        /// Load the Camera reference and other properties when this component is created
        /// </summary>
        public override void OnAwake() {
            cam = GetComponent<Camera>();
            previousMousePosition = Vector3.zero;
        }

        /// <summary>
        /// Update camera position based on zoom/pan input
        /// </summary>
        void Update() {
            // Handle zooming
            if (Input.mouseScrollDelta.y != 0.0) {
                // Take the inverse of the input so that scroll wheel up = zoom in (or -) and
                // scroll wheel down = zoom out (or +)
                this.HandleZoom(-Input.mouseScrollDelta.y);
            }

            // Handle panning
            if (Input.GetMouseButton(PANNING_BUTTON)) {
                this.HandlePan(this.cam.ScreenToWorldPoint(Input.mousePosition));
            }

            // Track the mouse position
            this.previousMousePosition = this.cam.ScreenToWorldPoint(Input.mousePosition);
        }

        /// <summary>
        /// Move the camera based on the delta in mouse position
        /// </summary>
        /// <param name="position"></param>
        void HandlePan(Vector3 position) {
            // Calculate the delta in the mouse position
            Vector3 delta = this.previousMousePosition - position;

            // Calculate the bounded position the camera should take
            Vector3 boundedPosition = this.BoundCameraPosition(this.transform.position + delta);

            // Set the new camera position
            this.transform.position = boundedPosition;
        }

        /// <summary>
        /// Handle a zooming action described by the delta in scroll wheel position
        /// </summary>
        /// <param name="delta">The change in scroll wheel position</param>
        private void HandleZoom(float delta) {
            // Get the changeInZoom
            float zoomChange = Mathf.Sign(delta) * ZOOM_CHANGE;

            // Get the new zoom
            float newZoom = this.cam.orthographicSize + zoomChange;

            // Apply the min and max
            newZoom = this.ApplyBounds(newZoom, ViewProperties.MAX_ZOOM, ViewProperties.MIN_ZOOM);

            // Set the zoom
            this.cam.orthographicSize = newZoom;

            // Adjust the camera position based on the zoom
            this.transform.position = this.BoundCameraPosition(this.transform.position);
        }

        /// <summary>
        /// Bound the provided position to the world view. A position is bounded if it does not allow area outside the bounds to be viewed
        /// </summary>
        /// <param name="position">The camera position</param>
        private Vector3 BoundCameraPosition(Vector3 position) {
            // Get the view limits based on the zoom
            float verticalView = this.cam.orthographicSize;
            float horizontalView = verticalView * ViewProperties.SCREEN_RATIO;

            // Bound the position based on the zoom
            position.x = this.ApplyBounds(position.x, ViewProperties.WORLD_LIMIT_RIGHT - horizontalView, ViewProperties.WORLD_LIMIT_LEFT + horizontalView);
            position.y = this.ApplyBounds(position.y, ViewProperties.WORLD_LIMIT_TOP - verticalView, ViewProperties.WORLD_LIMIT_BOTTOM + verticalView);

            // Return the position
            return position;
        }

        /// <summary>
        /// Returns value, guaranteed to be no larger than max and no smaller than min
        /// </summary>
        /// <param name="value">The value to transform</param>
        /// <param name="max">The maximum possible value</param>
        /// <param name="min">The minimum possible value</param>
        /// <returns>The value bounded by max and min</returns>
        private float ApplyBounds(float value, float max, float min) {
            value = Mathf.Min(max, value);
            value = Mathf.Max(min, value);
            return value;
        }
    }
}
