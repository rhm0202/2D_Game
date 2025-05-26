using TMPro;
using UnityEngine;

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

    }
}
