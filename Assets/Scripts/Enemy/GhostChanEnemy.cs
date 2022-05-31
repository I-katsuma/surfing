using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GhostChanEnemy : Enemy
{
    // DoTween用
    public float DurationSeconds = 1f;
    public Ease EaseType;

    Tweener tweener;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rd2d;

    [SerializeField] private float homingNum = 0.2f;

    void Start()
    {
        InitSet();
        tweener = spriteRenderer.DOFade(0.0f, this.DurationSeconds).SetEase(this.EaseType).SetLoops(-1, LoopType.Yoyo);
        tweener.Play()
        .SetLink(this.gameObject);

    }

    public override void EnemyMoveAction()
    {
        Vector3 diff = player.transform.position - this.transform.position;
        rd2d.velocity = new Vector3(
            GameDataManager.gameSpeed * -speedAid,
        diff.y * GameDataManager.gameSpeed * homingNum);


        if (this.transform.position.x < -10)
        {
            DestroyAction();
        }
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

    public override void DestroyAction()
    {
        base.DestroyAction();
    }
}
