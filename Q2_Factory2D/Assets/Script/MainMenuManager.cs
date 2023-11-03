using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPannel;
    [SerializeField] private GameObject levelSelectionPannel;

    public void Play()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void LevelSelection()
    {
        levelSelectionPannel.SetActive(true);
        mainMenuPannel.SetActive(false);
    }

    public void Back()
    {
        levelSelectionPannel.SetActive(false);
        mainMenuPannel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
