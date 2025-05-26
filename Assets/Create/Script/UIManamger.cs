using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI StageName;

    public StageData stageData;

    private float timeLimit;
    private float currentTime;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        timeLimit = stageData != null ? stageData.timeLimit : 180f;
        currentTime = timeLimit;

        // 초기 UI 값 설정
        UpdateHP();
        UpdateAmmo();
        UpdateStage();
        if (stageData.stageName != "Start")
        {
            UpdateTime();
        }
        
    }

    void Update()
    {
        if (stageData.stageName != "Start")
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0)
            currentTime = 0;

        UpdateTime();
    }

    private void UpdateTime()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        int milliseconds = Mathf.FloorToInt((currentTime * 100) % 100);

        timeText.text = $"Time: {minutes:00}:{seconds:00}:{milliseconds:00}";
    }

    public void UpdateHP()
    {
        var res = GameManager.Instance.playerResource;
        hpText.text = $"HP: {res.currentHP} / {res.maxHP}";
    }
    public void UpdateStage()
    {
        var res = UIManager.Instance.stageData;
        StageName.text = $"Stage: {res.stageName}";
    }

    public void UpdateAmmo()
    {
        if (GameManager.Instance != null)
        {
            var res = GameManager.Instance.playerResource;
            ammoText.text = $"Ammo: {res.currentAmmo}/{res.maxAmmo}";
        }
    }
}