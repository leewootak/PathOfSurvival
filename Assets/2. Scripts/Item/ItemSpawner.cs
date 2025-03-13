using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//자판기 설정 및 상호작용
public class ItemSpawner : MonoBehaviour
{
    ItemManager manager;

    private void Start()
    {
        manager = ItemManager.Instance;
        StartCoroutine(spawn());
    }
    
    IEnumerator spawn()
    {
        yield return new WaitForSeconds(1f);
        manager.ItemSpawn(transform);
    }
}

