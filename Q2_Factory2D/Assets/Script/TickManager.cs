using System.Collections;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    public static TickManager instance { get; private set; }

    public float tickrate;

    private void Awake()
    {
        instance = this;
    }

    public void StartTickRate()
    {
        StartCoroutine(Tick());
    }

    IEnumerator Tick()
    {
        if(TimeManager.instance.gameStarted)
        {
            yield return new WaitForSeconds(tickrate);
            StartCoroutine(Tick());

            foreach (GameObject mat in GameManager.instance.allMaterials)
            {
                mat.GetComponent<Quartz>().Tick();
            }


        }
        else
        {
            yield return null;
        }
    }
}
