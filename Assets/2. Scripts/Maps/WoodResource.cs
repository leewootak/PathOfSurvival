using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodResource : MonoBehaviour
{
    Renderer _renderer;
    Collider _collider;
    GameObject Particle;

    private void Start()
    {
        MapManager.Instance.Resource.resourceSpawn.WoodSpawnPosition.Add(this); 
        Particle = Instantiate(MapManager.Instance.Resource.resourceSpawn.ResourceParticle);
        Particle.transform.SetParent(transform, true);
        Particle.transform.localPosition = Vector3.zero;

        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
    }

    public void SpawnResource()
    {
        _collider.enabled = true;
        _renderer.enabled = true;
        Particle.SetActive(true);
    }

    public void DeleteResource()
    {
        _collider.enabled = false;
        _renderer.enabled = false;
        Particle.SetActive(false);
    }
}
