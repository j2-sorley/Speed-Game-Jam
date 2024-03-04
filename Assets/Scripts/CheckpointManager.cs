using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Text = TMPro.TextMeshProUGUI;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Transform checkpointTransform;
    [SerializeField] private List<SingleCheckpoint> singleCheckpointList;
    [SerializeField] private List<AIQuadContoller> aiList;
    [SerializeField] private Text startText;
    [SerializeField] private int nextCheckpointIndex;
    [SerializeField] private int currentLap;
    [SerializeField] private int checkpointSize;

    public event EventHandler OnPlayerCorrectCheckpoint;
    public event EventHandler OnPlayerIncorrectCheckpoint;

    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material clearMaterial;

    [SerializeField] private SingleCheckpoint nextCheckpoint;

    [SerializeField] private GameObject Scoreboard;
    [SerializeField] private GameObject playerui;
    [SerializeField] private Text timeText;
    [SerializeField] private MouseLock mouse;

    private float timer;

    private void Awake()
    {

        singleCheckpointList = new List<SingleCheckpoint>();
        foreach (Transform seperateCheckpoint in checkpointTransform)
        {
            Debug.Log(seperateCheckpoint);
            SingleCheckpoint singleCheckpoint = seperateCheckpoint.GetComponent<SingleCheckpoint>();
            singleCheckpoint.SetCheckpoints(this);
            singleCheckpointList.Add(singleCheckpoint);
        }

        for (int i = 0; i < singleCheckpointList.Count; i++)
        {
            if (i == singleCheckpointList.Count-1)
            {
                singleCheckpointList[i].SetNextCheckpoint(singleCheckpointList[0]);
            }
            else
            {
                singleCheckpointList[i].SetNextCheckpoint(singleCheckpointList[i+1]);
            }

        }
        

        StartCoroutine(StartRace());
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (nextCheckpointIndex >= singleCheckpointList.Count)
        {
            foreach (Transform seperateCheckpoint in checkpointTransform)
            {
                if (!seperateCheckpoint.GetComponent<SingleCheckpoint>().CheckCheckPointMark()) { return; }
                else {  }
            }

            foreach (Transform seperateCheckpoint in checkpointTransform)
            {
                seperateCheckpoint.GetComponent<SingleCheckpoint>().CheckpointMarkedToFalse();
                
            }
            currentLap++;
        }

    }

    public void PlayerThroughCheckpoint(SingleCheckpoint checkpointSingle)
    {
        if (singleCheckpointList.IndexOf(checkpointSingle) == nextCheckpointIndex && checkpointSingle.CheckpointMarkedToTrue()) 
        {
            //nextCheckpointIndex = (nextCheckpointIndex + 1) % singleCheckpointList.Count;
            nextCheckpointIndex++;
            Debug.Log("Correct");
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("Wrong");
            OnPlayerIncorrectCheckpoint?.Invoke(this, EventArgs.Empty);
        }

        if (currentLap >= 2)
        {
            Debug.Log("Race Finished!");
            Scoreboard.SetActive(true);
            playerui.SetActive(false);
            timeText.text = "Total Time: " + Mathf.CeilToInt(timer);
            mouse.enableCursor();
        }

    }

    public void AssignIntialDestinations()
    {
        foreach (AIQuadContoller ai in aiList)
        {
            singleCheckpointList[0].CalculateNavigation(ai);
        }
    }

    private IEnumerator StartRace()
    {
        yield return new WaitForSeconds(1f);
        startText.text = "2";
        yield return new WaitForSeconds(1f);
        startText.text = "1";
        yield return new WaitForSeconds(1f);
        startText.text = "GO!";
        aiList = FindObjectsOfType<AIQuadContoller>().ToList<AIQuadContoller>();
        AssignIntialDestinations();
        timer = 0;
    }

    private void SetToTrue(SingleCheckpoint checkpointSingle)
    {
        if (checkpointSingle.CheckpointMarkedToFalse())
        {
            currentLap++;
            checkpointSingle.CheckpointMarkedToTrue();
        }
    }
}
