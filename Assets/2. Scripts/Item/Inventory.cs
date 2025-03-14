using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public ItemData itemData;
    public int quantity;

    public Dictionary<int, Inventory> inventory = new Dictionary<int, Inventory>();


    public Inventory(ItemData item = null , int qty = 1)
    {
        itemData = item;
        quantity = qty;
    }

    public void GetItem(ItemData item)
    {
        if (inventory.TryGetValue((int)item.id, out Inventory existingItem))
        {
            existingItem.quantity += item.getAmount;
        }
        else
        {
            inventory[(int)item.id] = new Inventory(item, item.getAmount);
        }
    }

    public void RemoveItem(ItemData item, int quantity)
    {
        if (inventory.TryGetValue((int)item.id, out Inventory existingItem))
        {
            if (existingItem.quantity >= quantity)
            {
                existingItem.quantity -= item.getAmount;
                if (existingItem.quantity == 0)
                    inventory.Remove((int)item.id);
            }
            else
            {
                Debug.LogError("item의 갯수가 버릴 갯수보다 적습니다.");
            }
        }
        else
        {
            Debug.LogError($"Not found item {item.name} in inventory");
        }
    }
}