using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum name
{
    Bard,
    Ballkun,
    Yadokari,
    GhostChan,
    BlueFish
}

public abstract class Enemy : MonoBehaviour
{
    public int myScore;

    public float speedAid = 1;

    [HideInInspector]
    public float Xmove = 0,
     Ymove = 0,
     Zmove = 0;
    public AudioClip damageSE;

    [SerializeField]
    public Collider2D coll2D;

    [HideInInspector]
    public Player player;

    [HideInInspector]
    public ScoreManager scoreManager;

    [HideInInspector]
    public AudioSourceManager audioSourceManager;

    public AudioSource audioSource;

    private void Start() {
        

    }

    public void InitSet()
    {
        Xmove = GameDataManager.gameSpeed * Time.deltaTime;
        //Ymove = this.transform.position.y;
        //Zmove = this.transform.position.z;
        player = GameObject.Find("Player").GetComponent<Player>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }


    public virtual void EnemyMoveAction()
    {
        
        this.transform.position -= new Vector3(
            Xmove,
            Ymove,
            Zmove
        );
        
        //this.transform.position.x -= Xmove;

        if (this.transform.position.x < -10)
        {
            DestroyAction();
        }
    }

    public virtual void DestroyAction()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(this.gameObject + " が " + other.gameObject + " とぶつかったよ");
        if (other.CompareTag("Player") == true)
        {
            if (player.state == Player.STATE.NORMAL)
            {
                this.coll2D.enabled = false;
                audioSource.PlayOneShot(damageSE);
                scoreManager.AddScore(myScore);
            }
            else if (player.state == Player.STATE.MUTEKI)
            {
                audioSource.PlayOneShot(damageSE);
                scoreManager.AddScore(myScore);
                DestroyAction();
            }
        }
    }
}
