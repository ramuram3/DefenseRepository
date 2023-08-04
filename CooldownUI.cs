using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public Image cooldownImage;
    public int id;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownImage.fillAmount = SpawnAlly.instance.coolTimeTimer[id] / SpawnAlly.instance.spawnDelay[id];
    }
}
