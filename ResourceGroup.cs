using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceGroup : MonoBehaviour
{
    public List<Items> itemList = new List<Items>();

    public Items[] items;

    public GameObject[] itemPanels;
    void OnEnable()
    {
        if(GameManager.instance.curStageDiamond != 0)
        {
            itemList.Add(items[0]);
        }
        for(int i = 0; i < 4; i++)
        {
            if (GameManager.instance.curStageDropItems[i] != 0)
            {
                itemList.Add(items[i + 1]);
            }
        }

        int[] indexs = new int[items.Length];

        for(int i = 0; i < items.Length; i++)
        {
            indexs[i] = itemList.FindIndex(a => a.id == i);
        }
        if (indexs[0] != -1)
        {
            itemList[indexs[0]].count = (int)GameManager.instance.curStageDiamond;
        }
        for (int i =1; i<items.Length; i++)
        {
            if (indexs[i] == -1) continue;
            itemList[indexs[i]].count = GameManager.instance.curStageDropItems[i-1];
        }

        for(int i = 0; i < itemList.Count; i++)
        {
            itemPanels[i].GetComponent<RectTransform>().localScale = Vector3.one;
            itemPanels[i].transform.GetChild(0).GetComponent<Image>().sprite = itemList[i].sprite;
            itemPanels[i].transform.GetChild(1).GetComponent<Text>().text = itemList[i].count.ToString();
        }
    }


}
