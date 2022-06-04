using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ThePartyIsReadyManager : MonoBehaviour
{
    [SerializeField]
    GameObject ThisGameObj;

    [SerializeField]
    Text ThisGameObjectText;

    public Ease EaseType;

    private void OnEnable()
    {
        //Debug.Log("PartyText");
        StartCoroutine("PlayerTextMethod");
    }


    private void OnDisable() // このオブジェクトが消えたら
    {
        if (DOTween.instance != null)
        {

        }
    }

    IEnumerator PlayerTextMethod()
    {
        Debug.Log("PlayerTextMethod");

        yield return new WaitForSeconds(1f);
        ThisGameObjectText.DOFade(0.0f, 1f)
        .SetEase(this.EaseType)
        .SetLoops(-1, LoopType.Yoyo);

    }
}
