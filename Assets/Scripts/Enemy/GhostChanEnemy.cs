using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GhostChanEnemy : Enemy
{
    private float offset;
    
    private Tweener _shakeTweener;
    private Vector3 _initPosition;

    public float DurationSeconds = 1f;
    public Ease EaseType;

    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        offset = Random.Range(0, 2f * Mathf.PI);
        spriteRenderer.DOFade(0.0f, this.DurationSeconds).SetEase(this.EaseType).SetLoops(-1, LoopType.Yoyo);        
        
        player = GameObject.Find("Player").GetComponent<Player>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        if (GameDataManager.gameStart)
        {
            //Move();
            EnemyMoveAction();

        }
        else
        {
            return;
        }
    }

    public override void DestroyAction()
    {
        spriteRenderer.DOKill();
    }
    
}
