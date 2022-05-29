using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GhostChanEnemy : Enemy
{
    private Tweener _shakeTweener;
    private Vector3 _initPosition;

    // DoTween用
    public float DurationSeconds = 1f;
    public Ease EaseType;

    Tweener tweener;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        InitSet();
        
        tweener = spriteRenderer.DOFade(0.0f, this.DurationSeconds).SetEase(this.EaseType).SetLoops(-1, LoopType.Yoyo);
        tweener.Play()
        .SetLink(this.gameObject);

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
        // tweener.Kill();
        base.DestroyAction();
    }
}
