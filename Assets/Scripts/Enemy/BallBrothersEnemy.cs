using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBrothersEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameDataManager.gameStart)
        {
            EnemyMoveAction();
        }
        else
        {
            return;
        }
    }
}
