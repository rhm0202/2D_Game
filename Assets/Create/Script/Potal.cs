using UnityEngine;

public class Portal : MonoBehaviour
{
    public string destinationSceneName;
    public bool lastStage;

    public void Enter()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(destinationSceneName);

        if (lastStage)
        {
            GameManager.Instance.clearCheck = true;
        }
    }
}