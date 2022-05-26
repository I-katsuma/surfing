using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject BardPrefab;
    public GameObject GhostChanPrefab;


    void Start()
    {
       InvokeRepeating("BardSpawn", 1.5f, 8f / GameDataManager.gameSpeed);
       // InvokeRepeating("GhostChanSpawn", 1.5f, 8f / GameDataManager.gameSpeed) ;
    }

    void BardSpawn()
    {
        Vector3 spawnPosition = new Vector3(
            transform.position.x,
            Random.Range(-3f, 1f),
            transform.position.z
        );

        if (GameDataManager.gameStart == true)
        {
            Debug.Log("Bard Spawn!");
            Instantiate(BardPrefab, spawnPosition, transform.rotation);
        }
    }
    void GhostChanSpawn()
    {
        Vector3 spawnPosition = new Vector3(
            transform.position.x,
            Random.Range(-3f, 1f),
            transform.position.z
        );

        if (GameDataManager.gameStart == true)
        {
            Debug.Log("GhostChan Spawn!");
            Instantiate(GhostChanPrefab, spawnPosition, transform.rotation);
        }
    }
}
