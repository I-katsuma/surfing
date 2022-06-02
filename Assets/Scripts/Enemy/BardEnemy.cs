using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class BardEnemy : Enemy
{
    private float offset;

    [SerializeField] GameObject spriteObj;

    // [SerializeField] private float _durationSecounds = 0.25f;

    //private float _initY = 0;
    //private float _endY = 0;

    // [SerializeField] private float _moveY = 0.4f;

    private TweenerCore<Vector3, Vector3, VectorOptions> _tweener;

    public Ease easeType;

    void Start()
    {
        InitSet();
        //InitObjSet();
        // 波立動き用↓
        offset = Random.Range(0, 2f * Mathf.PI);
        /*
        if (spriteObj != null)
        {
            _tweener = spriteObj.transform
                .DOMoveY(_endY, duration: _durationSecounds)
                .SetEase(this.easeType)
                .SetLoops(-1, LoopType.Yoyo)
                .Play()
                .SetLink(this.gameObject);
        }
        */
    }

    /*
    void InitObjSet()
    {
        _initY = spriteObj.transform.position.y;
        _endY = _initY - _moveY;
    }
    */

    public override void EnemyMoveAction()
    {
        //base.EnemyMoveAction();
        this.transform.position -= new Vector3(
            Xmove = GameDataManager.gameSpeed * Time.deltaTime * speedAid,
            Ymove,
            Zmove
        );

        if (this.transform.position.x < -10)
        {
            DestroyAction();
        }
    }

    void FixedUpdate()
    {
        // 縦揺れ用↓
        Ymove = Mathf.Cos(Time.frameCount * 0.05f + offset) * 0.01f;
        
        if(GameDataManager.gameState == GameDataManager.GAMESTAGESTATE.GAMENOW)
        {
            EnemyMoveAction();
        }

        if(GameDataManager.gameState == GameDataManager.GAMESTAGESTATE.GAMEOVER)
        {
            //_tweener.Kill();
        }

    }

    public void TweenKill()
    {
        _tweener.Kill();
    }


    public override void DestroyAction()
    {
        if(spriteObj != null)
        {
            //Debug.Log("DOTWEENをKIll");
            //_tweener.Kill();
        }

        base.DestroyAction();
    }
}
