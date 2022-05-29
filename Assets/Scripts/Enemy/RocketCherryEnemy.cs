using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCherryEnemy : Enemy
{
    float ziguzahu = 0;
    float moveX = 0.03f;
    float moveY = 0.05f;

    Vector3 position;   
    
    // Start is called before the first frame update
    void Start()
    {
        InitSet();  
        speedAid = 2;             
    }

    // Update is called once per frame
    void Update()
    {
        position = new Vector3(moveX, moveY, 0f);
        transform.Translate(position);

    if (GameDataManager.gameState == GameDataManager.GAMESTAGESTATE.GAMENOW)
        {
            //EnemyMoveAction();
            ZiguzaguMove();
        }
        else
        {
            return;
        }   
    }

    void ZiguzaguMove()
    {
        ziguzahu++;
        if(ziguzahu == 100)
        {
            ziguzahu = 0;
            moveX *= -1;
            moveY *= -1;
        }
    }
}
