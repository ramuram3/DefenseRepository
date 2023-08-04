using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    public static WinLoseUI instance;

    public GameObject winGroup;
    public GameObject LoseGroup;
    public GameObject InGameGruop;
    public GameObject medalGroup;
    public GameObject resourceGroup;

    Image image;


    bool isChecked;
    void Awake()
    {
        instance = this;
        image = GetComponent<Image>();
        winGroup.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(GameManager.instance.NextStageStart);
        winGroup.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(GameManager.instance.goMenu);
        LoseGroup.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(GameManager.instance.goMenu);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StageEnd(bool isWin)
    {
        if (isChecked) return;

        if (isWin)
        {
            Invoke("Win", 1.5f);
        }
        else
        {
            Invoke("Lose", 1.5f);
        }

        isChecked = true;
    }

    void Win()
    {
        GameManager.instance.curStageDiamond *= 1.5f;
        medalGroup.SetActive(true);
        resourceGroup.SetActive(true);

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Victory);
        AudioManager.instance.StopBgm();
        if (MedalManager.instance.medalInfo[GameManager.instance.curStage] < 1)
        {
            MedalManager.instance.MedalQuests(GameManager.instance.curStage, 1); //º°1°³ Äù½ºÆ®
        }
        if (SpawnAlly.instance.gameObject.GetComponent<Allys>().hp >=
           SpawnAlly.instance.gameObject.GetComponent<Allys>().maxHp/2 &&
           MedalManager.instance.medalInfo[GameManager.instance.curStage] < 2) //º°2°³ Äù½ºÆ®
        {
            MedalManager.instance.MedalQuests(GameManager.instance.curStage, 2);
        }
        if (SpawnAlly.instance.gameObject.GetComponent<Allys>().hp ==
            SpawnAlly.instance.gameObject.GetComponent<Allys>().maxHp &&
            MedalManager.instance.medalInfo[GameManager.instance.curStage] < 3) //º°3°³ Äù½ºÆ®
        {
            MedalManager.instance.MedalQuests(GameManager.instance.curStage, 3);
        }


        if(GameManager.instance.curStage == GameManager.instance.stage)
        {
            GameManager.instance.stage++;
        }
        for(int i =0; i < DataManager.instance.gameData.dropItems.Length; i++)
        {
            GameManager.instance.dropItems[i] += GameManager.instance.curStageDropItems[i];
        }
        GameManager.instance.diamond += GameManager.instance.curStageDiamond;
        DataManager.instance.CheckStage(GameManager.instance.stage);
        DataManager.instance.CheckMedal(GameManager.instance.stage);
        DataManager.instance.CheckDiamond();
        DataManager.instance.CheckDropItems();
        DataManager.instance.Save();

        image.color = new Color(0, 0, 0, 0.5f);
        InGameGruop.SetActive(false);
        winGroup.SetActive(true);
    }

    void Lose()
    {
        resourceGroup.SetActive(true);

        DataManager.instance.CheckDiamond();
        DataManager.instance.CheckDropItems();
        DataManager.instance.Save();

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Defeat);
        AudioManager.instance.StopBgm();


        image.color = new Color(0, 0, 0, 0.5f);
        InGameGruop.SetActive(false);
        LoseGroup.SetActive(true);
    }
}
