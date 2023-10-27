using TMPro;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance {  get; private set; }

    [SerializeField] private TextMeshProUGUI actualSelectedtext;

    public string actualSelected;

    private void Awake()
    {
        instance = this;
    }

    public void SetActualSelected(int id)
    {
        switch (id)
        {
            case 0:
                actualSelected = "Conveyor Belt";
                break;
            default:
                actualSelected = "";
                break;
        }
        actualSelectedtext.text = actualSelected;

    }
}
