using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public string actualSelected = "";

    public int rotation = 0;

    [SerializeField] private TextMeshProUGUI actualSelectedText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SelectItem("");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            rotation -= 90;
        }
    }

    public void SelectItem(string item)
    {
        actualSelected = item;
        actualSelectedText.text = actualSelected;
    }
}
