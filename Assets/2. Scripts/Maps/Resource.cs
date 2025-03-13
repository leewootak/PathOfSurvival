using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ResourceSpawn resourceSpawn;

    private void Awake()
    {
        MapManager.Instance.Resource = this;
        resourceSpawn = gameObject.GetComponent<ResourceSpawn>();
    }
}
