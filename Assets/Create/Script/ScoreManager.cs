using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI gameOver;
    public TextMeshProUGUI killcount;
    public TextMeshProUGUI cleartime;
    public TextMeshProUGUI getItem;
    public TextMeshProUGUI BreakObject;

    public TextMeshProUGUI killScore;
    public TextMeshProUGUI ItemScore;
    public TextMeshProUGUI TimeScore;
    public TextMeshProUGUI BreakScore;

    private int finalScore;
    public TextMeshProUGUI FinalScore;
    public TextMeshProUGUI HighScore;
    void Start()
    {
        if(GameManager.Instance.clearCheck == true)
        {
            gameOver.text = "Game Clear";
        }

        killcount.text = $"kill : {GameManager.Instance.kill}";
        //cleartime.text = $"Clear Time : {GameManager.Instance.kill}";
        //getItem.text = $"GetItem : {GameManager.Instance.getItem}";
        BreakObject.text = $"Break Object : {GameManager.Instance.BreakObject}";

        killScore.text = $"Kill Score : {GameManager.Instance.kill*10}";
        //killcount.text = $"kill : {GameManager.Instance.kill}";
        //ItemScore.text = $"Item Score : {GameManager.Instance.kill * 10}";
        BreakScore.text = $"Break Score : {GameManager.Instance.ObjectScore}";

        finalScore = GameManager.Instance.kill * 10 + GameManager.Instance.itemScore + GameManager.Instance.ObjectScore;
        FinalScore.text = $"Final Score : {finalScore}";
        HighScore.text = $"High Score : {GameManager.Instance.highScore}";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Enter 키
        {
            if (GameManager.Instance.highScore <= finalScore)
            {
                GameManager.Instance.highScore = finalScore;
            } 
            GameManager.Instance.ResetGameData();
            SceneManager.LoadScene("Title"); // 타이틀 씬 이름 정확히 작성
        }
    }
}
