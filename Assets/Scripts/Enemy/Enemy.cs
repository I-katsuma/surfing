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

    [HideInInspector] public float Ymove = 0, Zmove = 0;
    public AudioClip damageSE;

    [SerializeField] public Collider2D coll2D;

    [HideInInspector] public Player player;
    [HideInInspector] public ScoreManager scoreManager;
    [HideInInspector] public AudioSourceManager audioSourceManager;

    public AudioSource audioSource;

    void Start()
    {
    }


    public void EnemyMoveAction()
    {
        this.transform.position -= new Vector3(
            GameDataManager.gameSpeed * Time.deltaTime, Ymove, Zmove
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
                audioSource.PlayOneShot(damageSE);
                scoreManager.AddScore(myScore);
                DestroyAction();
            }
        }
    }

}
