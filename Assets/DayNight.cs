using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public Material Sun;
    public Material Night;
    public Light Directional; 

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = Night; 
        Directional.intensity = 0.5f;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
