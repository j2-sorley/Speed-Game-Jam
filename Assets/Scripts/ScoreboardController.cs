using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreboardController : MonoBehaviour
{
    public Scoreboard scoreboard;
    public GameObject rowPrefab;
    public List<GameObject> rows = new List<GameObject>();
    void Awake()
    {
        foreach (Score score in scoreboard.scores)
        {
            GameObject row = Instantiate(rowPrefab, transform);
            if (row.GetComponent<RowController>() == null) { return; }
            row.GetComponent<RowController>().SetTexts(score.Username, score.bestLapString, score.totalTimeString);
            rows.Add(row);
        }
    }

    public void CreateNewScore(string user, float bestLap, float totalTime)
    {
        Score score = new Score();
        score.Username = user;
        score.bestLapFloat = bestLap;
        score.totalTimeFloat = totalTime;


        string firstNum = Mathf.CeilToInt(bestLap / 60).ToString();
        if (firstNum.Length == 1) { firstNum = "0" + firstNum; }

        string secondNum = Mathf.CeilToInt(bestLap % 60).ToString();
        if (secondNum.Length == 1) { secondNum = "0" + secondNum; }
        score.bestLapString = firstNum + ":" + secondNum;

        firstNum = Mathf.CeilToInt(totalTime / 60).ToString();
        if (firstNum.Length == 1) { firstNum = "0" + firstNum; }

        secondNum = Mathf.CeilToInt(totalTime % 60).ToString();
        if (secondNum.Length == 1) { secondNum = "0" + secondNum; }
        score.totalTimeString = firstNum + ":" + secondNum;

        AddNewScore(score);
    }

    public void AddNewScore(Score score)
    {
        bool scoreadded = false;
        Scoreboard currentScoreboard = scoreboard;
        Scoreboard newScoreboard = scoreboard;
        for (int i = 0; i < currentScoreboard.scores.Count; i++)
        {
            if (!scoreadded)
            {
                if (score.totalTimeFloat < currentScoreboard.scores[i].totalTimeFloat)
                {
                    newScoreboard.scores[i] = score;
                    scoreadded = true;
                }
            }
            else
            {
                newScoreboard.scores[i] = currentScoreboard.scores[i - 1];
            }
        }

        UpdateScoreboard();
    }

    public void UpdateScoreboard()
    {

        ClearScoreboard();

        foreach (Score score in scoreboard.scores)
        {
            GameObject row = Instantiate(rowPrefab, transform);
            if (row.GetComponent<RowController>() == null) { return; }
            row.GetComponent<RowController>().SetTexts(score.Username, score.bestLapString, score.totalTimeString);
            rows.Add(row);
        }
    }

    public void ClearScoreboard()
    {
        foreach (GameObject row in rows)
        {
            Destroy(row);
        }
    }
}
