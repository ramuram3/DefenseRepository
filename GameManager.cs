using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int unitNum = 4;

    [Header("#Stage")]
    public int maxStage;
    public int stage; //(Ŭ���� �� �������� + 1) ��������
    public int curStage; // ���� �÷����ϰ� �ִ� ��������

    [Header("#Resources")]
    public float diamond;
    public float curStageDiamond;
    public int medal;
    public int[] dropItems;
    public int[] curStageDropItems;

    [Header("#Unit")]
    public SpawnData[] spawnDatas;
    public int[] strLevel;
    public int[] dexLevel;
    public int[] defLevel;
    public int[] lukLevel;
    public int[] statUpgradeDiamond;
    public int[] awakeLevel;
    public int[] awakeReqItemNum;

    [Header("#MedalSkill")]
    public int[] medalSkillLevel; //�ε����� unit id
    public int[] medalSkillLevelUpMedal; //�ε����� ��ｺų ����


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    void Start()
    {
        DataManager.instance.Load();
        if (DataManager.instance.gameData != null)
        {
            MedalManager.instance.medalInfo = DataManager.instance.gameData.medalInfo;
            stage = DataManager.instance.gameData.stage;
            diamond = DataManager.instance.gameData.diamond;
            medal = DataManager.instance.gameData.medal;
            medalSkillLevel = DataManager.instance.gameData.medalSkillLevel;
            for(int i = 0; i < dropItems.Length; i++)
            {
                dropItems[i] = DataManager.instance.gameData.dropItems[i];
            }
            for(int i =0; i < strLevel.Length; i++)
            {
                strLevel[i] = DataManager.instance.gameData.strLevel[i];
                dexLevel[i] = DataManager.instance.gameData.strLevel[i];
                defLevel[i] = DataManager.instance.gameData.strLevel[i];
                lukLevel[i] = DataManager.instance.gameData.strLevel[i];
                awakeLevel[i] = DataManager.instance.gameData.awakeLevel[i];
            }
        }
    }

    public void StageStart(int curStage) //�޴�ȭ�鿡�� ���ӽ���
    {
        if (stage < curStage) return;
        for (int i = 0; i < dropItems.Length; i++)
        {
            curStageDropItems[i] = 0;
        }
        curStageDiamond = 0;
        GameManager.instance.curStage = curStage;
        SceneManager.LoadScene(0);
        AudioManager.instance.PlayBgm(AudioManager.Bgm.InGame);
    }
    public void NextStageStart() //����ȭ�鿡�� ���� �������� ��ư ������ ��
    {
        for (int i = 0; i < dropItems.Length; i++)
        {
            curStageDropItems[i] = 0;
        }
        curStageDiamond = 0;
        GameManager.instance.curStage++;
        SceneManager.LoadScene(0);
        AudioManager.instance.StopSfx();
        AudioManager.instance.PlayBgm(AudioManager.Bgm.InGame);
    }

    public void goMenu()
    {
        for (int i = 0; i < dropItems.Length; i++)
        {
            curStageDropItems[i] = 0;
        }
        curStageDiamond = 0;
        SceneManager.LoadScene(1);
        AudioManager.instance.StopSfx();
        AudioManager.instance.PlayBgm(AudioManager.Bgm.Menu);

    }

    public void Stop()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
}
