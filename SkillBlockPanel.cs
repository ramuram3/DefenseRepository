using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBlockPanel : MonoBehaviour
{
    public static SkillBlockPanel instance;

    public RectTransform instantiateRectTrans;
    public RectTransform skillBlocksZone;
    public RectTransform[] blockSpacesRectTrans;
    public GameObject[] blockPrefebs;
    public SkillBlock[] skillBlocks;
    public List<SkillBlock> skillBlockList;

    public float timer;
    public float reloadBlockTime;
    public float blockMoveVelocity;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        skillBlockList = new List<SkillBlock>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= reloadBlockTime && skillBlockList.Count < blockSpacesRectTrans.Length)
        {
            InstantiateBlock();
            timer = 0;
        }

    }

    public void InstantiateBlock()
    {
        Debug.Log("inst");
        int id = Random.Range(0, 4);
        GameObject skillBlockObj = Instantiate(blockPrefebs[id], instantiateRectTrans.anchoredPosition, Quaternion.identity,skillBlocksZone);
        skillBlockObj.GetComponent<RectTransform>().anchoredPosition = instantiateRectTrans.anchoredPosition;
        Debug.Log(instantiateRectTrans.anchoredPosition);
        SkillBlock skillBlock = skillBlockObj.GetComponent<SkillBlock>();
        skillBlock.moveSpeed = blockMoveVelocity;
        skillBlock.id = id;
       
    }
}