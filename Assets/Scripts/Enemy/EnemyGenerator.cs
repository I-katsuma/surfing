using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject BardPrefab;
    public GameObject GhostChanPrefab;
    public GameObject BlueFishMan;
    public GameObject BallBrathersPlafas;

    public GameObject LuckyFrog;

    // [SerializeField] GameDataManager gameDataManager;

    public enum NOWSTAGE
    {
        STAGE1,
        STAGE2,
        STAGE3,
        STAGE4,
        STAGE5
    }

    public static NOWSTAGE nowStage;

    static int nowNum = 1;

    [SerializeField]
    Text nosSategeText;

    float startNum;
    float spawnNum;

    [SerializeField]
    Player player;

    void Start()
    {
        nowStage = NOWSTAGE.STAGE1;
        nosSategeText.text = "NowStage : " + nowNum.ToString();
        RandomNum();

        InvokeRepeating("RandomNum", 10f, 10f);
        InvokeRepeating("MobSpawnEnterA", startNum, spawnNum / GameDataManager.gameSpeed);
        InvokeRepeating("MobSpawnEnterB", 130f, 120f); // LuckyFrog
    }

    private void FixedUpdate()
    {
        nosSategeText.text = "NowStage : " + nowNum.ToString();
    }

    void RandomNum()
    {
        startNum = Random.Range(1.5f, 4.5f);
        spawnNum = Random.Range(6f, 12f);
    }

    void MobSpawnEnterA()
    {
        if (GameDataManager.gameState == GameDataManager.GAMESTAGESTATE.GAMENOW)
        {
            if (nowStage == NOWSTAGE.STAGE5)
            {
                Spawn(BardPrefab, -3f, 1f);
                //Spawn(BlueFishMan, -4.5f, 0.5f);
                Spawn(BallBrathersPlafas, -4f, 1f);
            }
            else if (nowStage == NOWSTAGE.STAGE4)
            {
                //Spawn(BlueFishMan, -4.5f, 0.5f);
                Spawn(BallBrathersPlafas, -4f, 1f);
            }
            else if (nowStage == NOWSTAGE.STAGE3)
            {
                //Spawn(GhostChanPrefab, -3f, 1f);
                Spawn(BlueFishMan, -4.5f, 0.5f);
            }
            else if (nowStage == NOWSTAGE.STAGE2)
            {
                Spawn(GhostChanPrefab, -3f, 1f);
                //Spawn(BardPrefab, -3f, 1f);
            }
            else // stage1
            {
                //Spawn(GhostChanPrefab, -3f, 1f);
                Spawn(BardPrefab, -3f, 1f);
            }
        }
    }

    void MobSpawnEnterB()
    {
        if (GameDataManager.gameState == GameDataManager.GAMESTAGESTATE.GAMENOW)
        {
            if (player.state == Player.STATE.NORMAL)
            {
                Spawn(LuckyFrog, 3f, 1f);
            }
        }
    }

    public static void StageChangeButton()
    {
        nowNum++;

        if (nowNum < 6)
        {
            switch (nowNum)
            {
                case 1:
                    nowStage = NOWSTAGE.STAGE1;
                    break;
                case 2:
                    nowStage = NOWSTAGE.STAGE2;
                    break;
                case 3:
                    nowStage = NOWSTAGE.STAGE3;
                    break;
                case 4:
                    nowStage = NOWSTAGE.STAGE4;
                    break;
                case 5:
                    nowStage = NOWSTAGE.STAGE5;
                    break;
                default:
                    nowStage = NOWSTAGE.STAGE1;
                    break;
            }
        }
        else
        {
            nowNum = 5;
        }
    }

    void Spawn(GameObject prefab, float rangeA, float rangeB)
    {
        Vector3 spawnPosition = new Vector3(
            transform.position.x,
            Random.Range(rangeA, rangeB), // (-3f, 1f)
            transform.position.z
        );

        Debug.Log(prefab + " Spawn!");
        Instantiate(prefab, spawnPosition, transform.rotation);
    }
}
