using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//���Ǳ� ���� �� ��ȣ�ۿ�
public class VendingMachine : MonoBehaviour
{
    public ItemData[] data;
    public void ItemSpawn()
    {
        int rand = Random.Range(0, data.Length);
        Instantiate(data[rand].dropPrefab, transform.position + Vector3.up, Quaternion.identity);
    }
}

