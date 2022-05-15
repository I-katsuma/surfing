using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int playerScore;
    public static int KOBAN_score;

    public Text scoreText;
    public Text scoreResultText;
    public Text getKOBANtext;

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
            KOBAN_score += 1;   
        }
        else
        {
            KOBAN_score = 5;
        }
        getKOBANtext.text = string.Format("x" + KOBAN_score);
    }
}
