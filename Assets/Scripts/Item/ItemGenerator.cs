using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //public GameObject applePrefab;

    [SerializeField]
    GameObject[] fruitPrefabs;

    public GameObject KOBAN_prefab;

    //[SerializeField] GameObject[] RarePrefabs;

    Player player = null;

    float startNum,
        spawnNum;
    float startNum2,
        spawnNum2;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        RandomNum();

        InvokeRepeating("RandomNum", 1f, 1f);
        InvokeRepeating("RandomItemSpawn", startNum, spawnNum / GameDataManager.gameSpeed);
        InvokeRepeating("RandomItemSpawn", startNum2, spawnNum2 / GameDataManager.gameSpeed);
        //InvokeRepeating("RandomRareItemSpawn", 3f, 12f);
        InvokeRepeating("KOBAN_Spawn", 3f, 12f);
    }

    void RandomNum()
    {
        
        if (player.state == Player.STATE.NORMAL || player.state == Player.STATE.SHOWTIMEREADY)
        {
            startNum = Random.Range(1.2f, 1.8f);
            spawnNum = Random.Range(6.5f, 8.0f);
            startNum2 = Random.Range(1.5f, 2.6f);
            spawnNum2 = Random.Range(9.5f, 15.0f);
        }
        else if(player.state == Player.STATE.MUTEKI)
        {
            spawnNum2 = Random.Range(2f, 2f);
        }
    }

    void RandomItemSpawn()
    {
        int num = Random.Range(0, fruitPrefabs.Length);

        Vector3 spawnPoint = new Vector3(
            transform.position.x,
            Random.Range(-3f, 1f),
            transform.position.z
        );

        if (GameDataManager.gameState == GameDataManager.GAMESTAGESTATE.GAMENOW)
        {
            Debug.Log(fruitPrefabs[num].name + " Spawn!");
            Instantiate(fruitPrefabs[num], spawnPoint, transform.rotation);
        }
    }

    /*
    void RandomRareItemSpawn()
    {
        int num = Random.Range(0, RarePrefabs.Length);

        Vector3 spawnPoint = new Vector3(
            transform.position.x,
            Random.Range(-2.8f, 1f),
            transform.position.z
        );

        if (GameDataManager.gameState == GameDataManager.GAMESTAGESTATE.GAMENOW)
        {
            Debug.Log(RarePrefabs[num].name + " Spawn!");
            if (player.state == Player.STATE.NORMAL)
            {
                Instantiate(RarePrefabs[num], spawnPoint, transform.rotation);
            }
        }
    }
    */

    /*
    void AppleSpawn()
    {
        Vector3 spawnPoint = new Vector3(
            transform.position.x,
            Random.Range(-3f, 1f),
            transform.position.z
        );

        if (GameDataManager.gameState == GameDataManager.GAMESTAGESTATE.GAMENOW)
        {
            Debug.Log("Apple Spawn!");
            Instantiate(applePrefab, spawnPoint, transform.rotation);
        }
    }
    */

    void KOBAN_Spawn()
    {
        Vector3 spawnPoint = new Vector3(
            transform.position.x,
            Random.Range(-2.8f, 1f),
            transform.position.z
        );

        if (GameDataManager.gameState == GameDataManager.GAMESTAGESTATE.GAMENOW)
        {
            Debug.Log("KOBAN Spawn!");
            if (player.state == Player.STATE.NORMAL || player.state == Player.STATE.SHOWTIMEREADY)
            {
                Instantiate(KOBAN_prefab, spawnPoint, transform.rotation);
            }
        }
    }
}
