using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn", menuName = "Scriptble Object/SpawnData")]
public class SpawnData : ScriptableObject
{
    public float moveSpeed;
    public float attackSpeed;
    public float attackDamage;
    public float[] range;
    public float maxHp;
    public float hp;
    public int[] hitNum;
    public int id;
    public int purchaseGold;

    // only Enemy
    public int level;
    public int awakeLevel;
    public float delay;
}
