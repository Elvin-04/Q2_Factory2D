using System.Collections;
using UnityEngine;

public class MaterialSpawner : MonoBehaviour
{
    [SerializeField] private GameObject materialPrefab;
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private Transform spawnPoint;

    public IEnumerator SpawnMaterial()
    {
        if(TimeManager.instance.gameStarted)
        {
            GameObject material = Instantiate(materialPrefab);
            material.transform.position = transform.position;
            material.GetComponent<Quartz>().destination = spawnPoint.position;
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
