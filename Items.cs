using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/Items")]
public class Items : ScriptableObject
{
    public Sprite sprite;
    public int id;
    public float count;
    /*
     0 : ���̾�
     1 : ADrop
     2 : GDrop
     3 : SDrop
     4 : WDrop

    */
}
