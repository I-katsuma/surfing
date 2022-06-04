using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] Enemies;

    public GameObject LuckyFrog;

    public enum NOWSTAGE
    {
        STAGE1,
        STAGE2,
        STAGE3,
        STAGE4,
        STAGE5
    }

    public static NOWSTAGE nowStage;

    int nowNum = 1;

    [SerializeField]
    Text nowSategeText;

    float startNum;
    float spawnNum;

    [SerializeField] Player player;

    void Start()
    {
        nowStage = NOWSTAGE.STAGE1;
        nowNum = 1;
        NowStageMetnod(nowNum);
        RandomNum();

        InvokeRepeating("RandomNum", 10f, 10f);
        InvokeRepeating("MobSpawnEnterA", startNum, spawnNum / GameDataManager.gameSpeed);
        InvokeRepeating("MobSpawnEnterB", 130f, 120f); // LuckyFrog
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
                int num = Random.Range(0, Enemies.Length);
                Spawn(Enemies[num], -3.5f, 1f);
                ParipiCheck(num);
            }
            else if (nowStage == NOWSTAGE.STAGE4)
            {
                Spawn(Enemies[3], -4f, 1f);
                ParipiCheck(3);
            }
            else if (nowStage == NOWSTAGE.STAGE3)
            {
                Spawn(Enemies[2], -4f, 0.5f);
                ParipiCheck(2);
            }
            else if (nowStage == NOWSTAGE.STAGE2)
            {
                Spawn(Enemies[1], -3f, 1f);
                ParipiCheck(1);
            }
            else // stage1
            {
                Spawn(Enemies[0], -3f, 1f);
                ParipiCheck(0);
            }
        }
    }

    void ParipiCheck(int num)
    {

        if(player.state == Player.STATE.MUTEKI)
        {
            Spawn(Enemies[num], -3f, 1f);
        }
    }


    void MobSpawnEnterB()
    {
        if (GameDataManager.gameState == GameDataManager.GAMESTAGESTATE.GAMENOW)
        {
            if (player.state == Player.STATE.NORMAL)
            {
                if (nowStage != NOWSTAGE.STAGE5)
                {
                    return;
                }
                else
                {
                    Spawn(LuckyFrog, 3f, 1f);
                }
            }
        }
    }

    public void StageChangeButton()
    {
        nowNum++;
        NowStageMetnod(nowNum);
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

    void NowStageMetnod(int num)
    {
        nowSategeText.text = "STAGE : " + num.ToString();
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
