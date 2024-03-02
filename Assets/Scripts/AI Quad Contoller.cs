using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AIQuadContoller : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDestination(Vector3 positon)
    {
        agent.SetDestination(positon);
    }

    public void SetSpeed(float speed)
    {
        agent.speed = speed;
    }
}
