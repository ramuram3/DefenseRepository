using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabBar : MonoBehaviour
{
    public GameObject[] panels;
    public GameObject[] tabs;

    public int panelIndex;

    void OnEnable()
    {
        ChangePanel(panelIndex);
    }
    public void ChangePanel(int index)
    {
        for(int i = 0; i < panels.Length; i++)
        {
            if(i == index)
            {
                panels[i].SetActive(true);
                tabs[i].GetComponent<Image>().color = Color.white;
                tabs[i].transform.SetAsLastSibling();
                panelIndex = i;
            }
            else
            {
                panels[i].SetActive(false);
                tabs[i].GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f,1);
            }
        }

    }
}
