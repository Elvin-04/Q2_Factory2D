using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] Tilemap map;

    private Camera mainCamera;
    private float rotation = 0;

    [Header("Prefabs")]
    [SerializeField] private GameObject conveyorBeltPrefab;

    [Header("Preview")]
    [SerializeField] private GameObject higlighted;
    [SerializeField] private GameObject previewConveyorBelt;

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
            if(SelectionManager.instance.actualSelected == "Conveyor Belt")
            {
                previewConveyorBelt.SetActive(true);
                previewConveyorBelt.transform.position = new Vector2(gridPosition.x + 0.5f, gridPosition.y + 0.5f);
                previewConveyorBelt.transform.rotation = Quaternion.Euler(0, 0, rotation);
            }
            else
            {
                higlighted.SetActive(true);
                higlighted.transform.position = new Vector2(gridPosition.x + 0.5f, gridPosition.y + 0.5f);
            }
            
        }
        else
        {
            higlighted.SetActive(false);
            previewConveyorBelt.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(SelectionManager.instance.actualSelected == "Conveyor Belt") {
                PlaceConveyorBelt();
            }
            
            //SelectionManager.instance.SetActualSelected(100);
           
            higlighted.SetActive(false);
            previewConveyorBelt.SetActive(false);
            rotation = 0;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            rotation -= 90;
            if (rotation < -270)
                rotation = 0;
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
            conveyorBelt.transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }
}
