using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    ScoreManager scoreManager = null;
    public int ItemScore = 100;

    private void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        if (GameDataManager.gameState == GameDataManager.GAMESTAGESTATE.GAMENOW)
        {
            // 移動
            transform.position -= new Vector3(
                GameDataManager.gameSpeed * Time.deltaTime, 0, 0);
            if (transform.position.x < -10)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(this.gameObject + " が " + other.gameObject + " とぶつかったよ");
        if (this.gameObject.tag == "ScoreItem")
        {
            if (other.CompareTag("Player") == true)
            {
                scoreManager.AddScore(ItemScore);
                Destroy(this.gameObject);
            }
        }
        else if(this.gameObject.tag == "PowerItem")
        {
            if(other.CompareTag("Player") == true)
            {
                //Debug.Log("アイテム判定");
                scoreManager.AddKoban();
                scoreManager.AddScore(500);
                Destroy(this.gameObject);
            }
        }
        else if(this.gameObject.tag == "RareItem")
        {
            if (other.CompareTag("Player") == true)
            {
                scoreManager.AddScore(ItemScore);
                Destroy(this.gameObject);
            }
        }
    }

}
