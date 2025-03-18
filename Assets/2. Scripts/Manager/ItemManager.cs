
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;


public enum ItemID
{
    None = -1,
    [Header("Resources")]
    Arrow,
    Branch,
    Cotton,
    Stone,
    RiverWater,
    Bullet,
    [Header("Consumes")]
    Can = 10,
    Water,
    Drug,
    [Header("Equip")]
    Torch = 20,
    Sword,
    Bow,
    Gun,
    [Header("Objects")]
    Bonfire = 30,
    WoodenWall
}


public class ItemManager : MonoBehaviour
{
    public List<ItemData> list = new List<ItemData>();
    private static ItemManager _instance;
    public Inventory inventory = new Inventory();
    public float maxWeight;
    public float curWeight;

    public static ItemManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject().GetComponent<ItemManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this) Destroy(gameObject);
        }
        list = list.OrderBy(item => (int)item.id).ToList();
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

    public bool CompareWeight(float itemWeight)
    {
        return (itemWeight + curWeight < maxWeight) ? true : false;
    }

    public void LossWeight(float itemWeight)
    {
        curWeight -= itemWeight;
    }
}
