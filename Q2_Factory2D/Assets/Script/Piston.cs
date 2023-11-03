using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Piston : MonoBehaviour
{
    public float speed = 5f;
    public string direction;
    public int priority = 1;

    private Animation anim;

    bool pistonStarted = false;

    [SerializeField] private Transform detectionZone;

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
        bool done = false;
        if (TimeManager.instance.gameStarted)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(detectionZone.position, new Vector2(0.5f, 0.5f), 0);
           
            foreach(Collider2D collider in colliders)
            {
                if(collider.tag == "Material" && priority >= collider.GetComponent<Quartz>().actualPriority)
                {
                    done = true;
                    switch (direction)
                    {
                        case "left":
                            collider.gameObject.GetComponent<Quartz>().destination = detectionZone.position + new Vector3(-1, 0);
                            break;
                        case "right":
                            collider.gameObject.GetComponent<Quartz>().destination = detectionZone.position + new Vector3(1, 0);
                            break;
                        case "top":
                            collider.gameObject.GetComponent<Quartz>().destination = detectionZone.position + new Vector3(0, 1);
                            break;
                        case "down":
                            collider.gameObject.GetComponent<Quartz>().destination = detectionZone.position + new Vector3(0, -1);
                            break;
                    }
                }
            }
        }
        yield return new WaitForSeconds(TickManager.instance.tickrate);
        if(done) anim.Play();
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
