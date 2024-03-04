[System.Serializable]
public class ScoreData
{
    public string username;
    public float bestlap;
    public float time;
}

public class ScoreDatabase
{
    public ScoreData[] scores = new ScoreData[5];
}
