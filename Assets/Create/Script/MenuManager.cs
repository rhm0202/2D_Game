using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;

    private bool isMenuOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        menuPanel.SetActive(isMenuOpen);

        // 일시 정지
        Time.timeScale = isMenuOpen ? 0f : 1f;
    }

    public void OnContinueClicked()
    {
        ToggleMenu(); // 메뉴 닫고 다시 진행
    }

    public void OnQuitClicked()
    {
        Time.timeScale = 1f; // 일시 정지 해제 후 종료
        SceneManager.LoadScene("Title"); // 혹은 Application.Quit(); 빌드 시
    }
}
