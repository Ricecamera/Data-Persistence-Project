using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUlHandler : MonoBehaviour
{
    public InputField in_playerName;
    public Text BestScoreText;

    public void Start()
    {
        in_playerName.text = MenuManager.Instance.playerName;

        if (MenuManager.Instance.HighScoreTable.Count > 0)
        {
            string bestScoreName = MenuManager.Instance.HighScoreTable[0].playerName;
            int bestScore = MenuManager.Instance.HighScoreTable[0].score;
            BestScoreText.text = $"Best Score: {bestScoreName} : {bestScore}";
        }
    }
    public void StartGame()
    {
        string name = in_playerName.text;
        bool valid = MenuManager.Instance.SetPlayerName(name);
        if (valid)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.LogError("Please try a name");
        }
        
    }

    public void Exit()
    {
        MenuManager.Instance.SaveData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
