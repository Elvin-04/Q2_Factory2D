using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] Tilemap map;
    [SerializeField] private GameObject higlighted;

    private Camera mainCamera;

    [Header("Prefabs")]
    [SerializeField] private GameObject conveyorBeltPrefab;

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
        Vector3Int gridPosition = map.WorldToCell(Input.mousePosition);

        Ray ray = mainCamera.ScreenPointToRay(gridPosition);
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
        if (hit2D.collider != null && hit2D.collider.tag == "Floor")
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int positionToSpawn = map.WorldToCell(mousePos);

            GameObject conveyorBelt = Instantiate(conveyorBeltPrefab);
            conveyorBelt.transform.position = new Vector2(positionToSpawn.x + 0.5f, positionToSpawn.y + 0.5f);
        }
    }
}
