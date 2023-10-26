using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private string direction;

    private void Start()
    {
        switch (direction)
        {
            case "left":
                transform.rotation = Quaternion.Euler(0,0, 90);
                break;
            case "right":
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case "top":
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case "down":
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
        }
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
        }
        
    }
}
