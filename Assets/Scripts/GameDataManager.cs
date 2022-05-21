using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour
{
    // シングルトン(シーンをまたいでも破壊されない)
    public static GameDataManager Instance { get; private set; }

    [SerializeField]
    public static float gameSpeed = 1f;
    private float constGameSpeed = 1f;

    private bool gameReady;
    public static bool gameStart;

    private static float countDownTime;


    public GameObject TitlePanel;
    public GameObject HeartPanel;
    public GameObject CountDownPanel;
    public GameObject ScorePanel;
    public GameObject GameOverPanel;

    public Text TextCountDown;

    // [SerializeField] ScoreManager scoreManager;

    [SerializeField] AudioSource audioSourceNormal;
    [SerializeField] AudioSource audioSourceParipi;
    [SerializeField] AudioClip normalBGM;
    [SerializeField] AudioClip paripiBGM;

    private void Start()
    {
        Debug.Log("GameDataManager Start()");
        audioSourceNormal.clip = normalBGM;
        audioSourceParipi.clip = paripiBGM;
        audioSourceNormal.Play();

        gameSpeed = constGameSpeed;
        gameReady = false;
        gameStart = false;

        PanelDisplay(true);
        GameOverPanel.SetActive(false);

    }

    public void GameStartOnClick()
    {
        // カウントダウン用
        PanelDisplay(false);
        gameReady = true;
        countDownTime = 3.5f;
    }

    public void RetryButtonOnclick() // リトライ
    {
        if (GameOverPanel.activeSelf == true)
        {
            gameSpeed = constGameSpeed;
            ScoreManager.instance.playerScore = 0;
            ScoreManager.instance.KOBAN_score = 0;
            Player.isGameOver = false;
            //PanelDisplay(true);
            SceneManager.LoadScene("GameScene");
        }
    }

    public void ParipiMode(bool x)
    {
        if(x == true){
            // PARIPI MODE!
            audioSourceNormal.Stop();
            audioSourceParipi.Play();
            gameSpeed = 2.5f;
        }
        else if(x == false)
        {
            // 初期化
            audioSourceParipi.Stop();
            gameSpeed = constGameSpeed;
            audioSourceNormal.Play();
        }
    }

    private void PanelDisplay(bool x)
    {
        Debug.Log("PanelDisplay()実行");
        TitlePanel.SetActive(x);
        HeartPanel.SetActive(!x);
        CountDownPanel.SetActive(!x);
        ScorePanel.SetActive(!x);
    }

    private void Update()
    {
        if (gameReady)
        {
            // カウントダウン
            countDownTime -= Time.deltaTime;
            TextCountDown.text = countDownTime.ToString("0");
            if (countDownTime <= 0.0f)
            {
                countDownTime = 0.0f;

                gameStart = true;
                Debug.Log("GAME START!");

                CountDownPanel.SetActive(false);
                gameReady = false;
            }
        }

        // ゲームオーバー画面表示
        if (Player.isGameOver)
        {
            ScoreManager.instance.ResultScore();
            ScorePanel.SetActive(false);
            GameOverPanel.SetActive(true);
        }

    }

}
