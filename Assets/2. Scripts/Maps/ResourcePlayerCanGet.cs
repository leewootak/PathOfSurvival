using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePlayerCanGet : MonoBehaviour
{
    protected GameObject Particle;

    protected virtual void Start()
    {
        Particle = Instantiate(MapManager.Instance.Resource.resourceSpawn.ResourceParticle);
        Particle.transform.SetParent(transform, true);
        Particle.transform.localPosition = Vector3.zero;
    }
}
