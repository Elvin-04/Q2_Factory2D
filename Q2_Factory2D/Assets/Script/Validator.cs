using UnityEngine;

public class Validator : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("tets");
        if(collision.gameObject.tag == "Material")
        {
            ScoreManager.instance.AddScore();
        }
    }
}
