using TMPro;
using UnityEngine;
using UnityEngine.WSA;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance {  get; private set; }

    public int materials;
    public int neededMaterial = 5;
    [SerializeField] private GameObject winPannel;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetText();
    }

    public void AddScore()
    {
        materials++;
        if(materials == neededMaterial)
        {
            winPannel.SetActive(true);
        }
        SetText();
    }

    private void SetText()
    {
        scoreText.text = materials + "/" + neededMaterial;
    }
}
