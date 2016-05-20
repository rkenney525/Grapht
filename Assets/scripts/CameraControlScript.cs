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

    /// <summary>
    /// The change in orthagraphic size per zoom action
    /// </summary>
    private const float ZOOM_CHANGE = 0.2f;

    /// <summary>
    /// The mouse button used to control panning
    /// </summary>
    private const int PANNING_BUTTON = 1;

    /// <summary>
    /// The farthest up the camera can display, set to a fully zoomed out view
    /// </summary>
    private const float WORLD_LIMIT_TOP = MAX_ZOOM;

    /// <summary>
    /// The farthest down the camera can display, set to a fully zoomed out view
    /// </summary>
    private const float WORLD_LIMIT_BOTTOM = -MAX_ZOOM;

    /// <summary>
    /// The farthest left the camera can display, set to a fully zoomed out view
    /// </summary>
    private float WORLD_LIMIT_LEFT;

    /// <summary>
    /// The farthest right the camera can display, set to a fully zoomed out view
    /// </summary>
    private float WORLD_LIMIT_RIGHT;

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
    void Start() {
        WORLD_LIMIT_LEFT = WORLD_LIMIT_BOTTOM * (Screen.width / Screen.height);
        WORLD_LIMIT_RIGHT = WORLD_LIMIT_TOP * (Screen.width / Screen.height);
        this.cam = this.GetComponent<Camera>();
        this.previousMousePosition = Vector3.zero;
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
        // TODO factor in the world limits
        Vector3 delta = this.previousMousePosition - position;
        this.transform.position += delta;
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
        newZoom = Mathf.Min(MAX_ZOOM, newZoom);
        newZoom = Mathf.Max(MIN_ZOOM, newZoom);

        // Set the zoom
        this.cam.orthographicSize = newZoom;
    }
}
