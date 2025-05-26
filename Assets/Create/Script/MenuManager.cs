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

        // �Ͻ� ����
        Time.timeScale = isMenuOpen ? 0f : 1f;
    }

    public void OnContinueClicked()
    {
        ToggleMenu(); // �޴� �ݰ� �ٽ� ����
    }

    public void OnQuitClicked()
    {
        Time.timeScale = 1f; // �Ͻ� ���� ���� �� ����
        SceneManager.LoadScene("Title"); // Ȥ�� Application.Quit(); ���� ��
    }
}
