using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCheckpoint : MonoBehaviour
{
    private CheckpointManager checkpointManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "New Quad Bike")
        {
            //Debug.Log("Triggered!");
            checkpointManager.PlayerThroughCheckpoint(this);
        }
    }

    public void SetCheckpoints(CheckpointManager checkpointManager)
    {
        this.checkpointManager = checkpointManager;
    }
}
