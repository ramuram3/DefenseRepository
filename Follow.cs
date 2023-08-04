using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Follow : MonoBehaviour
{
    public GameObject castle;
    RectTransform rect;
    Vector3 pos;
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pos.x = Camera.main.WorldToScreenPoint(castle.transform.position).x;
        pos.y = Camera.main.WorldToScreenPoint(castle.transform.position).y + 115;
        rect.position = pos;
    }
}
