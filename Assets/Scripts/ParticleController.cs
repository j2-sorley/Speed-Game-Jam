using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> mudParticles;
    [SerializeField] private List<ParticleSystem> rockParticles;
    [SerializeField] private List<ParticleSystem> waterParticles;
    [SerializeField] private List<ParticleSystem> grassParticles;

    private terrainType previousTerrainType;

    public void SetParticle(terrainType terrain)
    {
        //if (previousTerrainType == terrain) return;
        //previousTerrainType = terrain;
        switch (terrain)
        {
            case terrainType.MUD:
                SetActive(mudParticles);
                break;
            case terrainType.ROCK:
                SetActive(rockParticles);
                break;
            case terrainType.SAND:
                SetActive(waterParticles);
                break;
            case terrainType.GRASS:
                SetActive(grassParticles);
                break;
        }
    }

    public void SetActive(List<ParticleSystem> particles)
    {
        if (particles[0].isPlaying && particles[1].isPlaying) { return; }

        ClearParticles();


        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }


    }

    public void ClearParticles()
    {
        foreach (ParticleSystem particle in mudParticles)
        {
            particle.Stop();
        }

        foreach (ParticleSystem particle in rockParticles)
        {
            particle.Stop();
        }
        foreach (ParticleSystem particle in waterParticles)
        {
            particle.Stop();
        }
        foreach (ParticleSystem particle in grassParticles)
        {
            particle.Stop();
        }
    }
}
