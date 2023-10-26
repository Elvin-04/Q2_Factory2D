using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor;
    [SerializeField] private Color offsetColor;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject conveyorBeltPreview;

    public void Init(bool isOffset)
    {
        spriteRenderer.color = isOffset ? offsetColor : baseColor;
    }

    private void OnMouseEnter()
    {
        if(GameManager.instance.actualSelected == "Conveyor Belt")
        {
            conveyorBeltPreview.SetActive(true);
        }
        else
        {
            highlight.SetActive(true);
        }
    }

    private void OnMouseOver()
    {
        if(conveyorBeltPreview.activeSelf)
        {
            conveyorBeltPreview.transform.rotation = Quaternion.Euler(0, 0, GameManager.instance.rotation);
        }
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
        conveyorBeltPreview.SetActive(false);
    }

}
