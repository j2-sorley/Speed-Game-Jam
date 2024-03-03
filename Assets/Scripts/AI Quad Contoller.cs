using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AIQuadContoller : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] Vector3 cuurentPosition;
    private terrainType currentTerrainType;
    private float speedMultiplier = 1f;

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
        cuurentPosition = positon;
        agent.SetDestination(positon);
        speedMultiplier = Random.Range(0.7f, 1f);
    }

    public void SetSpeed(float speed)
    {
        agent.speed = speed * speedMultiplier;
    }

    public void CheckTerrain()
    {
        RaycastHit hit;
        // Cast a raycast downwards from the player
        if (Physics.Raycast(transform.position - Vector3.forward * 1.2f + Vector3.up * 3f, Vector3.down, out hit))
        {
            Terrain terrain = hit.collider.GetComponent<Terrain>();
            // Check if what we hit is a terrain
            if (terrain != null)
            {
                // Get the terrain's texture at the hit point
                TerrainData terrainData = terrain.terrainData;
                int mapX = (int)((hit.point.x - terrain.transform.position.x) / terrainData.size.x * terrainData.alphamapWidth);
                int mapZ = (int)((hit.point.z - terrain.transform.position.z) / terrainData.size.z * terrainData.alphamapHeight);
                float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);

                // Now, you can check the layers of the terrain texture
                for (int i = 0; i < terrainData.terrainLayers.Length; i++)
                {
                    float textureValue = splatmapData[0, 0, i];
                    // Do something based on the texture value, like checking for a specific threshold

                    if (textureValue > 0.3f)
                    {

                        // Do whatever you need to do with this information
                        switch (terrainData.terrainLayers[i].name)
                        {
                            case "Grass01":
                                SetSpeed(7);
                                currentTerrainType = terrainType.GRASS;

                                break;
                            case "layer_MudMud_Normal2e716cd1dfbd9e5a":
                                SetSpeed(8);
                                currentTerrainType = terrainType.MUD;
                                break;
                            case "layer_RockwallRockwall_Normalb3407d0e55802f81":
                                currentTerrainType = terrainType.ROCK;
                                SetSpeed(10);
                                break;
                            case "layer_Sand_DesertBaseGrass_normals2023054508611406":
                                currentTerrainType = terrainType.SAND;
                                Debug.Log("Sand");
                                SetSpeed(5);
                                break;
                        }
                        GetComponent<ParticleController>().SetParticle(currentTerrainType);

                    }
                }
            }
        }
    }
}
