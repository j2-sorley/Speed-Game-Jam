using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Transform checkpointTransform;
    [SerializeField] private List<SingleCheckpoint> singleCheckpointList;
    private int nextCheckpointIndex;

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
    }

    public void PlayerThroughCheckpoint(SingleCheckpoint checkpointSingle)
    {
        //Debug.Log(checkpointSingle.transform.name);
        //Debug.Log(singleCheckpointList.IndexOf(checkpointSingle));
        if (singleCheckpointList.IndexOf(checkpointSingle) == nextCheckpointIndex) 
        {
            nextCheckpointIndex = (nextCheckpointIndex + 1) % singleCheckpointList.Count;
            Debug.Log("Correct");
        }
        else
        {
            Debug.Log("Wrong");
        }
    }
}
