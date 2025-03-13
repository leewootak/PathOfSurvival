using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneResource : MonoBehaviour
{
    Vector3 Scale;
    Collider Collider;
    GameObject Particle;

    private void Start()
    {
        MapManager.Instance.Resource.resourceSpawn.StoneSpawnPosition.Add(this);
        Particle = Instantiate(MapManager.Instance.Resource.resourceSpawn.ResourceParticle);
        Particle.transform.SetParent(transform, true);
        Particle.transform.localPosition = Vector3.zero;

        Scale = gameObject.transform.localScale;
        Collider = gameObject.GetComponent<Collider>();
    }

    public void SpawnResource()
    {
        gameObject.transform.localScale = Scale;
        Collider.enabled = true;
        Particle.SetActive(true);
    }

    public void DeleteResource()
    {
        gameObject.transform.localScale = Vector3.zero;
        Collider.enabled = false;
        Particle.SetActive(false);
    }
}
