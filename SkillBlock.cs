using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBlock : MonoBehaviour
{
    RectTransform rect;

    public float moveSpeed;

    public int id;

    public bool isSettled;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!isSettled)
        {
            rect.anchoredPosition += Vector2.left * moveSpeed * Time.deltaTime;

            if (SkillBlockPanel.instance.skillBlockList.Count < SkillBlockPanel.instance.blockSpacesRectTrans.Length
                && rect.anchoredPosition.x <= SkillBlockPanel.instance.blockSpacesRectTrans[SkillBlockPanel.instance.skillBlockList.Count].anchoredPosition.x)
            {
                rect.anchoredPosition = SkillBlockPanel.instance.blockSpacesRectTrans[SkillBlockPanel.instance.skillBlockList.Count].anchoredPosition;
                SkillBlockPanel.instance.skillBlockList.Add(this);
                isSettled = true;
            }
        }
        Debug.Log(rect.anchoredPosition);
    }
}
