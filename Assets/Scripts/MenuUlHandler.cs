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
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
