using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneResource : ResourcePlayerCanGet
{
    Vector3 Scale;
    Collider Collider;

    protected override void Start()
    {
        base.Start();

        MapManager.Instance.Resource.resourceSpawn.StoneSpawnPosition.Add(this);

        Scale = gameObject.transform.localScale;
        Collider = gameObject.GetComponent<Collider>();
    }

    public void SpawnResource()
    {
        gameObject.transform.localScale = Scale;
        Collider.enabled = true;
        Particle.SetActive(true);
    }

    public override void GetResource()
    {
        GameObject go = Instantiate(MapManager.Instance.Resource.resourceSpawn.Stone);
        go.transform.position = new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y + 2f, transform.position.z + Random.Range(-1f, 1f));
        go.transform.eulerAngles = new Vector3(transform.eulerAngles.x + Random.Range(0, 360f), transform.eulerAngles.y, transform.eulerAngles.z + Random.Range(0, 360f));

        DeleteResource();
    }

    public void DeleteResource()
    {
        gameObject.transform.localScale = Vector3.zero;
        Collider.enabled = false;
        Particle.SetActive(false);
    }
}
