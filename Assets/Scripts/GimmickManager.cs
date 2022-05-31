using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GimmickManager : MonoBehaviour
{
    [SerializeField]
    GameObject ThisGameObj;

    private SpriteRenderer ThisGameObjectSprite;

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        

        if (ThisGameObj.name == "PartyText")
        {
            Debug.Log("PartyText");
            PlayerTextMethod();
        }
        if (ThisGameObj.name == "SPEEDUP")
        {
            Debug.Log("SPEEDUP");
            ThisGameObjectSprite = ThisGameObj.GetComponent<SpriteRenderer>();
            SpeedUpMethod();
        }
    }

    void PlayerTextMethod()
    {
        Debug.Log("PlayerTextMethod");
    }

    void SpeedUpMethod()
    {
        Debug.Log("SppedUpMethod");
        StartCoroutine("FlashMethod");
        
    }

    IEnumerator FlashMethod()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("FlashMethodのコルーチン");
            yield return new WaitForSeconds(0.4f);
            ThisGameObjectSprite.color = new Color32(255, 255, 255, 0);
            yield return new WaitForSeconds(0.4f);
            ThisGameObjectSprite.color = new Color32(255, 255, 255, 255);
        }
        ThisGameObj.SetActive(false);
    }
}
