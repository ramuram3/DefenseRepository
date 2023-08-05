using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePanels : MonoBehaviour
{
    public Text diamondText;
    public Text medalText;
    public Text[] dropItemsText;

    void Update()
    {
        diamondText.text = ((int)GameManager.instance.diamond).ToString();
        medalText.text = ((int)GameManager.instance.medal).ToString();
        for (int i = 0; i < GameManager.instance.dropItems.Length; i++)
        {
            dropItemsText[i].text = GameManager.instance.dropItems[i].ToString();
        }
    }
}
