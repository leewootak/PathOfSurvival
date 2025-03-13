
using System.Collections.Generic;
using UnityEngine;


public enum ItemID
{
    None = -1,
    [Header("Resources")]
    Arrow,
    Branch,
    Stone,
    [Header("Consumes")]
    Can,
    Water,
    Drug,
    [Header("Equip")]
    Torch,
    Sword,
    Bow,
    Gun,
    [Header("Objects")]
    Bonfire,
}

public class ItemManager : MonoBehaviour
{
    public List<ItemData> list = new List<ItemData>();
    public List<ItemData> inventory = new List<ItemData>();
    private static ItemManager _instance;

    public float maxWeight;
    public float curWeight;

    public static ItemManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject().GetComponent<ItemManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(_instance != this) Destroy(gameObject);
        }
    }
    public void RandomItemSpawn(Transform trans)
    {
        if (transform.childCount == 0)
        {
            int rand = Random.Range(0, list.Count);
            if (list[rand].type == ItemType.Resource)
                list[rand].getAmount = Random.Range(1, 10);
            GameObject child = Instantiate(list[rand].dropPrefab, trans.position + Vector3.up, Quaternion.identity);
            child.transform.SetParent(trans);
        }
    }

    public void WantItemSpawn(Transform trans, ItemID id)
    {
        ItemData wantItem = list.Find(wantItem => wantItem.id == id);
        GameObject child = Instantiate(wantItem.dropPrefab, trans.position + Vector3.up, Quaternion.identity);
        child.transform.SetParent(trans);
    }
}
