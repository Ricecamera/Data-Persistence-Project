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

    public HighScoreCollection m_highScoreTable = null; // save data that is used for loading from and saving into file
    public HighScoreCollection HighScoreTable
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

    public void SetPlayerName(string name)
    {
        if (name == null)
        {
            Debug.LogError("Please try a name");
        }
        else
        {
            playerName = name;
        }
    }

    public void LoadData()
    {
         string filePath = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            m_highScoreTable = JsonUtility.FromJson<HighScoreCollection>(json);
        }
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(m_highScoreTable);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void AddData(string name, int score)
    {
        if (name != null)
        {
            int found = m_highScoreTable.highScores.FindIndex(item => item.playerName == name);

            if (found == -1)
            {
                HighScore newData = new HighScore();
                newData.playerName = playerName;
                newData.score = score;
                m_highScoreTable.highScores.Add(newData);
            }
            else
            {
                if (m_highScoreTable.highScores[found].score < score)
                {
                    m_highScoreTable.highScores[found].score = score;
                }
            }
        }
    }
}
