using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

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

    [Header("UI")]
    [SerializeField] private GameObject helpPannel;
    [SerializeField] private TextMeshProUGUI conveyorBeltLimitText;
    [SerializeField] private TextMeshProUGUI pistonLimitText;

    [Header("Game")]
    public int conveyorBeltLimit;
    public int pistonLimit;
    int coveyorBeltPlaced;
    int pistonPlaced;

    private void Awake()
    {
        instance = this;
        mainCamera = Camera.main;
    }

    private void Start()
    {
        coveyorBeltPlaced = conveyorBeltLimit;
        pistonPlaced = pistonLimit;
        SetTextPlacedElement();
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
            SetTextPlacedElement();
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
        if (hit2D.collider != null && hit2D.collider.tag == "Floor" && coveyorBeltPlaced > 0)
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int positionToSpawn = map.WorldToCell(mousePos);

            GameObject conveyorBelt = Instantiate(conveyorBeltPrefab);
            conveyorBelt.transform.position = new Vector2(positionToSpawn.x + 0.5f, positionToSpawn.y + 0.5f);
            conveyorBelt.transform.rotation = Quaternion.Euler(0, 0, rotation);

            coveyorBeltPlaced--;


        }
    }

    public void PlacePiston()
    {
        Vector3Int gridPosition = map.WorldToCell(Input.mousePosition);

        Ray ray = mainCamera.ScreenPointToRay(gridPosition);
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
        if (hit2D.collider != null && hit2D.collider.tag == "Floor" && pistonPlaced > 0)
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int positionToSpawn = map.WorldToCell(mousePos);

            GameObject piston = Instantiate(pistonPrefab);
            piston.transform.position = new Vector2(positionToSpawn.x + 0.5f, positionToSpawn.y + 0.5f);
            piston.transform.rotation = Quaternion.Euler(0, 0, rotation);

            pistonPlaced--;

        }
    }

    private void SetTextPlacedElement()
    {
        conveyorBeltLimitText.text = coveyorBeltPlaced.ToString();
        pistonLimitText.text = pistonPlaced.ToString();
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
        allMaterials.Clear();
    }


    public void Retry()
    {
        Scene actualLoadedScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(actualLoadedScene.name);
    }

    public void OpenHelpPannel()
    {
        helpPannel.SetActive(true);
    }

    public void CloseHelpPannel()
    {
        helpPannel.SetActive(false);
    }

    public void BackMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
