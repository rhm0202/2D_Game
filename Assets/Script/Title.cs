using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game"); 

    public void ShowHowToPlay()
    {
        
    }

    public void OpenSettings()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}