using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public string map;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(map);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        
    }
}
