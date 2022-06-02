using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ThePartyIsReadyManager : MonoBehaviour
{
    [SerializeField] GameObject ThisGameObj;

    [SerializeField] Text ThisGameObjectText;

    Sequence sequence;

    public Ease EaseType;

    private void OnEnable()
    {
        if (ThisGameObj.name == "PartyText")
        {
            //Debug.Log("PartyText");
            //Invoke("PlayerTextMethod", 1.5f);
            StartCoroutine("PlayerTextMethod");
        }
    }
    private void OnDisable() // このオブジェクトが消えたら
    {
        if(DOTween.instance != null)
        {
            //Debug.Log("sequence.Kill()");
            //sequence.Kill();
        }
    }

    IEnumerator PlayerTextMethod()
    {
        Debug.Log("PlayerTextMethod");



        yield return new WaitForSeconds(1.5f);
        
        sequence = DOTween.Sequence()
        .Append(
            ThisGameObjectText.DOFade(0.0f, 1f).SetEase(this.EaseType).SetLoops(-1, LoopType.Yoyo)
            .Play()
            .SetLink(this.gameObject)
        );
        
    }
}
