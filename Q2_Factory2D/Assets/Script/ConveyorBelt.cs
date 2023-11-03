using System.Collections;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public string direction;
    public float speed = 1f;
    public int priority = 0;

    bool conveyorStarted = false;

    private void Start()
    {
        SetDirection();
    }

    private void FixedUpdate()
    {
        if (TimeManager.instance.gameStarted && !conveyorStarted)
        {
            conveyorStarted = true;
            StartCoroutine(ConveyorBeltAction());
        }
        else if (!TimeManager.instance.gameStarted && conveyorStarted)
        {
            conveyorStarted = false;
        }
    }

    private void SetDestination(Collider2D collider)
    {
        if(!collider.gameObject.GetComponent<Quartz>().isMoving && priority >= collider.GetComponent<Quartz>().actualPriority)
        {
            switch (direction)
            {
                case "left":
                    collider.gameObject.GetComponent<Quartz>().destination = new Vector2(transform.position.x - 1, transform.position.y);
                    break;
                case "right":
                    collider.gameObject.GetComponent<Quartz>().destination = new Vector2(transform.position.x + 1, transform.position.y);
                    break;
                case "top":
                    collider.gameObject.GetComponent<Quartz>().destination = new Vector2(transform.position.x, transform.position.y + 1);
                    break;
                case "down":
                    collider.gameObject.GetComponent<Quartz>().destination = new Vector2(transform.position.x, transform.position.y - 1);
                    break;
            }
            collider.gameObject.GetComponent<Quartz>().speed = speed;
        }
        
    }

    public void SetDirection()
    {
        switch (GameManager.instance.rotation)
        {
            case -90:
                direction = "right";
                break;
            case -270:
                direction = "left";
                break;
            case 0:
                direction = "top";
                break;
            case -180:
                direction = "down";
                break;
        }
    }

    IEnumerator ConveyorBeltAction()
    {
        if(TimeManager.instance.gameStarted)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, Vector2.zero, 0);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.tag == "Material")
                {
                    SetDestination(collider);
                }
            }
        }
        yield return new WaitForSeconds(TickManager.instance.tickrate);
        StartCoroutine(ConveyorBeltAction());
    }
}
