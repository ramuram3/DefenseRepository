using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnAlly : MonoBehaviour
{
    public static SpawnAlly instance;
    public GameObject[] prefebs;
    
    public int[] allyUnitLevel;
    public int allyUnitMaxLevel;
    public int[] purchaseGold;
    public int[] allyUnitDefaultUpGradeGold;
    public float[] spawnDelay;
    public float[] coolTimeTimer;
    public bool[] isReady;

    public float gold;
    public float defaultGPS; //gold per second
    public float defaultGPSLevelUpGold;

    public int GPSlevel;
    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        gold += defaultGPS * (GPSlevel * 0.5f + 1) * Time.deltaTime;

        for (int i = 0; i < prefebs.Length; i++)
        {
            if(!isReady[i])
            {
                coolTimeTimer[i] -= Time.deltaTime;
                if (coolTimeTimer[i] <= 0) isReady[i] = true;
            }
        }
    }
    public void InstAlly(SpawnData spawnData)
    {
        if(gold >= purchaseGold[spawnData.id] && isReady[spawnData.id])
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Buy);

            isReady[spawnData.id] = false;
            coolTimeTimer[spawnData.id] = spawnDelay[spawnData.id];

            gold -= purchaseGold[spawnData.id];
            GameObject unitObj = Instantiate(prefebs[spawnData.id], transform);
            Allys ally = unitObj.GetComponent<Allys>();
            ally.moveSpeed = spawnData.moveSpeed;
            ally.attackSpeed = spawnData.attackSpeed * (1 / (1 + 0.2f * allyUnitLevel[spawnData.id])) * (1/(float)(1 + 0.05f * GameManager.instance.dexLevel[spawnData.id])) * 1/(1 + 0.2f * GameManager.instance.awakeLevel[spawnData.id]);
            ally.attackDamage = spawnData.attackDamage * (1 + 0.2f * allyUnitLevel[spawnData.id]) * (1 + 0.05f * GameManager.instance.strLevel[spawnData.id]) * (1 + 0.2f * GameManager.instance.awakeLevel[spawnData.id]);
            ally.range = spawnData.range[allyUnitLevel[spawnData.id]];
            ally.maxHp = spawnData.maxHp * (1 + 0.2f * allyUnitLevel[spawnData.id]) * (1 + 0.05f * GameManager.instance.defLevel[spawnData.id]) * (1 + 0.2f * GameManager.instance.awakeLevel[spawnData.id]);
            ally.hp = spawnData.hp * (1 + 0.2f * allyUnitLevel[spawnData.id]) * (1 + 0.05f * GameManager.instance.defLevel[spawnData.id]);
            int ran = Random.Range(0, 100);
            if(ran<10 + 2 * GameManager.instance.lukLevel[spawnData.id])
            {
                ally.isElite = true;
            }
            Debug.Log(10 + 2 * GameManager.instance.lukLevel[spawnData.id]);
            ally.hitNum = spawnData.hitNum[allyUnitLevel[spawnData.id]];
            ally.level = allyUnitLevel[spawnData.id];
        }
    }

    public void UnitLevelUp(int id)
    {
        if (allyUnitLevel[id] < allyUnitMaxLevel && gold >= allyUnitDefaultUpGradeGold[id] * (allyUnitLevel[id] * 0.5f + 1))
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Upgrade);
            gold -= allyUnitDefaultUpGradeGold[id] * (allyUnitLevel[id] + 1);

            allyUnitLevel[id]++;
        }
    }

    public void GPSLevelUp()
    {
        if (gold >= defaultGPSLevelUpGold * (GPSlevel + 1))
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Upgrade);

            gold -= defaultGPSLevelUpGold * (GPSlevel + 1);
            GPSlevel++;
        }
    }
}
