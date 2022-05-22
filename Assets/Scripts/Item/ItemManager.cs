using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    ScoreManager scoreManager = null;

    private void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        if (GameDataManager.gameStart)
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
                //Debug.Log("小判判定");
                //ScoreManager.instance.AddScore(100);
                scoreManager.AddScore(100);
                Destroy(this.gameObject);
            }
        }
        else if(this.gameObject.tag == "PowerItem")
        {
            if(other.CompareTag("Player") == true)
            {
                Debug.Log("アイテム判定");
                //ScoreManager.instance.AddScore(500);
                //ScoreManager.instance.AddKoban();
                scoreManager.AddKoban();
                scoreManager.AddScore(500);
                Destroy(this.gameObject);
            }
        }
    }

}
