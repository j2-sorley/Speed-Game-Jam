using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour
{
    public bool Enabled;
    public GameObject Menu; 
    void Start()
    {
     Cursor.lockState = CursorLockMode.Locked;
     Time.timeScale = 1;     

    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Enabled)
            {
                ShowMenu();
                Enabled = false;
            }

            else 
            {   
                HideMenu();
                Enabled = true; 
            }
        
        }
        
    }

    void ShowMenu() 
    {
        
        Menu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0; 
    }

    void HideMenu() 
    {
        Time.timeScale = 1; 
        Menu.SetActive(false);
        Cursor.lockState  = CursorLockMode.Locked;
    }
}
