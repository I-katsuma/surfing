using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float offset;
    
    Player player;

    ScoreManager scoreManager;

    public int myScore;

    [SerializeField] CircleCollider2D cc2D; // 自身のコライダー

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        // 波打つ動き用
        offset = Random.Range(0, 2f * Mathf.PI);
    }

    void Update()
    {
        /*
        if (GameDataManager.gameStart)
        {
            // 移動
            transform.position -= new Vector3(
                GameDataManager.gameSpeed * Time.deltaTime, Mathf.Cos(Time.frameCount * 0.05f + offset) * 0.01f, 0);
            if (transform.position.x < -10)
            {
                Destroy(this.gameObject);
            }
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        // Debug.Log(this.gameObject + " が " + other.gameObject + " とぶつかったよ");
        if(other.CompareTag("Player") == true)
        {
            if(player.state == Player.STATE.NORMAL) {
                this.cc2D.enabled = false;
                AudioSourceManager.instance.DamageSEClipA();
                //ScoreManager.instance.AddScore(300);
                scoreManager.AddScore(300);
            }
            else if(player.state == Player.STATE.MUTEKI)
            {
                AudioSourceManager.instance.DamageSEClipA();
                //ScoreManager.instance.AddScore(300);
                scoreManager.AddScore(300);
                Destroy(this.gameObject);
            }
        }

    }
    
}
