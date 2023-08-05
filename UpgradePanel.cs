using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public Text diamondText;
    public Text[] dropItemsText;

    public Sprite[] portraits;
    public Sprite[] awakeReqItems;
    public Image curPortrait;
    public Image curAwakeReqItem;
    public Text awakeLevel;
    public Text strDiamond;
    public Text dexDiamond;
    public Text defDiamond;
    public Text lukDiamond;
    public Text[] infoTexts;
    public Image[] str;
    public Image[] dex;
    public Image[] def;
    public Image[] luk;
    public GameObject[] stars;
    public GameObject[] descPanels;
    public int curUnitId;

    public int descIndex;
    string descText;


    void LateUpdate()
    {
        diamondText.text = ((int)GameManager.instance.diamond).ToString();
        for (int i = 0; i < GameManager.instance.dropItems.Length; i++)
        {
            dropItemsText[i].text = GameManager.instance.dropItems[i].ToString();
        }
        infoTexts[0].text = "공격 : " + string.Format("{0:F1}", GameManager.instance.spawnDatas[curUnitId].attackDamage * (1 + 0.05f * GameManager.instance.strLevel[curUnitId]) * (1 + 0.2f * GameManager.instance.awakeLevel[curUnitId]));
        infoTexts[1].text = "공속 : " + string.Format("{0:F2}",GameManager.instance.spawnDatas[curUnitId].attackSpeed * (1/(1 + 0.05f * GameManager.instance.dexLevel[curUnitId])) * (1/(1 + 0.2f * GameManager.instance.awakeLevel[curUnitId])));
        infoTexts[2].text = "체력 : " + (int)(GameManager.instance.spawnDatas[curUnitId].maxHp * (1 + 0.05f * GameManager.instance.defLevel[curUnitId]) * (1 + 0.2f * GameManager.instance.awakeLevel[curUnitId]));
    }

    void OnEnable()
    {
        descIndex = -1;
        curPortrait.sprite = portraits[curUnitId];
        curAwakeReqItem.sprite = awakeReqItems[curUnitId];

        if (GameManager.instance.strLevel[curUnitId] == 20)
            strDiamond.text = "Max";
        else
            strDiamond.text = GameManager.instance.statUpgradeDiamond[GameManager.instance.strLevel[curUnitId]].ToString();

        if (GameManager.instance.dexLevel[curUnitId] == 20)
            dexDiamond.text = "Max";
        else
            dexDiamond.text = GameManager.instance.statUpgradeDiamond[GameManager.instance.dexLevel[curUnitId]].ToString();

        if (GameManager.instance.defLevel[curUnitId] == 20)
            defDiamond.text = "Max";
        else
            defDiamond.text = GameManager.instance.statUpgradeDiamond[GameManager.instance.defLevel[curUnitId]].ToString();

        if (GameManager.instance.lukLevel[curUnitId] == 20)
            lukDiamond.text = "Max";
        else
            lukDiamond.text = GameManager.instance.statUpgradeDiamond[GameManager.instance.lukLevel[curUnitId]].ToString();

        if (GameManager.instance.awakeLevel[curUnitId] == 4)
        {
            awakeLevel.text = "Max";
        }
        else
        {
            awakeLevel.text = GameManager.instance.awakeReqItemNum[GameManager.instance.awakeLevel[curUnitId]].ToString();
        }

        for (int i = 0; i < 5; i++)
        {
            str[i].rectTransform.localScale = Vector3.zero;
            dex[i].rectTransform.localScale = Vector3.zero;
            def[i].rectTransform.localScale = Vector3.zero;
            luk[i].rectTransform.localScale = Vector3.zero;
        }

        for (int i = 0; i < GameManager.instance.strLevel[curUnitId] % 5; i++)
        {
            str[i].rectTransform.localScale = Vector3.one * 0.8f;
        }
        if(GameManager.instance.strLevel[curUnitId] == GameManager.instance.awakeLevel[curUnitId] * 5 + 5)
        {
            for (int i = 0; i < 5; i++)
            {
                str[i].rectTransform.localScale = Vector3.one * 0.8f;
            }
        }
        for (int i = 0; i < GameManager.instance.dexLevel[curUnitId] % 5; i++)
        {
            dex[i].rectTransform.localScale = Vector3.one * 0.8f;
        }
        if (GameManager.instance.dexLevel[curUnitId] == GameManager.instance.awakeLevel[curUnitId] * 5 + 5)
        {
            for (int i = 0; i < 5; i++)
            {
                dex[i].rectTransform.localScale = Vector3.one * 0.8f;
            }
        }
        for (int i = 0; i < GameManager.instance.defLevel[curUnitId] % 5; i++)
        {
            def[i].rectTransform.localScale = Vector3.one * 0.8f;
        }
        if (GameManager.instance.defLevel[curUnitId] == GameManager.instance.awakeLevel[curUnitId] * 5 + 5)
        {
            for (int i = 0; i < 5; i++)
            {
                def[i].rectTransform.localScale = Vector3.one * 0.8f;
            }
        }
        for (int i = 0; i < GameManager.instance.lukLevel[curUnitId] % 5; i++)
        {
            luk[i].rectTransform.localScale = Vector3.one * 0.8f;
        }
        if (GameManager.instance.lukLevel[curUnitId] == GameManager.instance.awakeLevel[curUnitId] * 5 + 5)
        {
            for (int i = 0; i < 5; i++)
            {
                luk[i].rectTransform.localScale = Vector3.one * 0.8f;
            }
        }
        for(int i = 0; i < 4; i++)
        {
            stars[i].SetActive(false);
        }
        for(int i = 0; i < GameManager.instance.awakeLevel[curUnitId]; i++)
        {
            stars[i].SetActive(true);
        }
    }
    public void PageUp()
    {
        descIndex = -1;
        if(curUnitId != 3)
        {
            curUnitId++;
        }
        else curUnitId = 0;

        curPortrait.sprite = portraits[curUnitId];
        curAwakeReqItem.sprite = awakeReqItems[curUnitId];

        if (GameManager.instance.strLevel[curUnitId] == 20)
            strDiamond.text = "Max";
        else
            strDiamond.text = GameManager.instance.statUpgradeDiamond[GameManager.instance.strLevel[curUnitId]].ToString();

        if (GameManager.instance.dexLevel[curUnitId] == 20)
            dexDiamond.text = "Max";
        else
            dexDiamond.text = GameManager.instance.statUpgradeDiamond[GameManager.instance.dexLevel[curUnitId]].ToString();

        if (GameManager.instance.defLevel[curUnitId] == 20)
            defDiamond.text = "Max";
        else
            defDiamond.text = GameManager.instance.statUpgradeDiamond[GameManager.instance.defLevel[curUnitId]].ToString();

        if (GameManager.instance.lukLevel[curUnitId] == 20)
            lukDiamond.text = "Max";
        else
            lukDiamond.text = GameManager.instance.statUpgradeDiamond[GameManager.instance.lukLevel[curUnitId]].ToString();

        for (int i = 0; i < 4; i++)
        {
            stars[i].SetActive(false);
        }
        for (int i = 0; i < GameManager.instance.awakeLevel[curUnitId]; i++)
        {
            stars[i].SetActive(true);
        }

        if (GameManager.instance.awakeLevel[curUnitId] == 4)
        {
            awakeLevel.text = "Max";
        }
        else
        {
            awakeLevel.text = GameManager.instance.awakeReqItemNum[GameManager.instance.awakeLevel[curUnitId]].ToString();
        }

        for (int i = 0; i < 5; i++)
        {
            str[i].rectTransform.localScale = Vector3.zero;
            dex[i].rectTransform.localScale = Vector3.zero;
            def[i].rectTransform.localScale = Vector3.zero;
            luk[i].rectTransform.localScale = Vector3.zero;
        }
        for (int i = 0; i < GameManager.instance.strLevel[curUnitId] % 5; i++)
        {
            str[i].rectTransform.localScale = Vector3.one * 0.8f;
        }
        if (GameManager.instance.strLevel[curUnitId] == GameManager.instance.awakeLevel[curUnitId] * 5 + 5)
        {
            for (int i = 0; i < 5; i++)
            {
                str[i].rectTransform.localScale = Vector3.one * 0.8f;
            }
        }
        for (int i = 0; i < GameManager.instance.dexLevel[curUnitId] % 5; i++)
        {
            dex[i].rectTransform.localScale = Vector3.one * 0.8f;
        }
        if (GameManager.instance.dexLevel[curUnitId] == GameManager.instance.awakeLevel[curUnitId] * 5 + 5)
        {
            for (int i = 0; i < 5; i++)
            {
                dex[i].rectTransform.localScale = Vector3.one * 0.8f;
            }
        }
        for (int i = 0; i < GameManager.instance.defLevel[curUnitId] % 5; i++)
        {
            def[i].rectTransform.localScale = Vector3.one * 0.8f;
        }
        if (GameManager.instance.defLevel[curUnitId] == GameManager.instance.awakeLevel[curUnitId] * 5 + 5)
        {
            for (int i = 0; i < 5; i++)
            {
                def[i].rectTransform.localScale = Vector3.one * 0.8f;
            }
        }
        for (int i = 0; i < GameManager.instance.lukLevel[curUnitId] % 5; i++)
        {
            luk[i].rectTransform.localScale = Vector3.one * 0.8f;
        }
        if (GameManager.instance.lukLevel[curUnitId] == GameManager.instance.awakeLevel[curUnitId] * 5 + 5)
        {
            for (int i = 0; i < 5; i++)
            {
                luk[i].rectTransform.localScale = Vector3.one * 0.8f;
            }
        }

    }

    public void PageDown()
    {
        descIndex = -1;
        if (curUnitId != 0)
        {
            curUnitId--;
        }
        else curUnitId = 3;

        curPortrait.sprite = portraits[curUnitId];
        curAwakeReqItem.sprite = awakeReqItems[curUnitId];

        if (GameManager.instance.strLevel[curUnitId] == 20)
            strDiamond.text = "Max";
        else
            strDiamond.text = GameManager.instance.statUpgradeDiamond[GameManager.instance.strLevel[curUnitId]].ToString();

        if (GameManager.instance.dexLevel[curUnitId] == 20)
            dexDiamond.text = "Max";
        else
            dexDiamond.text = GameManager.instance.statUpgradeDiamond[GameManager.instance.dexLevel[curUnitId]].ToString();

        if (GameManager.instance.defLevel[curUnitId] == 20)
            defDiamond.text = "Max";
        else
            defDiamond.text = GameManager.instance.statUpgradeDiamond[GameManager.instance.defLevel[curUnitId]].ToString();

        if (GameManager.instance.lukLevel[curUnitId] == 20)
            lukDiamond.text = "Max";
        else
            lukDiamond.text = GameManager.instance.statUpgradeDiamond[GameManager.instance.lukLevel[curUnitId]].ToString();

        for (int i = 0; i < 4; i++)
        {
            stars[i].SetActive(false);
        }
        for (int i = 0; i < GameManager.instance.awakeLevel[curUnitId]; i++)
        {
            stars[i].SetActive(true);
        }

        if (GameManager.instance.awakeLevel[curUnitId] == 4)
        {
            awakeLevel.text = "Max";
        }
        else
        {
            awakeLevel.text = GameManager.instance.awakeReqItemNum[GameManager.instance.awakeLevel[curUnitId]].ToString();
        }

        for (int i = 0; i < 5; i++)
        {
            str[i].rectTransform.localScale = Vector3.zero;
            dex[i].rectTransform.localScale = Vector3.zero;
            def[i].rectTransform.localScale = Vector3.zero;
            luk[i].rectTransform.localScale = Vector3.zero;
        }
        for (int i = 0; i < GameManager.instance.strLevel[curUnitId] % 5; i++)
        {
            str[i].rectTransform.localScale = Vector3.one * 0.8f;
        }
        if (GameManager.instance.strLevel[curUnitId] == GameManager.instance.awakeLevel[curUnitId] * 5 + 5)
        {
            for (int i = 0; i < 5; i++)
            {
                str[i].rectTransform.localScale = Vector3.one * 0.8f;
            }
        }
        for (int i = 0; i < GameManager.instance.dexLevel[curUnitId] % 5; i++)
        {
            dex[i].rectTransform.localScale = Vector3.one * 0.8f;
        }
        if (GameManager.instance.dexLevel[curUnitId] == GameManager.instance.awakeLevel[curUnitId] * 5 + 5)
        {
            for (int i = 0; i < 5; i++)
            {
                dex[i].rectTransform.localScale = Vector3.one * 0.8f;
            }
        }
        for (int i = 0; i < GameManager.instance.defLevel[curUnitId] % 5; i++)
        {
            def[i].rectTransform.localScale = Vector3.one * 0.8f;
        }
        if (GameManager.instance.defLevel[curUnitId] == GameManager.instance.awakeLevel[curUnitId] * 5 + 5)
        {
            for (int i = 0; i < 5; i++)
            {
                def[i].rectTransform.localScale = Vector3.one * 0.8f;
            }
        }
        for (int i = 0; i < GameManager.instance.lukLevel[curUnitId] % 5; i++)
        {
            luk[i].rectTransform.localScale = Vector3.one * 0.8f;
        }
        if (GameManager.instance.lukLevel[curUnitId] == GameManager.instance.awakeLevel[curUnitId] * 5 + 5)
        {
            for (int i = 0; i < 5; i++)
            {
                luk[i].rectTransform.localScale = Vector3.one * 0.8f;
            }
        }

    }

    public void StatUpgrade(int statId)
    {

        // statId 
        // 0 : str
        // 1 : dex
        // 2 : def
        // 3 : luk
        switch (statId)
        {
            case 0 :
                if (GameManager.instance.diamond >= GameManager.instance.statUpgradeDiamond[GameManager.instance.strLevel[curUnitId]] &&
                    GameManager.instance.strLevel[curUnitId] < GameManager.instance.awakeLevel[curUnitId] * 5 + 5 &&
                    GameManager.instance.strLevel[curUnitId] < 20)
                {
                    GameManager.instance.diamond -= GameManager.instance.statUpgradeDiamond[GameManager.instance.strLevel[curUnitId]];
                    str[GameManager.instance.strLevel[curUnitId] % 5].rectTransform.localScale = Vector3.one * 0.8f;
                    GameManager.instance.strLevel[curUnitId]++;
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Upgrade);
                    DataManager.instance.CheckStatLevel(curUnitId);
                    DataManager.instance.CheckDiamond();
                    DataManager.instance.Save();
                    if (GameManager.instance.strLevel[curUnitId] < 20)
                    {
                        strDiamond.text = GameManager.instance.statUpgradeDiamond[GameManager.instance.strLevel[curUnitId]].ToString();
                    }
                    else strDiamond.text = "Max";
                }
                else return;
                break;
            case 1 :
                if (GameManager.instance.diamond >= GameManager.instance.statUpgradeDiamond[GameManager.instance.dexLevel[curUnitId]] &&
                    GameManager.instance.dexLevel[curUnitId] < GameManager.instance.awakeLevel[curUnitId] * 5 + 5 &&
                    GameManager.instance.dexLevel[curUnitId] < 20)
                {
                    GameManager.instance.diamond -= GameManager.instance.statUpgradeDiamond[GameManager.instance.dexLevel[curUnitId]];
                    dex[GameManager.instance.dexLevel[curUnitId] % 5].rectTransform.localScale = Vector3.one * 0.8f;
                    GameManager.instance.dexLevel[curUnitId]++;
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Upgrade);
                    DataManager.instance.CheckStatLevel(curUnitId);
                    DataManager.instance.CheckDiamond();
                    DataManager.instance.Save();
                    if (GameManager.instance.dexLevel[curUnitId] < 20)
                    {
                        dexDiamond.text = GameManager.instance.statUpgradeDiamond[GameManager.instance.dexLevel[curUnitId]].ToString();
                    }
                    else dexDiamond.text = "Max";
                }
                else return;
                break;
            case 2 :
                if (GameManager.instance.diamond >= GameManager.instance.statUpgradeDiamond[GameManager.instance.defLevel[curUnitId]] &&
                    GameManager.instance.defLevel[curUnitId] < GameManager.instance.awakeLevel[curUnitId] * 5 + 5 &&
                    GameManager.instance.defLevel[curUnitId] < 20)
                {
                    GameManager.instance.diamond -= GameManager.instance.statUpgradeDiamond[GameManager.instance.defLevel[curUnitId]];
                    def[GameManager.instance.defLevel[curUnitId] % 5].rectTransform.localScale = Vector3.one * 0.8f;
                    GameManager.instance.defLevel[curUnitId]++;
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Upgrade);
                    DataManager.instance.CheckStatLevel(curUnitId);
                    DataManager.instance.CheckDiamond();
                    DataManager.instance.Save();
                    if (GameManager.instance.defLevel[curUnitId] < 20)
                    {
                        defDiamond.text = GameManager.instance.statUpgradeDiamond[GameManager.instance.defLevel[curUnitId]].ToString();
                    }
                    else defDiamond.text = "Max";
                }
                else return;
                break;
            case 3 :
                if (GameManager.instance.diamond >= GameManager.instance.statUpgradeDiamond[GameManager.instance.lukLevel[curUnitId]] &&
                    GameManager.instance.lukLevel[curUnitId] < GameManager.instance.awakeLevel[curUnitId] * 5 + 5 &&
                    GameManager.instance.lukLevel[curUnitId] < 20)
                {
                    GameManager.instance.diamond -= GameManager.instance.statUpgradeDiamond[GameManager.instance.lukLevel[curUnitId]];
                    luk[GameManager.instance.lukLevel[curUnitId] % 5].rectTransform.localScale = Vector3.one * 0.8f;
                    GameManager.instance.lukLevel[curUnitId]++;
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Upgrade);
                    DataManager.instance.CheckStatLevel(curUnitId);
                    DataManager.instance.CheckDiamond();
                    DataManager.instance.Save();
                    if (GameManager.instance.lukLevel[curUnitId] < 20)
                    {
                        lukDiamond.text = GameManager.instance.statUpgradeDiamond[GameManager.instance.lukLevel[curUnitId]].ToString();
                    }
                    else lukDiamond.text = "Max";
                }
                else return;
                break;
        }
    }

    public void AwakeUpgrade()
    {
        if (GameManager.instance.awakeLevel[curUnitId] < 4 &&
            GameManager.instance.dropItems[curUnitId] >= GameManager.instance.awakeReqItemNum[GameManager.instance.awakeLevel[curUnitId]])
        {
            if (GameManager.instance.strLevel[curUnitId] != (GameManager.instance.awakeLevel[curUnitId] * 5 + 5) ||
                GameManager.instance.dexLevel[curUnitId] != (GameManager.instance.awakeLevel[curUnitId] * 5 + 5) ||
                GameManager.instance.defLevel[curUnitId] != (GameManager.instance.awakeLevel[curUnitId] * 5 + 5) ||
                GameManager.instance.lukLevel[curUnitId] != (GameManager.instance.awakeLevel[curUnitId] * 5 + 5)) return;

            GameManager.instance.dropItems[curUnitId] -= GameManager.instance.awakeReqItemNum[GameManager.instance.awakeLevel[curUnitId]];
            GameManager.instance.awakeLevel[curUnitId]++;
            for (int i = 0; i < 4; i++)
            {
                stars[i].SetActive(false);
            }
            for (int i = 0; i < GameManager.instance.awakeLevel[curUnitId]; i++)
            {
                stars[i].SetActive(true);
            }
            DataManager.instance.CheckAwakeLevel(curUnitId);
            DataManager.instance.CheckDropItems();
            DataManager.instance.Save();
            if (GameManager.instance.awakeLevel[curUnitId] == 4)
            {
                awakeLevel.text = "Max";
            }
            else
            {
                awakeLevel.text = GameManager.instance.awakeReqItemNum[GameManager.instance.awakeLevel[curUnitId]].ToString();
            }
            for (int i = 0; i < 5; i++)
            {
                str[i].rectTransform.localScale = Vector3.zero;
                dex[i].rectTransform.localScale = Vector3.zero;
                def[i].rectTransform.localScale = Vector3.zero;
                luk[i].rectTransform.localScale = Vector3.zero;
            }
            // 각성 별 추가 
        }
    }

    public void ShowDescription(int index)
    {
        if(descIndex != index)
        {
            if(descIndex != -1)
            {
                descPanels[descIndex].SetActive(false);
            }
            descIndex = index;
            switch(descIndex)
            {
                case 0:
                    descText = "데미지 5% 증가\n" + "(현재 +" + GameManager.instance.strLevel[curUnitId] * 5 + "%)";
                    break;
                case 1:
                    descText = "공격속도 5% 증가\n" + "(현재 +" + GameManager.instance.dexLevel[curUnitId] * 5 + "%)";
                    break;
                case 2:
                    descText = "체력 5% 증가\n" + "(현재 +" + GameManager.instance.defLevel[curUnitId] * 5 + "%)";
                    break;
                case 3:
                    descText = "엘리트유닛 등장 확률 2% 증가\n" + "(현재 +" + (GameManager.instance.lukLevel[curUnitId] * 2 + 10) + "%)";
                    break;
            }
            descPanels[index].SetActive(true);
            descPanels[index].transform.GetChild(0).gameObject.GetComponent<Text>().text = descText;
        }
        else
        {
            descPanels[index].SetActive(false);
            descIndex = -1;
        }

    }
}
