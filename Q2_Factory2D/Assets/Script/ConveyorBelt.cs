using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public string direction;
    public float speed = 1f;

    private void Start()
    {
        SetDirection();
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, Vector2.zero, 0);
        foreach (Collider2D collider in colliders)
        {
            if(collider.gameObject.tag == "Material")
            {
                SetDestination(collider);
            }
        }
    }

    private void SetDestination(Collider2D collider)
    {
        if(!collider.gameObject.GetComponent<Quartz>().isMoving)
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
}
