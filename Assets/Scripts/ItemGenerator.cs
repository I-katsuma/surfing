using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject applePrefab;

    public GameObject KOBAN_prefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AppleSpawn", 2.2f, 8f / GameDataManager.gameSpeed);        
        InvokeRepeating("KOBAN_Spawn", 1f, 12f);
    }

    void AppleSpawn()
    {
        Vector3 spawnPoint = new Vector3(
            transform.position.x,
            Random.Range(-3f, 1f),
            transform.position.z
        );

        if(GameDataManager.gameStart == true)
        {
            Debug.Log("Apple Spawn!");
            Instantiate(applePrefab, spawnPoint, transform.rotation);
        }
    }

    void KOBAN_Spawn()
    {
         Vector3 spawnPoint = new Vector3(
            transform.position.x,
            Random.Range(-2.8f, 1f),
            transform.position.z
        );

        if(GameDataManager.gameStart == true)
        {
            Debug.Log("KOBAN Spawn!");
            Instantiate(KOBAN_prefab, spawnPoint, transform.rotation);
        }       
    }

}
