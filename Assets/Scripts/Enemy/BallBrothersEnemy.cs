using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBrothersEnemy : Enemy
{
    [SerializeField] SpriteRenderer IchiroSprite;
    [SerializeField] SpriteRenderer JiroSrite;
    [SerializeField] SpriteRenderer SaburoSprite;

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
