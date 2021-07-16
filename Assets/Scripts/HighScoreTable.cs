using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private static int MAX_HIGHSCORES = 6;

    public Transform entryContainer;
    public Transform entryTemplate;
    private List<HighScore> highscoreEntryList;

    private void Awake()
    {
        entryTemplate.gameObject.SetActive(false);

        ShowHighScoreTable(entryContainer, MenuManager.Instance.HighScoreTable);
    }

    private void ShowHighScoreTable(Transform container, List<HighScore> highScoresList)
    {
        float templateHeight = 20f;

        for (int i = 0; i < highScoresList.Count && i < MAX_HIGHSCORES; ++i)
        {
            Transform entrytransform = Instantiate(entryTemplate, container);
            RectTransform entryRectTransform = entrytransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, - templateHeight * i + entryTemplate.localPosition.y);
            entrytransform.gameObject.SetActive(true);

            int rank = i + 1;

            entrytransform.Find("RankText").GetComponent<Text>().text = rank.ToString();
            entrytransform.Find("ScoreText").GetComponent<Text>().text = highScoresList[i].score.ToString();
            entrytransform.Find("NameText").GetComponent<Text>().text = highScoresList[i].playerName.ToString();
        }
    }

    public void GoToNextScene()
    {
        MenuManager.Instance.SaveData();
        SceneManager.LoadScene(0);
    }
}
