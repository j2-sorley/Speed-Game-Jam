using System.Collections.Generic;

[System.Serializable]
public class ScoreData
{
    public string username;
    public float bestlap;
    public float time;
}

[System.Serializable]
public class ScoreDatabase
{
    public List<ScoreData> scores = new List<ScoreData>();
}
