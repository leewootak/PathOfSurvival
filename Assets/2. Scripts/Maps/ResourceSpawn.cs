using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawn : MonoBehaviour
{
    public List<StoneResource> StoneSpawnPosition;
    public List<WoodResource> WoodSpawnPosition;

    public GameObject ResourceParticle;

    [Range(0f, 1f)] public float SpawnPercentage;

    int HowManyStoneSpawn;
    int HowManyWoodSpawn;

    public GameObject Branch;
    public GameObject Stone;

    private void Start()
    {
        Invoke("LateStart", 0.1f);
    }

    void LateStart()
    {
        ChangeSpawnPercentage();
    }

    public void ChangeSpawnPercentage()
    {
        HowManyStoneSpawn = Mathf.RoundToInt(SpawnPercentage * StoneSpawnPosition.Count);
        HowManyWoodSpawn = Mathf.RoundToInt(SpawnPercentage * WoodSpawnPosition.Count);
    }

    public void SpawnResource()
    {
        BreakAllResource();

        int[] StoneSpawnNum = new int[StoneSpawnPosition.Count];
        int[] WoodSpawnNum = new int[WoodSpawnPosition.Count];

        for (int i = 0; i < StoneSpawnPosition.Count; i++)
        {
            StoneSpawnNum[i] = i;
        }
        for (int i = 0;i < WoodSpawnPosition.Count; i++)
        {
            WoodSpawnNum[i] = i;
        }

        for (int i = 0; i < HowManyStoneSpawn; i++)
        {
            int random = Random.Range(i, StoneSpawnPosition.Count);

            StoneSpawnPosition[random].SpawnResource();

            StoneSpawnNum[random] = StoneSpawnNum[i];
        }
        for (int i = 0; i < HowManyWoodSpawn; i++)
        {
            int random = Random.Range(i, WoodSpawnPosition.Count);

            WoodSpawnPosition[random].SpawnResource();

            WoodSpawnNum[random] = WoodSpawnNum[i];
        }
    }

    void BreakAllResource()
    {
        foreach (StoneResource stone in StoneSpawnPosition)
        {
            stone.DeleteResource();
        }
        foreach (WoodResource wood in WoodSpawnPosition)
        {
            wood.DeleteResource();
        }
    }
}
