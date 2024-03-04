using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Text = TMPro.TextMeshProUGUI;

public class ScoreboardController : MonoBehaviour
{
    public ScoreDatabase scoreboard;
    public GameObject rowPrefab;
    public List<GameObject> rows = new List<GameObject>();

    [SerializeField] private Text usenameText;

    public float currentBestLapTime;
    public float currentTrackTime;
    void Awake()
    {
        scoreboard = LoadFromJson();

        foreach (ScoreData score in scoreboard.scores)
        {
            GameObject row = Instantiate(rowPrefab, transform);
            if (row.GetComponent<RowController>() == null) { return; }
            row.GetComponent<RowController>().SetTexts(score.username, ConvertTimeToString(score.bestlap), ConvertTimeToString(score.time));
            rows.Add(row);
        }
    }

    public void Button()
    {
        CreateNewScore(usenameText.text, currentBestLapTime, currentTrackTime);
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
        if (IsHighScore(score.time))
        {


            scoreboard = LoadFromJson();

            scoreboard.scores.Add(score);

            // Sort the list based on scores (descending order)
            scoreboard.scores.Sort((x, y) => y.time.CompareTo(x.time));

            // If the list exceeds the maximum number of scores, remove the lowest score
            if (scoreboard.scores.Count > 5)
            {
                scoreboard.scores.RemoveAt(scoreboard.scores.Count - 1);
            }

            // Save the updated scores to the JSON file
            SaveToJson(scoreboard);
        }

        UpdateScoreboard();
    }

    public void UpdateScoreboard()
    {

        ClearScoreboard();

        scoreboard = LoadFromJson();

        foreach (ScoreData score in scoreboard.scores)
        {
            GameObject row = Instantiate(rowPrefab, transform);
            if (row.GetComponent<RowController>() == null) { return; }
            row.GetComponent<RowController>().SetTexts(score.username, ConvertTimeToString(score.bestlap), ConvertTimeToString(score.time));
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

    public bool IsHighScore(float newScore)
    {
        List<ScoreData> s = scoreboard.scores.ToList<ScoreData>();
        if (scoreboard.scores.Count() < 5)
        {
            return true;
        }
        else
        {
            return newScore > scoreboard.scores[scoreboard.scores.Count() - 1].time;
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
        File.WriteAllText(Application.dataPath + "/score.json", json);
    }

    public ScoreDatabase LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/score.json");
        return JsonUtility.FromJson<ScoreDatabase>(json);
    }
}
