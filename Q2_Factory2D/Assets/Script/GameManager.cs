using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] Tilemap map;
    [SerializeField] private GameObject higlighted;

    private Camera mainCamera;

    private void Awake()
    {
        instance = this;
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = map.WorldToCell(mousePosition);

        if(map.GetTile(gridPosition) != null)
        {
            higlighted.SetActive(true);
            higlighted.transform.position = new Vector2(gridPosition.x + 0.5f, gridPosition.y + 0.5f);
        }
        else
        {
            higlighted.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PlaceConveyorBelt();
        }
    }

    public void PlaceConveyorBelt()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = map.WorldToCell(mousePosition);

        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);

        if (hit2D.collider != null)
        {
            Debug.Log(hit2D.collider.name);
        }
    }
}
