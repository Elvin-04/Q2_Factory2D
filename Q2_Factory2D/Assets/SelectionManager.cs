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
        if(!TimeManager.instance.gameStarted)
        {
            switch (id)
            {
                case 0:
                    actualSelected = "Conveyor Belt";
                    break;
                case 1:
                    actualSelected = "Piston";
                    break;
                default:
                    actualSelected = "";
                    break;
            }
            actualSelectedtext.text = actualSelected;
        }
    }

    public void Unselect()
    {
        actualSelected = "";
        actualSelectedtext.text = actualSelected;
    }
}
