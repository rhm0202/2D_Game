using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject[] howToPlayPanel;

    private int currentPageIndex = 0;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowHowToPlay()
    {
        howToPlayPanel[0].SetActive(true);
    }

    public void OpenSettings()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        howToPlayPanel[currentPageIndex].SetActive(false);
    }
}