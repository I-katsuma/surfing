using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float offset;
    
    ScoreManager scoreManager;
    Player player;
    
    public int myScore;

    [SerializeField] CircleCollider2D cc2D; // 自身のコライダー

    AudioSource audioSource;
    public AudioClip damageSE;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<Player>();
        scoreManager = GameObject.Find("GameDataManager").GetComponent<ScoreManager>();
        // 波打つ動き用
        offset = Random.Range(0, 2f * Mathf.PI);
    }

    void Update()
    {
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
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        // Debug.Log(this.gameObject + " が " + other.gameObject + " とぶつかったよ");
        if(other.CompareTag("Player") == true)
        {
            if(player.state == Player.STATE.NORMAL) {
                this.cc2D.enabled = false;
                audioSource.PlayOneShot(damageSE);
                scoreManager.AddScore(myScore);
            }
            else if(player.state == Player.STATE.MUTEKI)
            {
                scoreManager.AddScore(myScore);
                audioSource.PlayOneShot(damageSE);
                Destroy(this.gameObject);
            }
        }

    }
    
}
