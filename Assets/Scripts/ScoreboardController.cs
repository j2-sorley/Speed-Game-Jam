using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    public void Button()
    {
        //CreateNewScore();
    }

    public void CreateNewScore(string user, float bestLap, float totalTime)
    {
        ScoreData score = new ScoreData();
        score.username = user;
        score.bestlap = bestLap;
        score.time = totalTime;


        AddNewScore(score);
    }

    public void AddNewScore(ScoreData score)
    {
        

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

    public string ConvertTimeToString(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SaveToJson(ScoreDatabase score)
    {
        string json = JsonUtility.ToJson(score, true);
        File.WriteAllText(Application.dataPath + "/ScoreboardData.json", json);
    }

    public void LoadFromJson(ScoreDatabase score)
    {

    }
}
