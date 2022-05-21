using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DOTweenTest : MonoBehaviour
{
    Color colorNormal = new Color(255, 255, 255, 200);

    public float colorNum = 1f;
    public float alphaNum = 1f;
    public int loopNum = 2;

    [SerializeField] SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //spriteRenderer.GetComponent<SpriteRenderer>();        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PointerDown()
    {
        Debug.Log("PointerDown");
        StartCoroutine(ColorChange());
        //ColorChangeNormal();
    }

    private void ActionTestA()
    {
        // x:3fの地点まで, 0秒(位置移動テクニック)
        this.transform.DOMoveX(3f, 0);
    }

    private void ActionRelative()
    {
        // 相対的に移動
        this.transform.DOMoveX(3f, 0.5f).SetRelative(true);
    }

    IEnumerator ColorChange()
    //private void colorChange()
    {
        spriteRenderer.DOColor(Color.red, colorNum);
        spriteRenderer.DOFade(0f, alphaNum).SetEase(Ease.Flash, loopNum);
        yield return new WaitForSeconds(0.2f);
        /*
        spriteRenderer.DOColor(Color.white, colorNum);
        spriteRenderer.DOFade(0f, alphaNum).SetLoops(loopNum, LoopType.Yoyo);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.DOColor(Color.blue, colorNum);
        spriteRenderer.DOFade(0f, alphaNum).SetLoops(loopNum, LoopType.Yoyo);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.DOColor(Color.white, colorNum);
        spriteRenderer.DOFade(0f, alphaNum).SetLoops(loopNum, LoopType.Yoyo);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.DOColor(Color.green, colorNum);
        spriteRenderer.DOFade(0f, alphaNum).SetLoops(loopNum, LoopType.Yoyo);
        */

    }


    private void ColorChangeNormal()
    {
        spriteRenderer.DOColor(Color.red, colorNum);
        spriteRenderer.DOFade(0f, alphaNum).SetLoops(loopNum, LoopType.Yoyo);
        spriteRenderer.DOColor(Color.white, colorNum);
        spriteRenderer.DOFade(0f, alphaNum).SetLoops(loopNum, LoopType.Yoyo);
        spriteRenderer.DOColor(Color.blue, colorNum);
        spriteRenderer.DOFade(0f, alphaNum).SetLoops(loopNum, LoopType.Yoyo);
        spriteRenderer.DOColor(Color.white, colorNum);
        spriteRenderer.DOFade(0f, alphaNum).SetLoops(loopNum, LoopType.Yoyo);
        spriteRenderer.DOColor(Color.green, colorNum);
        spriteRenderer.DOFade(0f, alphaNum).SetLoops(loopNum, LoopType.Yoyo);
    }

}
