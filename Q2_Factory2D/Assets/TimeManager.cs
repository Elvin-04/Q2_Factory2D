using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public bool gameStarted;
    public static TimeManager instance {  get; private set; }

    public MaterialSpawner spawner;

    private void Awake()
    {
        instance = this;
    }

    public void PlayGame()
    {
        gameStarted = true;
        StartCoroutine(spawner.SpawnMaterial());
        SelectionManager.instance.Unselect();
    }

    public void StopGame()
    {
        gameStarted = false;
        GameManager.instance.DestroyAllMaterials();
    }
}
