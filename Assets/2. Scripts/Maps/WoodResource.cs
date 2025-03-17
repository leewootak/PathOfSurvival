using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodResource : ResourcePlayerCanGet
{
    Renderer _renderer;
    Collider _collider;

    protected override void Start()
    {
        base.Start();

        MapManager.Instance.Resource.resourceSpawn.WoodSpawnPosition.Add(this); 

        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
    }

    public void SpawnResource()
    {
        _collider.enabled = true;
        _renderer.enabled = true;
        Particle.SetActive(true);
    }

    public void GetResource()
    {
        for(int i = 0; i < Random.Range(1, 4); i++)
        {
            GameObject go = Instantiate(MapManager.Instance.Resource.resourceSpawn.Branch);
            go.transform.position = new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y + 2f, transform.position.z + Random.Range(-1f, 1f));
            go.transform.eulerAngles = new Vector3(transform.eulerAngles.x + Random.Range(0, 360f), transform.eulerAngles.y, transform.eulerAngles.z + Random.Range(0, 360f));
        }

        DeleteResource();
    }

    public void DeleteResource()
    {
        _collider.enabled = false;
        _renderer.enabled = false;
        Particle.SetActive(false);
    }
}
