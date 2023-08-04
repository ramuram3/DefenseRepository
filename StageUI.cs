using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    public GameObject[] Lockedstages;

    void Awake()
    {
        for(int i = 0; i < GameManager.instance.maxStage; i++)
        {
            int index = i;
            transform.GetChild(i).gameObject.GetComponent<Button>().onClick.AddListener(() => GameManager.instance.StageStart(index));
        }
    }
    void OnEnable()
    {
        for(int i = 0; i < Lockedstages.Length; i++)
        {
            for (int j = 0; j < MedalManager.instance.medalInfo[i]; j++)
                Lockedstages[i].transform.GetChild(j).gameObject.SetActive(false);
        }
    }

}
