using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Vector2 targeting;


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
                /*
                if(other.gameObject.name == "BlueBardSan")
                {
                    BardEnemy bard = other.gameObject.GetComponent<BardEnemy>();
                    bard.TweenKill();
                }
                */
                this.coll2D.enabled = false;
                audioSource.PlayOneShot(damageSE);
                scoreManager.AddScore(myScore);
                StartCoroutine("DamageAction");
                //DestroyAction();
            }
        }
    }

    IEnumerator DamageAction()
    {
        Debug.Log("DamageAction");
        targeting = (player.transform.position - this.gameObject.transform.position).normalized;
        int i = 0;
        while(i < 10)
        {    
            yield return new WaitForSeconds(0.02f);
            this.gameObject.transform.Translate(targeting.x * -1f, targeting.y * 1.5f, 0);
            i++;
        }
        DestroyAction();
    }
}
