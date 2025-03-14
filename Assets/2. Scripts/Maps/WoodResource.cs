using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodResource : MonoBehaviour
{
    Renderer _renderer;
    Collider _collider;

    private void Start()
    {
        MapManager.Instance.Resource.resourceSpawn.WoodSpawnPosition.Add(this);
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
    }

    public void SpawnResource()
    {
        _collider.enabled = true;
        _renderer.enabled = true;
    }

    public void DeleteResource()
    {
        _collider.enabled = false;
        _renderer.enabled = false;
    }
}
