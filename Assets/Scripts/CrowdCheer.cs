using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdCheer : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource Source;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Entered");
        Source.Play();

    }
}
