using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject howToPlayPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowHowToPlay()
    {
        howToPlayPanel.SetActive(true);
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
        howToPlayPanel.SetActive(false);
    }
}