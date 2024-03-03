using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Scoreboard")]
public class Scoreboard : ScriptableObject
{
    public List<Score> scores;
}
