using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//자판기 설정 및 상호작용
public class ItemSpawner : MonoBehaviour
{
    ItemManager manager;
    public ItemID wantSpawnID;

    private void Start()
    {
        manager = ItemManager.Instance;
        
    }

    private void Update()
    {
        TestSpawn();
    }

    private void TestSpawn()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (wantSpawnID != ItemID.None)
                manager.WantItemSpawn(transform, wantSpawnID);
            else manager.RandomItemSpawn(transform);
        }
    }
}

