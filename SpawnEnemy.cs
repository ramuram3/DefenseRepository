using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpawnEnemy : MonoBehaviour
{
    public static SpawnEnemy instance;
    public GameObject[] prefebs;

    public List<SpawnData> spawnDataList;
    public SpawnData[] spawnDatas;

    public int spawnIndex;
    public bool spawnEnd;

    public int enemyUnitMaxLevel;
    public float enemyCurSpawnDelay;
    public float enemyNextSpawnDelay;
    float offset;

    void Awake()
    {
        instance = this;

        spawnDataList = new List<SpawnData>();
        ReadSpawnFile(GameManager.instance.curStage);
    }
    void Start()
    {
        enemyNextSpawnDelay = spawnDataList[0].delay;
    }
    void Update()
    {
        enemyCurSpawnDelay += Time.deltaTime;

        if(enemyCurSpawnDelay > enemyNextSpawnDelay && gameObject.GetComponent<Enemys>().hp > 0)
        {
            InstEnemy();
            enemyCurSpawnDelay = 0;
        }
    }

    public void ReadSpawnFile(int stage)
    {
        spawnDataList.Clear();
        spawnIndex = 0;

        TextAsset textFile = Resources.Load("Stage" + stage) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            Debug.Log(line);

            if (line == null)
                break;

            int id = int.Parse(line.Split(',')[0]);

            //리스폰 데이터 생성
            SpawnData spawnData = ScriptableObject.CreateInstance<SpawnData>();
            spawnData.id = int.Parse(line.Split(',')[0]);
            spawnData.moveSpeed = spawnDatas[id].moveSpeed;
            spawnData.attackSpeed = spawnDatas[id].attackSpeed;
            spawnData.attackDamage = spawnDatas[id].attackDamage;
            spawnData.range = spawnDatas[id].range;
            spawnData.maxHp = spawnDatas[id].maxHp;
            spawnData.hp = spawnDatas[id].hp;
            spawnData.hitNum = spawnDatas[id].hitNum;
            spawnData.delay = float.Parse(line.Split(',')[1]);
            offset = float.Parse(line.Split(',')[2]);
            spawnData.awakeLevel = int.Parse(line.Split(',')[3]);


            spawnDataList.Add(spawnData);
        }

        //텍스트 파일 닫기
        stringReader.Close();

    }

    void InstEnemy()
    {
        GameObject enemyObj = Instantiate(prefebs[spawnDataList[spawnIndex].id], transform);
        Enemys enemy= enemyObj.GetComponent<Enemys>();
        SpawnData spawnData = spawnDataList[spawnIndex];

        enemy.moveSpeed = spawnData.moveSpeed;
        enemy.attackSpeed = spawnData.attackSpeed[spawnData.level] * (100/(float)(100 + offset));
        enemy.attackDamage = spawnData.attackDamage[spawnData.level] * (1 + offset/100);
        enemy.range = spawnData.range[spawnData.level];
        enemy.maxHp = spawnData.maxHp[spawnData.level] * (1 + offset/100);
        enemy.hp = spawnData.hp[spawnData.level] * (1 + offset/100);
        enemy.hitNum = spawnData.hitNum[spawnData.level];
        enemy.level = spawnData.level;
        enemy.awakeLevel = spawnData.awakeLevel;



        spawnIndex++;
        if (spawnIndex == spawnDataList.Count)
        {
            spawnIndex = 0;
        }
        //다음 리스폰 딜레이 갱신
        enemyNextSpawnDelay = spawnDataList[spawnIndex].delay;
    }
}
