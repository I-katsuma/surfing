using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YokoScroll : MonoBehaviour
{
    //private float speed;

    void Update()
    {
        transform.position -= new Vector3((Time.deltaTime * GameDataManager.gameSpeed)/3 , 0, 0);

        if(transform.position.x <= -18f)
        {
            transform.position = new Vector2(36f, 6.25f);
        }        
    }
}
