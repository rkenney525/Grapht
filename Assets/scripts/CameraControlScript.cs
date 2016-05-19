using UnityEngine;
using System.Collections;

/// <summary>
/// Controls zooming and panning while in the main game mode
/// </summary>
public class CameraControlScript : MonoBehaviour {

    /// <summary>
    /// The largest orthographic size the Camera can attain
    /// </summary>
    private const float MAX_ZOOM = 15f;

    /// <summary>
    /// The smallest orthographic size the Camera can attain
    /// </summary>
    private const float MIN_ZOOM = 2f;

    private const float ZOOM_CHANGE = 0.2f;

    /// <summary>
    /// Reference to the main Camera in the Scene
    /// </summary>
    private Camera camera;

    /// <summary>
    /// Load the Camera reference when this component is created
    /// </summary>
    void Start() {
        this.camera = this.GetComponent<Camera>();
    }

    /// <summary>
    /// Update camera position based on zoom/pan input
    /// </summary>
    void Update() {
        if (Input.mouseScrollDelta.y != 0.0) {
            // Take the inverse of the input so that scroll wheel up = zoom in (or -) and
            // scroll wheel down = zoom out (or +)
            this.HandleZoom(-Input.mouseScrollDelta.y);
        }
    }

    /// <summary>
    /// Handle a zooming action described by the delta in scroll wheel position
    /// </summary>
    /// <param name="delta">The change in scroll wheel position</param>
    private void HandleZoom(float delta) {
        // Get the changeInZoom
        float zoomChange = Mathf.Sign(delta) * ZOOM_CHANGE;

        // Get the new zoom
        float newZoom = this.camera.orthographicSize + zoomChange;

        // Apply the min and max
        newZoom = Mathf.Min(MAX_ZOOM, newZoom);
        newZoom = Mathf.Max(MIN_ZOOM, newZoom);

        // Set the zoom
        this.camera.orthographicSize = newZoom;
    }
}
