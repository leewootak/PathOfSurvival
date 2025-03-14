using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneResource : MonoBehaviour
{
    Vector3 Scale;
    Collider Collider;

    private void Start()
    {
        MapManager.Instance.Resource.resourceSpawn.StoneSpawnPosition.Add(this);
        Scale = gameObject.transform.localScale;
        Collider = gameObject.GetComponent<Collider>();
    }

    public void SpawnResource()
    {
        gameObject.transform.localScale = Scale;
        Collider.enabled = true;
    }

    public void DeleteResource()
    {
        gameObject.transform.localScale = Vector3.zero;
        Collider.enabled = false;
    }
}
