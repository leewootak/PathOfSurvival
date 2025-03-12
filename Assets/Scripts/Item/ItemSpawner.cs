using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//자판기 설정 및 상호작용
public class VendingMachine : MonoBehaviour
{
    public ItemData[] data;
    public void ItemSpawn()
    {
        int rand = Random.Range(0, data.Length);
        Instantiate(data[rand].dropPrefab, transform.position + Vector3.up, Quaternion.identity);
    }
}

