using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MedalManager : MonoBehaviour
{
    public static MedalManager instance;

    public int[] medalInfo;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }


    }

    public void MedalQuests(int stage, int medalNum)
    {
        medalInfo[stage] = medalNum;
    }
}
