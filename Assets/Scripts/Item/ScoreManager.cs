using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //public static ScoreManager instance = null;

    public int playerScore = 0;
    public int KOBAN_score = 0;

    public Text scoreText;
    public Text scoreResultText;
    public Text getKOBANtext;

    [SerializeField] Player player;
    [SerializeField] GameDataManager gameDataManager;              
    [SerializeField] KobanGuageManager kobanGuageManager;
    /*
    private void Awake() {

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        } 
    }
    */

    // Start is called before the first frame update
    void Start()
    {

        playerScore = 0;
        KOBAN_score = 0;

        scoreText.text = playerScore.ToString("D5");
        getKOBANtext.text = string.Format("x" + KOBAN_score);
    }

    public void ResultScore()
    {
        scoreResultText.text = string.Format("SCORE : " + playerScore);
    }

    public int resultScoreApper()
    {
        int resultScore = playerScore;
        return resultScore;
    }

    // スコア加算
    public void AddScore(int score)
    {
        playerScore += score;
        scoreText.text = playerScore.ToString("D5");
    }

    // KOBAN加算
    public void AddKoban()
    {
        if(KOBAN_score < 5)
        {
            if(KOBAN_score == 3)
            {
                // プレイヤーをスタンバイモードへ
                player.state = Player.STATE.SHOWTIMEREADY;
                gameDataManager.ShowTimeReadyMethod(true);
            }
            KOBAN_score++;
            kobanGuageManager.SetKobanGuage(1);
        }
        else
        {
            KOBAN_score = 5;
        }
        getKOBANtext.text = string.Format("x" + KOBAN_score);
    }

    public void ResetKoban()
    {
        KOBAN_score = 0;
        kobanGuageManager.ReSetKobanGuage();
        getKOBANtext.text = string.Format("x" + KOBAN_score);
    }

    public void ResetScore()
    {
        playerScore = 0;
        scoreText.text = playerScore.ToString("D5");
    }
}
