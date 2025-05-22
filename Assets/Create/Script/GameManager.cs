using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerResource playerResource;

    //점수 환산을 위한 변수
    public int kill = 0;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            playerResource.ResetData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        playerResource.TakeDamage(amount);
    }

    public void Heal(int amount)
    {
        playerResource.Heal(amount);
    }

    public void UseAmmo(int amount)
    {
        playerResource.UseAmmo(amount);
    }

    public void Reload()
    {
        playerResource.Reload();
    }

    public void AddItem(int count)
    {
        playerResource.AddItem(count);
    }


}
