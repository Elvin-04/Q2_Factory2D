using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Piston : MonoBehaviour
{
    public float timeBetweendPistonAction = 1f;
    public float speed = 5f;
    public string direction;

    private Animation anim;

    bool pistonStarted = false;

    private void Start()
    {
        anim = GetComponent<Animation>();
        SetDirection();
    }

    private void FixedUpdate()
    {
        if(TimeManager.instance.gameStarted && !pistonStarted)
        {
            pistonStarted = true;
            StartCoroutine(PistonAction());
        }
        else if (!TimeManager.instance.gameStarted && pistonStarted)
        {
            pistonStarted = false;
        }
    }

    IEnumerator PistonAction()
    {
        if(TimeManager.instance.gameStarted)
        {
            anim.Play();
            Collider2D[] colliders = { };
            switch(direction)
            {
                case "left":
                    colliders = Physics2D.OverlapBoxAll(transform.position + new Vector3(-1, 0), new Vector2(0.5f, 0.5f), 0);
                    break;
                case "right":
                    colliders = Physics2D.OverlapBoxAll(transform.position + new Vector3(1, 0), new Vector2(0.5f, 0.5f), 0);
                    break;
                case "top":
                    colliders = Physics2D.OverlapBoxAll(transform.position + new Vector3(0, 1), new Vector2(0.5f, 0.5f), 0);
                    break;
                case "down":
                    colliders = Physics2D.OverlapBoxAll(transform.position + new Vector3(0, -1), new Vector2(0.5f, 0.5f), 0);
                    break;
            }

            foreach(Collider2D collider in colliders)
            {
                if(collider.tag == "Material")
                {
                    switch (direction)
                    {
                        case "left":
                            collider.gameObject.GetComponent<Quartz>().destination = transform.position + new Vector3(-2, 0);
                            break;
                        case "right":
                            collider.gameObject.GetComponent<Quartz>().destination = transform.position + new Vector3(2, 0);
                            break;
                        case "top":
                            collider.gameObject.GetComponent<Quartz>().destination = transform.position + new Vector3(0, 2);
                            break;
                        case "down":
                            collider.gameObject.GetComponent<Quartz>().destination = transform.position + new Vector3(0, -2);
                            break;
                    }

                    collider.gameObject.GetComponent<Quartz>().speed = speed;
                }
            }

            
        }
        yield return new WaitForSeconds(timeBetweendPistonAction);
        StartCoroutine(PistonAction());
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
}
