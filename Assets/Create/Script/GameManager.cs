using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerResource playerResource;

    public bool clearCheck = false;

    //점수 환산을 위한 변수
    public int kill = 0;

    public int BreakObject = 0;
    public int ObjectScore = 0;

    public int stageCheck = 0;
    public int[] StageClearTime;

    public int getItem;
    public int itemScore;

    public int highScore;

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

    public void ResetGameData()
    {
        kill = 0;
        BreakObject = 0;
        ObjectScore = 0;
        stageCheck = 0;
        getItem = 0;
        itemScore = 0;

        if (StageClearTime != null)
        {
            for (int i = 0; i < StageClearTime.Length; i++)
            {
                StageClearTime[i] = 0;
            }
        }
    }


}
