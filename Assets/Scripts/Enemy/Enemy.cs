using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum name {
    Bard,
    Ballkun,
    Yadokari,
    GhostChan,
    BlueFish
}
public abstract class Enemy : MonoBehaviour
{
    public int myScore;

    public AudioClip damageSE;

    [SerializeField] CircleCollider2D cc2D;


    Player player;
    ScoreManager scoreManager;
    AudioSource audioSource;




    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
