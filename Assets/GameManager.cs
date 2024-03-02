using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.SocialPlatforms.Impl;
using UnityEditor.VersionControl;
using LootLocker.Admin; 

public class GameManager : MonoBehaviour
{
    void Start()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");

                return;
            }

            Debug.Log("successfully started LootLocker session");
        });

        string memberID = "69";
        int leaderboardID = 20; 
        int score = 1000;

        LootLockerSDKManager.SubmitScore(memberID, score, leaderboardID, (response) =>
        {
            if (response.statusCode == 200)
            {
                
                Debug.Log("Successful");
                
                
            }
            else
            {
                Debug.Log("failed: " + response.errorData);
            }
        });
    }
}