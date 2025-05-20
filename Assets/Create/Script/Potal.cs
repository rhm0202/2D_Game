using UnityEngine;

public class Portal : MonoBehaviour
{
    public string destinationSceneName;

    public void Enter()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(destinationSceneName);
    }
}