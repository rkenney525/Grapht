using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CameraControl : MonoBehaviour
{

    public float MinX = -10f, MaxX = 10f;
    public float MinY = -5f, MaxY = -5f;
    public float MaximumZoom = 90f, MinimumZoom = 60f;
    private bool ortho = false;
    private Camera cameraRef;
    private Vector3 dragStart = new Vector2(0f, 0f);

    // Use this for initialization
    void Start()
    {
        this.cameraRef = this.GetComponent<Camera>();
        this.ortho = cameraRef.orthographic;

    }

    void ChangePosition(Vector3 movement)
    {
        Vector3 newPos = this.cameraRef.transform.position + this.cameraRef.ScreenToWorldPoint(movement);
        //Vector3 newPos = movement;
        //Vector3 newPos = this.cameraRef.transform.(movement);
        newPos.z = this.cameraRef.transform.position.z;
        //   newPos *= 0.5f; // account for half the screen
        //newPos = (newPos * 2.0f) - new Vector3(1.0f,1.0f,0.0f);
        //newPos.z = originalZ;
        //   newPos.x = Mathf.Min(Mathf.Max(newPos.x, MinX), MaxX);
        //  newPos.y = Mathf.Min(Mathf.Max(newPos.y, MinY), MaxY);
        this.cameraRef.transform.position = Vector3.Lerp(this.cameraRef.transform.position, newPos, 0.2f);
    }

    /// <summary>
    /// Change camera FOV based on scroll delta
    /// </summary>
    /// <param name="delta"></param>
    void ChangeZoom(float delta)
    {
        float zoomLevel = (!ortho)? this.cameraRef.fieldOfView : this.cameraRef.orthographicSize;
        float changeAmount = (ortho) ? 0.1f : 1.0f;

        float zoomChange = Mathf.Sign(delta) * changeAmount;
        float previewZoom = zoomLevel + zoomChange;
        
        if (previewZoom >= MinimumZoom && previewZoom <= MaximumZoom)
        {
            if (ortho)
            {
                cameraRef.orthographicSize = previewZoom;
            }
            else
            {
                this.cameraRef.fieldOfView = previewZoom;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    dragStart = Input.mousePosition;
        //}
        //if (Input.GetMouseButton(1))
        //{
        //    ChangePosition(dragStart - Input.mousePosition);
        //}
        if (Input.mouseScrollDelta.y != 0.0)
        {
            ChangeZoom(Input.mouseScrollDelta.y);
        }
    }
}
