using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject[] howToPlayPanel;
    public GameObject settingPanel;

    private int currentPageIndex = 0;

    public void StartGame()
    {
        SceneManager.LoadScene("GameStart");
    }

    public void ShowHowToPlay()
    {
        currentPageIndex = 0;
        howToPlayPanel[0].SetActive(true);
    }

    public void OpenSettings()
    {
        settingPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        if (howToPlayPanel[currentPageIndex].activeSelf == true)
        {
            howToPlayPanel[currentPageIndex].SetActive(false);
            currentPageIndex = 0;
        }
        else if (settingPanel.activeSelf == true)
        {
            settingPanel.SetActive(false);
        }

    }

    public void Nextpage()
    {
        howToPlayPanel[currentPageIndex].SetActive(false);
        currentPageIndex++;
        howToPlayPanel[currentPageIndex].SetActive(true);
    }

    public void PreviousPage()
    {
        howToPlayPanel[currentPageIndex].SetActive(false);
        currentPageIndex--;
        howToPlayPanel[currentPageIndex].SetActive(true);
    }
}