using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public Tilemap map;

    private Camera mainCamera;
    [HideInInspector] public float rotation = 0;

    [Header("Prefabs")]
    [SerializeField] private GameObject conveyorBeltPrefab;
    [SerializeField] private GameObject pistonPrefab;

    [Header("Preview")]
    [SerializeField] private GameObject higlighted;
    [SerializeField] private GameObject previewConveyorBelt;
    [SerializeField] private GameObject previewPiston;

    [Header("Materials")]
    public List<GameObject> allMaterials;

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
            else if(SelectionManager.instance.actualSelected == "Piston")
            {
                previewPiston.SetActive(true);
                previewPiston.transform.position = new Vector2(gridPosition.x + 0.5f, gridPosition.y + 0.5f);
                previewPiston.transform.rotation = Quaternion.Euler(0, 0, rotation);
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
            previewPiston.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (SelectionManager.instance.actualSelected == "Conveyor Belt")
            {
                PlaceConveyorBelt();
            }
            else if (SelectionManager.instance.actualSelected == "Piston")
            {
                PlacePiston();
            }
            else
            {
                rotation = 0;
            }
           
            higlighted.SetActive(false);
            previewConveyorBelt.SetActive(false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            SelectionManager.instance.SetActualSelected(100);
            higlighted.SetActive(false);
            previewConveyorBelt.SetActive(false);
            previewPiston.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            rotation -= 90;
            if (rotation < -270)
                rotation = 0;
        }

        if(Input.GetKeyDown(KeyCode.D) && !TimeManager.instance.gameStarted)
        {
            RemoveElement();
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

    public void PlacePiston()
    {
        Vector3Int gridPosition = map.WorldToCell(Input.mousePosition);

        Ray ray = mainCamera.ScreenPointToRay(gridPosition);
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
        if (hit2D.collider != null && hit2D.collider.tag == "Floor")
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int positionToSpawn = map.WorldToCell(mousePos);

            GameObject piston = Instantiate(pistonPrefab);
            piston.transform.position = new Vector2(positionToSpawn.x + 0.5f, positionToSpawn.y + 0.5f);
            piston.transform.rotation = Quaternion.Euler(0, 0, rotation);

        }
    }

    private void RemoveElement()
    {
        Vector3Int gridPosition = map.WorldToCell(Input.mousePosition);

        Ray ray = mainCamera.ScreenPointToRay(gridPosition);
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
        if (hit2D.collider != null && hit2D.collider.tag != "Floor" && hit2D.collider.tag != "Material")
        {
            Destroy(hit2D.collider.gameObject);
        }
    }

    public void DestroyAllMaterials()
    {
        foreach(GameObject material in allMaterials)
        {
            Destroy(material);
        }
    }


    public void Retry()
    {
        Scene actualLoadedScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(actualLoadedScene.name);
    }
}
