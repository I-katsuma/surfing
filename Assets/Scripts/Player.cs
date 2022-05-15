using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class Player : MonoBehaviour
{
    // private float playerSpeed = 1f;

    // private readonly float superSpeed = 3f;
    GameDataManager gameDataManager;

    readonly float flashInterval = 0.08f; // 点滅間隔 0.08
    readonly float paripiInterval = 1f;
    readonly int loopCount = 20; // 点滅させるときのループカウント 20
    readonly int paripiTimeCount = 20; // パリピタイムのカウント 30
    [SerializeField] private SpriteRenderer spCat; // 点滅させるためのSpriteRenderer
    [SerializeField] private SpriteRenderer spDog;

    // コライダーをOnOffするため
    [SerializeField] private CircleCollider2D cc2d;

    public GameObject SurfDog;
    public GameObject ParipiDog;

    public static bool isGameOver;

    [HideInInspector] public enum STATE
    {
        NORMAL,
        DAMAGED,
        MUTEKI,
        PARIPI,
        GAMEOVER
    }
    public STATE state;

    [SerializeField] private int playerHp;
    [SerializeField] HeartManager heartManager;
    [SerializeField] GameObject ParipiPanel;

    AudioSource audioSource;
    [SerializeField]AudioClip itemSE;
    [SerializeField]AudioClip kobanSE;

    private Vector2 playerPos;
    private readonly float playerPosXClamp = 2.2f;
    private readonly float playerPoxYClamp = 1.6f;

    private void Start()
    {
    
        gameDataManager = GameObject.Find("GameDataManager").GetComponent<GameDataManager>();
        audioSource = GetComponent<AudioSource>();

        ParipiPanel.SetActive(false);
        SurfDog.SetActive(true);        
        
        isGameOver = false;
        state = STATE.NORMAL;

        playerHp = 3;
        heartManager.SetLifeGauge(playerHp);
    }

    private void Update()
    {
        if (state == STATE.GAMEOVER) // ゲームオーバーになったら
        {
            isGameOver = true;
            GameDataManager.gameSpeed = 0;
            return;
        }

        if(state == STATE.PARIPI) // パリピモードに入ったら
        {
            StartCoroutine(_paripi());
            return;
        }

        if(state == STATE.MUTEKI)
        {
            ScoreManager.KOBAN_score = 0;
        }

        // 動くべき方向に変数に格納
        Vector3 moveDir = ((transform.up * JoyStickMove.joyStickPosY) +
        (transform.right * JoyStickMove.joyStickPosX)).normalized;

        // ポジション更新
        transform.position += moveDir * GameDataManager.gameSpeed * Time.deltaTime;

        this.MovingRestrictions();

    }

    private void MovingRestrictions()
    {
        this.playerPos = transform.position;

        this.playerPos.x = Mathf.Clamp(this.playerPos.x,
        -this.playerPosXClamp, this.playerPosXClamp);

        this.playerPos.y = Mathf.Clamp(this.playerPos.y,
        -this.playerPoxYClamp, this.playerPoxYClamp);

        transform.position = new Vector2(this.playerPos.x, this.playerPos.y);
    }

    // ダメージ処理メソッド（全削除＆HP分作成）
    public void PlayerDamage(int damage)
    {
        if (state == STATE.DAMAGED)
        {
            playerHp -= damage;

            playerHp = Mathf.Max(0, playerHp);

            if (playerHp >= 0)
            {
                heartManager.SetLifeGauge(playerHp);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (state == STATE.MUTEKI)
        {
            if(other.gameObject.tag == "PowerItem")
            {
                audioSource.PlayOneShot(kobanSE);
            }
            if(other.gameObject.tag == "ScoreItem")
            {
                audioSource.PlayOneShot(itemSE);
            }
            if(other.gameObject.tag == "Enemy")
            {

            }
        }
        else if(state == STATE.NORMAL)
        {
            if (other.gameObject.tag == "PowerItem")
            {
                audioSource.PlayOneShot(kobanSE);
                if(ScoreManager.KOBAN_score < 5)
                {
                    GameDataManager.gameSpeed += 0.2f;
                }
                else
                {
                    // KOBANを5つそろえてパリピモード!
                    state = STATE.PARIPI;
                }
            }
            else if(other.gameObject.tag == "ScoreItem")
            {
                audioSource.PlayOneShot(itemSE);
            }
            else if (other.gameObject.tag == "Enemy")
            {
                if (playerHp == 1)
                {
                    state = STATE.GAMEOVER;
                    StartCoroutine(_hit());
                }
                else if (playerHp > 0)
                {
                    
                    state = STATE.DAMAGED;
                    PlayerDamage(1);
                    StartCoroutine(_hit());
                }
            }
        }
    }

    IEnumerator _hit()
    {
        // あたり判定オフ
        // cc2d.enabled = false;

        for (int i = 0; i < loopCount; i++)
        {
            // flashIntervalを待ってから
            yield return new WaitForSeconds(flashInterval);

            // SpriteRendererをオフ
            spCat.enabled = false;
            spDog.enabled = false;

            // flashIntervalを待ってから
            yield return new WaitForSeconds(flashInterval);
            spCat.enabled = true;
            spDog.enabled = true;

            if (i > 1)
            {
                // stateをMUTEKIにする（点滅しながら動けるようになる）
                state = STATE.MUTEKI;
            }
        }

        // ループ抜けたらstateをnomalに
        state = STATE.NORMAL;
        // あたり判定をオン
        // cc2d.enabled = true;
    }

    IEnumerator _paripi()
    {
        Debug.Log("ぱりぴモード！");
        gameDataManager.ParipiMode(true);
        SurfDog.SetActive(false);
        ParipiDog.SetActive(true);
        
 
        for (int i = 0; i < paripiTimeCount; i++)
        {
            Debug.Log(i); 
            ParipiPanel.SetActive(true);
            yield return new WaitForSeconds(paripiInterval);
            ParipiPanel.SetActive(false);
             yield return new WaitForSeconds(paripiInterval);
            if(i > 0)
            {
                state = STATE.MUTEKI;
            }
        }
        state = STATE.NORMAL;
        ParipiPanel.SetActive(false);
        gameDataManager.ParipiMode(false);
        ParipiDog.SetActive(false);
        SurfDog.SetActive(true);
    }
}
