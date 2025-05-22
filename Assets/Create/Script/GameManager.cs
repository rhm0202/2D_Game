using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerResource playerResource;

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
