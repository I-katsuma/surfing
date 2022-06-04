using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KobanGuageManager : MonoBehaviour
{
    [SerializeField] private GameObject KobanObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReSetKobanGuage()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            // いったん全削除
            Destroy(transform.GetChild(i).gameObject);   
        }
        /*
        // 現在の枚数分のKOBANケージを作成
        for (int i = 0; i < koban; i++)
        {
            Instantiate<GameObject>(KobanObj, transform);    
        }
        */
    }

    // 取得分だけ増加
    public void SetKobanGuage(int koban)
    {
        for (int i = 0; i < koban; i++)
        {
            Instantiate<GameObject>(KobanObj, transform);
        }
    }
}
