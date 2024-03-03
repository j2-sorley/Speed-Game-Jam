using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Score")]
public class Score : ScriptableObject
{
    public string Username;
    public float bestLapFloat;
    public string bestLapString;
    public float totalTimeFloat;
    public string totalTimeString;
}
