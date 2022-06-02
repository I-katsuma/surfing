using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NCMB;
using DG.Tweening;

public class GameDataManager : MonoBehaviour
{
    //public static GameDataManager Instance { get; private set; }

    [SerializeField]
    public static float gameSpeed = 1f;

    private float constGameSpeed = 1f;

    //private bool gameReady;
    //public static bool gameStart;

    private static float countDownTime;

    [SerializeField]
    ScoreManager scoreManager;
    public GameObject TitlePanel;
    public GameObject ShowTimeReadyPanel;
    public GameObject HeartPanel;
    public GameObject CountDownPanel;
    public GameObject ScorePanel;
    public GameObject GameOverPanel;
    public GameObject PartyText;

    public Text TextCountDown;

    [SerializeField]
    AudioSource audioSourceNormal;

    [SerializeField]
    AudioSource audioSourceParipi;

    [SerializeField]
    AudioClip normalBGM;

    [SerializeField]
    AudioClip paripiBGM;

    [SerializeField] Text GameStateText;

    public enum GAMESTAGESTATE
    {
        GAMELEADY,
        GAMENOW,
        GAMEPOUSE,
        GAMEOVER,
        GAMEWAIT
    }
    public static GAMESTAGESTATE gameState;


    private void Start()
    {
        gameState = GAMESTAGESTATE.GAMEWAIT;

        audioSourceNormal.clip = normalBGM;
        audioSourceParipi.clip = paripiBGM;
        audioSourceNormal.Play();

        gameSpeed = constGameSpeed;
        //gameReady = false;
        //gameStart = false;

        PanelDisplay(true);
        GameOverPanel.SetActive(false);
    }


    public void GameStartOnClick() // カウントダウン開始
    {
        gameState = GAMESTAGESTATE.GAMELEADY;

        // カウントダウン用
        PanelDisplay(false);
        countDownTime = 3.5f;
    }

    public void RetryButtonOnclick() // リトライ
    {
        if(gameState == GAMESTAGESTATE.GAMEOVER)
        {
            gameSpeed = constGameSpeed;
            scoreManager.ResetKoban();
            scoreManager.ResetScore();

            gameState = GAMESTAGESTATE.GAMEWAIT;
            DOTween.Clear();
            SceneManager.LoadScene("GameScene");
        }
    }

    public void ShowTimeReadyMethod(bool x)
    {
        if(x == true)
        {
            ShowTimeReadyPanel.SetActive(x); // まっくら照明演出
            //PartyText.SetActive(x);
            audioSourceNormal.Stop(); // 一旦BGMを止める
            StartCoroutine("ShowTimeReadyAction");
        }
        else
        {   
            ShowTimeReadyPanel.SetActive(x);
            PartyText.SetActive(x);
            return;
        }
    }

    private IEnumerator ShowTimeReadyAction()
    {
        Debug.Log("ショータイムのはじまり...");
        yield return new WaitForSeconds(0.75f);
        AudioSourceManager.instance.StandbySE_Clip();
        PartyText.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        AudioSourceManager.instance.AudienceSE_Clip();
        //yield return new WaitForSeconds(4f);
        //AudioSourceManager.instance.AudienceSE2_Clip();
    }

    public void ParipiMode(bool x)
    {
        if (x == true)
        {
            // PARIPI MODE!
            // audioSourceNormal.Stop();
            ShowTimeReadyMethod(false); // ShowTimeReady終了
            audioSourceParipi.Play();
            gameSpeed = 2.5f;
        }
        else if (x == false)
        {
            // 初期化
            audioSourceParipi.Stop();
            gameSpeed = constGameSpeed;
            audioSourceNormal.Play();
        }
    }

    private void PanelDisplay(bool x) // MainCanvasパネルのオンオフ
    {
        TitlePanel.SetActive(x);
        HeartPanel.SetActive(!x);
        CountDownPanel.SetActive(!x);
        ScorePanel.SetActive(!x);
        // ShowTimeReadyPanel.SetActive(!x);
    }

    private void FixedUpdate()
    {
        GameStateText.text = "GameStageState : " + gameState;  

        if(gameState == GAMESTAGESTATE.GAMELEADY)
        {
            // カウントダウン実施
            countDownTime -= Time.deltaTime;
            TextCountDown.text = countDownTime.ToString("0");
            if (countDownTime <= 0.0f)
            {
                countDownTime = 0.0f;

                gameState = GAMESTAGESTATE.GAMENOW; 
                Debug.Log("GAME START!");
                Debug.Log(gameState);                
                CountDownPanel.SetActive(false);
            }
        }

    }

    public void GameOver()
    {
        gameState = GAMESTAGESTATE.GAMEOVER;
        int score = scoreManager.resultScoreApper();

        ShowTimeReadyMethod(false);

        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);
        
        scoreManager.ResultScore();
        ScorePanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }
}
