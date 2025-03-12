using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//자판기 설정 및 상호작용
public class ItemSpawner : MonoBehaviour
{
    public ItemData[] data;


    private void Start()
    {
        InvokeRepeating("ItemSpawn", 0f, 3f);
    }
    public void ItemSpawn()
    {
        if (transform.childCount == 0)
        {
            int rand = Random.Range(0, data.Length);
            if (data[rand].type == ItemType.Resource)
                data[rand].getAmount = Random.Range(1, 10);
            GameObject child = Instantiate(data[rand].dropPrefab, transform.position + Vector3.up, Quaternion.identity);
            child.transform.SetParent(transform);
        }
    }
}

