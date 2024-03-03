using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = TMPro.TextMeshProUGUI;

public class RowController : MonoBehaviour
{
    [SerializeField] private Text usernameText;
    [SerializeField] private Text bestLapText;
    [SerializeField] private Text totalTimeText;
    

    public void SetTexts(string username, string bestLap, string totalTime)
    {
        usernameText.text = username;
        bestLapText.text = bestLap;
        totalTimeText.text = totalTime;
    }
}
