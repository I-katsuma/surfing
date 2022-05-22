using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class Player : MonoBehaviour
{

    GameDataManager gameDataManager;

    ScoreManager scoreManager;

    readonly float flashInterval = 0.08f; // 点滅間隔 0.08
    readonly int loopCount = 20; // 点滅させるときのループカウント 20
    [SerializeField] int paripiTimeCount = 36; // パリピタイムのカウント 
    [SerializeField] private SpriteRenderer spCat; // 点滅させるためのSpriteRenderer
    [SerializeField] private SpriteRenderer spDog;

    // コライダーをOnOffするため
    [SerializeField] private CircleCollider2D cc2d;

    public GameObject SurfDog;
    public GameObject ParipiDog;

    public static bool isGameOver;

    [HideInInspector]
    public enum STATE
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

    [SerializeField] Image ParipiImage;


    private Vector2 playerPos;
    private readonly float playerPosXClamp = 2.2f;
    private readonly float playerPoxYClamp = 1.6f;

    public float paripiInterval = 1f;

    public float DurationSeconds = 1f;

    public Ease EaseType;

    private void Start()
    {
        //ParipiPanel.SetActive(true);

        ParipiImage.color = new Color32(255, 255, 255, 0);
        gameDataManager = GameObject.Find("GameDataManager").GetComponent<GameDataManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        /*
        if (ParipiPanel && ParipiImage)
        {
            ParipiImage.color = new Color32(255, 255, 255, 200);
            ParipiPanel.SetActive(false);
        }
        */

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

        if (state == STATE.PARIPI) // パリピモードに入ったら
        {
            //ScoreManager.KOBAN_score = 0; // 小判スコアリセット
            StartCoroutine(_paripi());
            return;
        }

        if (state == STATE.MUTEKI) 
        {
            //TODO:無敵中は小判が増えないようにしたい
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
        if (state == STATE.MUTEKI) // 無敵モード中の効果音設定
        {
            if (other.gameObject.tag == "PowerItem") // 小判
            {
                AudioSourceManager.instance.KobanSEClip();
            }
            if (other.gameObject.tag == "ScoreItem") // 果物
            {
                AudioSourceManager.instance.ItemSEClip();
            }
            if (other.gameObject.tag == "Enemy") // 敵
            {
                //TODO: 後でふっとばすかんじの効果音に変更予定
                AudioSourceManager.instance.DamageSEClipA();
            }
        }
        else if (state == STATE.NORMAL)
        {
            if (other.gameObject.tag == "PowerItem")
            {
                AudioSourceManager.instance.KobanSEClip();
                if (scoreManager.KOBAN_score < 5)
                {
                    GameDataManager.gameSpeed += 0.2f;
                }
                else
                {
                    // KOBANを5つそろえてパリピモード!
                    state = STATE.PARIPI;
                }
            }
            else if (other.gameObject.tag == "ScoreItem")
            {
                AudioSourceManager.instance.ItemSEClip();
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
        scoreManager.ResetKoban();
        state = STATE.MUTEKI;
        ChangePlayerSetMode(true);

        ParipiImage.color = new Color32(255, 255, 255, 200);

        /*
        Sequence seq = DOTween.Sequence();
        seq.Append(ParipiImage.DOFade(0.0f, DurationSeconds).SetEase(this.EaseType));
        seq.SetLoops(-1, LoopType.Yoyo);  
        */

        for (int i = 0; i < paripiTimeCount; i++)
        {
            Debug.Log(i);
            /* 
            ParipiPanel.SetActive(true);
            //yield return new WaitForSeconds(alphaNum);
            ParipiPanel.SetActive(false);
            yield return new WaitForSeconds(alphaNum);
            */
            ParipiImage.DOFade(0.0f, DurationSeconds).SetEase(this.EaseType).SetLoops(1, LoopType.Yoyo);
            ParipiImage.color = Color.HSVToRGB(Time.time % 1f, 1f, 1f);
            yield return new WaitForSeconds(paripiInterval);
        }
        
        state = STATE.NORMAL;
        
        ParipiImage.color = new Color32(255, 255, 255, 0);
        ChangePlayerSetMode(false);

    }

    private void ChangePlayerSetMode(bool x)
    {
        // パリピモードオンオフセット
        gameDataManager.ParipiMode(x);
        SurfDog.SetActive(!x);
        ParipiDog.SetActive(x);
    }

}
