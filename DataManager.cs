using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameData
{
    public int[] medalInfo= new int[20];
    public int stage;
    public float diamond;
    public int medal;
    public int[] dropItems = new int[4];
    public int[] strLevel = new int[4];
    public int[] dexLevel = new int[4];
    public int[] defLevel = new int[4];
    public int[] lukLevel = new int[4];
    public int[] awakeLevel = new int[4];
    public int[] medalSkillLevel = new int[4]; //ÀÎµ¦½º´Â unit id

}


public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public GameData gameData = new GameData();

    string path;
    string gameDataFilename = "saveGameData";
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

        path = Application.persistentDataPath + "/";

    }

    public void Save()
    {
        string _gameData = JsonUtility.ToJson(gameData);
        File.WriteAllText(path + gameDataFilename, _gameData);
    }

    public void Load()
    {
        if (!File.Exists(path + gameDataFilename)) return;

        string _gameData = File.ReadAllText(path + gameDataFilename);
        if(_gameData == null) return;
        gameData = JsonUtility.FromJson<GameData>(_gameData);
    }

    public void CheckStage(int stage)
    {
        gameData.stage = stage;
    }

    public void CheckMedalInfo(int stage)
    {
        gameData.medalInfo[stage] = MedalManager.instance.medalInfo[stage];
        gameData.medal = GameManager.instance.medal;
        
    }

    public void CheckMedal()
    {
        gameData.medal = GameManager.instance.medal;
    }

    public void CheckDiamond()
    {
        gameData.diamond = GameManager.instance.diamond;
    }

    public void CheckDropItems()
    {
        for(int i = 0; i < gameData.dropItems.Length; i++)
        {
            gameData.dropItems[i] = GameManager.instance.dropItems[i];
        }
    }

    public void CheckStatLevel(int i)
    {
        gameData.strLevel[i] = GameManager.instance.strLevel[i];
        gameData.dexLevel[i] = GameManager.instance.dexLevel[i];
        gameData.defLevel[i] = GameManager.instance.defLevel[i];
        gameData.lukLevel[i] = GameManager.instance.lukLevel[i];
    }

    public void CheckAwakeLevel(int i)
    {
        gameData.awakeLevel[i] = GameManager.instance.awakeLevel[i];
    }

    public void CheckMedalSkillLevel()
    {
        gameData.medalSkillLevel = GameManager.instance.medalSkillLevel;
    }
}
