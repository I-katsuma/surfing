using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    public static AudioSourceManager instance = null;

    [SerializeField] public AudioSource audioSource = null;

    [SerializeField] private AudioClip ItemSE;
    [SerializeField] private AudioClip RareItemSE;
    [SerializeField] private AudioClip KobanSE;

    [SerializeField] private AudioClip standbySE;
    [SerializeField] private AudioClip audienceSE;
    [SerializeField] private AudioClip audienceSE2;

    private void Awake()
    {

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        } 
    }

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    public void ItemSEClip()
    {
        if(audioSource != null)
        {
            // 指定した音を一回鳴らす
            // 既になっている音に重複する
            audioSource.PlayOneShot(ItemSE);
        }
        else
        {
            Debug.Log("オーディオソースがありません");
        }
    }

     public void KobanSEClip()
    {
        if(audioSource != null)
        {
            // 指定した音を一回鳴らす
            // 既になっている音に重複する
            audioSource.PlayOneShot(KobanSE);
        }
        else
        {
            Debug.Log("オーディオソースがありません");
        }
    }

    public void RareItemSE_Clicp()
    {
        if(audioSource != null)
        {
            // 指定した音を一回鳴らす
            // 既になっている音に重複する
            audioSource.PlayOneShot(RareItemSE);
        }
        else
        {
            Debug.Log("オーディオソースがありません");
        }
    }

    public void StandbySE_Clip()
    {
        if(audioSource != null)
        {
            audioSource.PlayOneShot(standbySE);
        }
    }

    public void AudienceSE_Clip()
    {
        if(audioSource != null)
        {
            audioSource.PlayOneShot(audienceSE);
        }
    }

    public void AudienceSE2_Clip()
    {
        if(audioSource != null)
        {
            audioSource.PlayOneShot(audienceSE2);
        }
    }

}
