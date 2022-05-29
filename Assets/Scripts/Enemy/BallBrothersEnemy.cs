using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBrothersEnemy : Enemy
{
    void Start()
    {
        InitSet();
    }

    void Update()
    {

        if (GameDataManager.gameState == GameDataManager.GAMESTAGESTATE.GAMENOW)
        {
            EnemyMoveAction();
        }
        else
        {
            return;
        }
    }
}
