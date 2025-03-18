using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEditor.Progress;

public class CraftUI : MonoBehaviour
{
    public List<ItemData> CraftableItem;

    public GameObject CraftOneLine;
    public Transform SpawnCraftPosition;
    public RectTransform SpawnCraftheight;

    private void Start()
    {
        SpawnCraftheight.sizeDelta = new Vector2(SpawnCraftheight.sizeDelta.x, CraftableItem.Count * 140);

        foreach (ItemData item in CraftableItem)
        {
            GameObject cr = Instantiate(CraftOneLine);
            cr.transform.parent = SpawnCraftPosition;
            cr.GetComponent<CraftOneLine>().ItemData = item;
            cr.GetComponent<CraftOneLine>().SetItem();
        }
    }

    public void AddCrafttable(ItemData item)
    {
        CraftableItem.Add(item);
        GameObject cr = Instantiate(CraftOneLine);
        cr.transform.parent = SpawnCraftPosition;
        cr.GetComponent<CraftOneLine>().ItemData = item;
        cr.GetComponent<CraftOneLine>().SetItem();
    }
}
