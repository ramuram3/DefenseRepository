using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedalUpgradePanel : MonoBehaviour
{
    public GameObject[] skillsA;
    public GameObject[] skillsG;
    public GameObject[] skillsS;
    public GameObject[] skillsW;

    void Start()
    {
        for (int i = 0; i < skillsA.Length; i++)
        {
            int index = i;
            skillsA[index].GetComponent<Button>().onClick.AddListener(() => AcqurieSkill(0, index));
        }
        for (int i = 0; i < skillsG.Length; i++)
        {
            int index = i;
            skillsG[index].GetComponent<Button>().onClick.AddListener(() => AcqurieSkill(1, index));
        }
        for (int i = 0; i < skillsS.Length; i++)
        {
            int index = i;
            skillsS[index].GetComponent<Button>().onClick.AddListener(() => AcqurieSkill(2, index));
        }
        for (int i = 0; i < skillsW.Length; i++)
        {
            int index = i;
            skillsW[index].GetComponent<Button>().onClick.AddListener(() => AcqurieSkill(3, index));
        }
    }
    void OnEnable()
    {
        for(int i = 0; i < GameManager.instance.medalSkillLevel[0]; i++)
        {
            skillsA[i].GetComponent<Image>().color = Color.white;
        }
        for (int i = 0; i < GameManager.instance.medalSkillLevel[1]; i++)
        {
            skillsG[i].GetComponent<Image>().color = Color.white;
        }
        for (int i = 0; i < GameManager.instance.medalSkillLevel[2]; i++)
        {
            skillsS[i].GetComponent<Image>().color = Color.white;
        }
        for (int i = 0; i < GameManager.instance.medalSkillLevel[3]; i++)
        {
            skillsW[i].GetComponent<Image>().color = Color.white;
        }
    }

    public void AcqurieSkill(int id, int skillLevel)
    {
        Debug.Log("PressedAS");
        if (GameManager.instance.medalSkillLevel[id] == skillLevel && GameManager.instance.medal >= GameManager.instance.medalSkillLevelUpMedal[skillLevel] && GameManager.instance.medalSkillLevel[id] != skillsA.Length)
        {
            switch (id)
            {
                case 0:
                    skillsA[GameManager.instance.medalSkillLevel[id]].GetComponent<Image>().color = Color.white;
                    GameManager.instance.medal -= GameManager.instance.medalSkillLevelUpMedal[skillLevel];
                    break;
                case 1:
                    skillsG[GameManager.instance.medalSkillLevel[id]].GetComponent<Image>().color = Color.white;
                    GameManager.instance.medal -= GameManager.instance.medalSkillLevelUpMedal[skillLevel];
                    break;
                case 2:
                    skillsS[GameManager.instance.medalSkillLevel[id]].GetComponent<Image>().color = Color.white;
                    GameManager.instance.medal -= GameManager.instance.medalSkillLevelUpMedal[skillLevel];
                    break;
                case 3:
                    skillsW[GameManager.instance.medalSkillLevel[id]].GetComponent<Image>().color = Color.white;
                    GameManager.instance.medal -= GameManager.instance.medalSkillLevelUpMedal[skillLevel];
                    break;
            }
            GameManager.instance.medalSkillLevel[id]++;
            DataManager.instance.CheckMedalSkillLevel();
            DataManager.instance.CheckMedal();
            DataManager.instance.Save();
        }
    }

    public void Test()
    {
        Debug.Log("Pressed");
    }
}
