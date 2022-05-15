using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    [SerializeField]
    private GameObject LifeGauge;

    // ライフゲージ全削除&HP分作成
    public void SetLifeGauge(int life)
    {
        // 体力をいったん削除
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);   
        }
        // 現在の体力数分のライフゲージを作成
        for (int i = 0; i < life; i++)
        {
            Instantiate<GameObject>(LifeGauge, transform);   
        }
    }
}
