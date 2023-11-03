using System.Collections;
using UnityEngine;

public class Quartz : MonoBehaviour
{
    public Vector2 destination;

    public float speed;
    public bool isMoving = false;

    public int actualPriority = 0;


    Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    public void Tick()
    {
        actualPriority = 0;
        if (destination != Vector2.zero)
        {
            StartCoroutine(MoveQuartz(destination));
        }
    }

    IEnumerator MoveQuartz(Vector2 targetPosition)
    {
        Vector3 startposition = transform.position;
        float timeElapsed = 0;

        while(timeElapsed < TickManager.instance.tickrate)
        {
            transform.position = Vector2.Lerp(startposition, targetPosition, timeElapsed / TickManager.instance.tickrate);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }


}
