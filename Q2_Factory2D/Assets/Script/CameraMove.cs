using UnityEngine;

public class CameraMove : MonoBehaviour
{

    private Vector2 mousePosition;

    [SerializeField] private Transform cameraLimit;

    public float camSpeed = 5f;
    public float boundaryDistance = 10f;

    public float camMaxSize = 5;
    public float camMinSize = 1;
    public float zoomSpeed = 0.2f;


    void Update()
    {
        mousePosition = Input.mousePosition;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (mousePosition.x < boundaryDistance)
        {
            MoveCamera(Vector3.left);
        }
        else if (mousePosition.x > screenWidth - boundaryDistance)
        {
            MoveCamera(Vector3.right);
        }

        if (mousePosition.y < boundaryDistance)
        {
            MoveCamera(Vector3.down);
        }
        else if (mousePosition.y > screenHeight - boundaryDistance)
        {
            MoveCamera(Vector3.up);
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            ZoomIn();
        }
        else if(Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            ZoomOut();
        }
    }

    void MoveCamera(Vector3 direction)
    {
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(transform.position.x + direction.x, cameraLimit.position.x - cameraLimit.localScale.x / 2, cameraLimit.position.x + cameraLimit.localScale.x / 2),
            Mathf.Clamp(transform.position.y + direction.y, cameraLimit.position.y - cameraLimit.localScale.y / 2, cameraLimit.position.y + cameraLimit.localScale.y / 2),
            transform.position.z
        );

        transform.position = Vector3.Lerp(transform.position, clampedPosition, camSpeed * Time.deltaTime);
    }

    private void ZoomIn()
    {
        if(Camera.main.orthographicSize < camMaxSize)
            Camera.main.orthographicSize += zoomSpeed;
    }

    private void ZoomOut()
    {
        if (Camera.main.orthographicSize > camMinSize)
            Camera.main.orthographicSize -= zoomSpeed;
    }


}
