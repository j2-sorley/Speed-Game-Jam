using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Box")
        {
            Debug.Log("Triggered!");
        }
    }
}
