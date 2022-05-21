using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class Player : MonoBehaviour
{
    // private float playerSpeed = 1f;

    // private readonly float superSpeed = 3f;
    GameDataManager gameDataManager;
    AudioSourceManager audioSourceManager;

    readonly float flashInterval = 0.08f; // 点滅間隔 0.08
    readonly int loopCount = 20; // 点滅させるときのループカウント 20
    [SerializeField] int paripiTimeCount = 35; // パリピタイムのカウント 
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
    public static GameObject ParipiPanel;
    [SerializeField] Image ParipiImage;

    AudioSource audioSource;
    [SerializeField] AudioClip itemSE;
    [SerializeField] AudioClip kobanSE;
    [SerializeField] AudioClip damageSE;

    private Vector2 playerPos;
    private readonly float playerPosXClamp = 2.2f;
    private readonly float playerPoxYClamp = 1.6f;

    public float paripiInterval = 1f;

    public float DurationSeconds = 0.1f;

    public Ease EaseType;

    private void Start()
    {
        // ParipiPanel.SetActive(true);
        Debug.Log("Player Start()");

        gameDataManager = GameObject.Find("GameDataManager").GetComponent<GameDataManager>();

        if(ParipiPanel)
        {
            // リセット用の仕掛け
            ParipiPanel.SetActive(true);
        }
        else
        {
            ParipiPanel = GameObject.Find("ParipiPanel").gameObject;
            ParipiImage = GameObject.Find("ParipiPanel").GetComponent<Image>();
        }

        audioSource = GetComponent<AudioSource>();

        if (ParipiPanel && ParipiImage)
        {
            ParipiImage.color = new Color32(255, 255, 255, 200);
            ParipiPanel.SetActive(false);
        }

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
            //TODO 無敵中は小判が増えないようにしたい
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
                audioSource.PlayOneShot(kobanSE);
                if (ScoreManager.instance.KOBAN_score < 5)
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
        ScoreManager.instance.KOBAN_score = 0;
        state = STATE.MUTEKI;
        ChangePlayerSetMode(true);

        Sequence seq = DOTween.Sequence();
        seq.Append(ParipiImage.DOFade(0.0f, DurationSeconds).SetEase(this.EaseType));
        seq.SetLoops(-1, LoopType.Yoyo);  
        
        //Sequence seq = DOTween.Sequence();
        for (int i = 0; i < paripiTimeCount; i++)
        {
            Debug.Log(i);
            /* 
            ParipiPanel.SetActive(true);
            //yield return new WaitForSeconds(alphaNum);
            ParipiPanel.SetActive(false);
            yield return new WaitForSeconds(alphaNum);
            */

            //ParipiImage.color = Color.HSVToRGB(Time.deltaTime % 1f, 1f, 1f);


            //ParipiImage.color = Color.Lerp(Color.red, Color.blue, Mathf.PingPong(Time.time, 1));
            /*
            seq.Append(ParipiImage.DOColor(Color.red, 2f)).AppendInterval(0.25f)
            .Append(ParipiImage.DOColor(Color.blue, 2f)).AppendInterval(0.25f)
            .Append(ParipiImage.DOColor(Color.green, 2f)).AppendInterval(0.25f)
            .Append(ParipiImage.DOColor(Color.white, 2f)).AppendInterval(0.25f);
            */

            yield return new WaitForSeconds(paripiInterval);
        }
        
        state = STATE.NORMAL;
        ChangePlayerSetMode(false);

    }

    private void ChangePlayerSetMode(bool x)
    {
        // パリピモードオンオフセット
        gameDataManager.ParipiMode(x);
        SurfDog.SetActive(!x);
        ParipiDog.SetActive(x);
        ParipiPanel.SetActive(x);
    }

}
