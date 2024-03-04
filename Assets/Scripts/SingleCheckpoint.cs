using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCheckpoint : MonoBehaviour
{
    private CheckpointManager checkpointManager;
    [SerializeField] private SingleCheckpoint nextCheckpoint;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material clearMaterial;
    [SerializeField] private Renderer rend;
    [SerializeField] private bool active;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!active)
            {
                rend.material = redMaterial;
                return;
            }
            checkpointManager.PlayerThroughCheckpoint(this);
            rend.material = clearMaterial;
            nextCheckpoint.rend.material = greenMaterial;
            nextCheckpoint.Activate();
            active = false;
        }

        if (other.tag == "AI")
        {

            if (other.gameObject.GetComponent<AIQuadContoller>() != null)
            {
                nextCheckpoint.CalculateNavigation(other.gameObject.GetComponent<AIQuadContoller>());

            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!active)
            {
                rend.material = clearMaterial;
                return;
            }
        }
        }

        public void SetCheckpoints(CheckpointManager checkpointManager)
    {
        this.checkpointManager = checkpointManager;
    }

    public void SetNextCheckpoint(SingleCheckpoint next)
    {
        nextCheckpoint = next;
    }

    public void CalculateNavigation(AIQuadContoller quad)
    {

        float randX = Random.Range(point1.position.x, point2.position.x);
        float randz = Random.Range(point1.position.z, point2.position.z);

        Vector3 newPoint = new Vector3(randX, transform.position.y, randz);

        quad.SetDestination(newPoint);
    }

    public void Activate()
    {
        active = true;
    }
}
