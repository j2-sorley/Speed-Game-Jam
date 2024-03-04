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

    public event EventHandler OnPlayerCorrectCheckpoint;
    public event EventHandler OnPlayerIncorrectCheckpoint;

    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material clearMaterial;

    //[SerializeField] private SingleCheckpoint nextCheckpoint;

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

    public void PlayerThroughCheckpoint(SingleCheckpoint checkpointSingle)
    {
        //Debug.Log(checkpointSingle.transform.name);
        //Debug.Log(singleCheckpointList.IndexOf(checkpointSingle));
        if (singleCheckpointList.IndexOf(checkpointSingle) == nextCheckpointIndex) 
        {
            nextCheckpointIndex = (nextCheckpointIndex + 1) % singleCheckpointList.Count;
            Debug.Log("Correct");
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
        }
        /*else if (singleCheckpointList.IndexOf(checkpointSingle) == nextCheckpointIndex + 1)
        {
            Debug.Log("Wrong");
            checkpoint.rend.material = redMaterial;
        }*/
        else
        {
            Debug.Log("Wrong");
            OnPlayerIncorrectCheckpoint?.Invoke(this, EventArgs.Empty);
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
    }
}
