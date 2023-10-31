using System.Collections;
using UnityEngine;

public class MaterialSpawner : MonoBehaviour
{
    [SerializeField] private GameObject materialPrefab;
    [SerializeField] private string spawnDirection;
    [SerializeField] private float timeBetweenSpawn;

    private Vector2 direction;

    private void Start()
    {
        switch (spawnDirection)
        {
            case "left":
                direction = new Vector2(transform.position.x - 1, transform.position.y);
                break;
            case "right":
                direction = new Vector2(transform.position.x + 1, transform.position.y);
                break;
            case "top":
                direction = new Vector2(transform.position.x, transform.position.y + 1);
                break;
            case "down":
                direction = new Vector2(transform.position.x, transform.position.y - 1);
                break;
            default:
                direction = new Vector2();
                break;
        }
    }

    public IEnumerator SpawnMaterial()
    {
        if(TimeManager.instance.gameStarted)
        {
            GameObject material = Instantiate(materialPrefab);
            material.transform.position = transform.position;
            material.GetComponent<Quartz>().destination = direction;
            GameManager.instance.allMaterials.Add(material);
            yield return new WaitForSeconds(timeBetweenSpawn);
            StartCoroutine(SpawnMaterial());
        }
        else
        {
            yield return null;
        }
    }


}
