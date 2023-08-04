using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum Type { Level, curGold, purchaseGold, upgradeGold, GPSUpgradeGold, health, stage }
    public Type type;

    public int id;

    Text text;
    public Slider slider;
    public GameObject castle;

    public float[] levelUpGold;

    void Awake()
    {
        text = GetComponent<Text>();
    }
    void LateUpdate()
    {
        switch(type)
        {
            case Type.Level:
                text.text = "Lv. " + (SpawnAlly.instance.allyUnitLevel[id] + 1).ToString();
                break;
            case Type.curGold:
                text.text = ((int)(SpawnAlly.instance.gold)).ToString() + " G";
                break;
            case Type.purchaseGold:
                text.text = (SpawnAlly.instance.purchaseGold[id]).ToString() + " G";
                break;
            case Type.upgradeGold:
                if (SpawnAlly.instance.allyUnitLevel[id] < SpawnAlly.instance.allyUnitMaxLevel)
                {
                    int upgradeGold = SpawnAlly.instance.allyUnitDefaultUpGradeGold[id]
                                   * (SpawnAlly.instance.allyUnitLevel[id] + 1);
                    text.text = upgradeGold.ToString() + " G";
                }
                else if(SpawnAlly.instance.allyUnitLevel[id] == SpawnAlly.instance.allyUnitMaxLevel)
                {
                    text.text = "MaxLv";

                }
                break;
            case Type.GPSUpgradeGold:
                float GPSUpgradeGold = SpawnAlly.instance.defaultGPSLevelUpGold *
                    (SpawnAlly.instance.GPSlevel + 1);
                text.text = ((int)GPSUpgradeGold).ToString() + " G";
                break;
            case Type.health:
                if(castle.tag == "AllyCastle")
                    slider.value = castle.GetComponent<Allys>().hp / castle.GetComponent<Allys>().maxHp;
                if(castle.tag == "EnemyCastle")
                    slider.value = castle.GetComponent<Enemys>().hp / castle.GetComponent<Enemys>().maxHp;
                break;
            case Type.stage:
                text.text = "Stage " + (GameManager.instance.curStage + 2);
                break;
        }
    }
}
