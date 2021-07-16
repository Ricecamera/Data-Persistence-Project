using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]
public class HighScore
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class HighScoreCollection
{
    public List<HighScore> highScores;
}

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    private string playerName;
    private int playerScore = 0;

    public List<HighScore> m_highScoreTable = null; // save data that is used for loading from and saving into file
    public List<HighScore> HighScoreTable
    {
        get
        {
            return m_highScoreTable;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            LoadData();
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(gameObject);
    }

    public bool SetPlayerName(string name)
    {
        if (name == "")
        {
            return false;
        }
        return true;
    }

    public void LoadData()
    {
         string filePath = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            HighScoreCollection data = JsonUtility.FromJson<HighScoreCollection>(json);
            m_highScoreTable = data.highScores;
        }
    }

    public void SaveData()
    {
        HighScoreCollection saveData = new HighScoreCollection();
        saveData.highScores = m_highScoreTable;
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void AddData(string name, int score)
    {
        if (name != null)
        {
            int found = m_highScoreTable.FindIndex(item => item.playerName == name);

            if (found == -1)
            {
                HighScore newData = new HighScore();
                newData.playerName = playerName;
                newData.score = score;
                m_highScoreTable.Add(newData);
            }
            else
            {
                if (m_highScoreTable[found].score < score)
                {
                    m_highScoreTable[found].score = score;
                }
            }
        }
    }
}
