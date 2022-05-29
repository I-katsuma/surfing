using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LuckFrog : MonoBehaviour
{

    public int MyScore = 1000;

    int speedAid = 1;

    public AudioClip damageSE;
    public Collider2D col2D;
    Player player;
    ScoreManager scoreManager;
    public AudioSource audioSource;

    public GameObject spriteObj;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        if (GameDataManager.gameState == GameDataManager.GAMESTAGESTATE.GAMENOW)
        {
            EnemyMoveAction();
        }
    }

    public virtual void EnemyMoveAction()
    {
        this.transform.position -= new Vector3(
            GameDataManager.gameSpeed * Time.deltaTime * speedAid,
            this.transform.position.y,
            this.transform.position.z
        );
        if (this.transform.position.x < -10f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(this.gameObject + " が " + other.gameObject + " とぶつかったよ");
        if (other.CompareTag("Player") == true)
        {
            bool flag = false;

            if (flag == false)
            {
                this.col2D.enabled = false;
                spriteObj.SetActive(false);
                EnemyGenerator.StageChangeButton();
                audioSource.PlayOneShot(damageSE);
                scoreManager.AddScore(MyScore);
            }
            else
            {

            }
        }
    }
}
