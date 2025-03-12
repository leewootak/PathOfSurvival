using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawn : MonoBehaviour
{
    public GameObject Stone;
    public GameObject Wood;

    public List<GameObject> OnFieldStoneList;
    public List<GameObject> OnFieldWoodList;

    public List<Vector3> StoneSpawnPosition;
    public List<Vector3> WoodSpawnPosition;

    [Range(0f, 1f)] public float SpawnPercentage;

    int HowManyStoneSpawn;
    int HowManyWoodSpawn;

    private void Start()
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

            OnFieldStoneList.Add(Instantiate(Stone));
            OnFieldStoneList[i].transform.position = StoneSpawnPosition[random];

            StoneSpawnNum[random] = StoneSpawnNum[i];
        }
        for (int i = 0; i < HowManyWoodSpawn; i++)
        {
            int random = Random.Range(i, WoodSpawnPosition.Count);

            OnFieldWoodList.Add(Instantiate(Wood));
            OnFieldWoodList[i].transform.position = WoodSpawnPosition[random];

            WoodSpawnNum[random] = WoodSpawnNum[i];
        }
    }

    void BreakAllResource()
    {
        foreach (GameObject stone in OnFieldStoneList)
        {
            Destroy(stone);
            OnFieldStoneList.Remove(stone);
        }
        foreach (GameObject wood in OnFieldWoodList)
        {
            Destroy(wood);
            OnFieldStoneList.Remove(wood);
        }
    }
}
