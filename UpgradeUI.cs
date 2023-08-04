using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public Image image;
    public Sprite sprite;
    public int id;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnAlly.instance.allyUnitLevel[id] == SpawnAlly.instance.allyUnitMaxLevel)
        {
            image.sprite = sprite;
        }
    }
}
