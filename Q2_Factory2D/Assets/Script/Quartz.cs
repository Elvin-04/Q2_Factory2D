using UnityEngine;

public class Quartz : MonoBehaviour
{
    [HideInInspector] public Vector2 destination;

    public float speed;
    public bool isMoving = false;

    Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if(destination != null && destination != Vector2.zero)
        {
            if (Vector2.Distance(transform.position, destination) >= 0.01f)
            {
                isMoving = true;
                transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            }
            else
            {
                isMoving = false;
                destination = Vector2.zero;
            }
        }
    }


}
