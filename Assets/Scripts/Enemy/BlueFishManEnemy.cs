using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlueFishManEnemy : Enemy
{
    [SerializeField]
    GameObject spriteOBJ,shadowOBJ;
    //Sequence sequence;

    private void Start()
    {
        InitSet();
        sequence = DOTween.Sequence();
        sequence.Append(
        spriteOBJ.transform.DOJump(
            new Vector3(-12f, this.transform.position.y, Zmove), // 移動終了地点
            1f, // ジャンプの高さ
            4, // ジャンプの総数
            10f // 演出時間
        ))
        .Join(
        shadowOBJ.transform.DOMoveX(
            -12f, //移動後の座標
            10f // 時間
        ))
        .Play()
        .SetLink(this.gameObject);
    }

    private void Update() {
        if(spriteOBJ.transform.position.x <= -10f)
        {
            DestroyAction();
        }
    }


    public override void DestroyAction()
    {
        base.DestroyAction();
    }

}
