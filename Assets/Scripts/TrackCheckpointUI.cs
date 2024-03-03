using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class TrackCheckpointUI : MonoBehaviour
{
    [SerializeField] private CheckpointManager checkpointManager;

    private void Start()
    {
        checkpointManager.OnPlayerCorrectCheckpoint += CheckpointManager_OnPlayerCorrectCheckpoint;
        checkpointManager.OnPlayerIncorrectCheckpoint += CheckpointManager_OnPlayerIncorrectCheckpoint;

        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void CheckpointManager_OnPlayerIncorrectCheckpoint(object sender, System.EventArgs e)
    {
        Show();
    }

    private void CheckpointManager_OnPlayerCorrectCheckpoint(object sender, System.EventArgs e)
    {
        Hide();
    }
}
