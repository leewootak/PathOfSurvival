using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public ItemData itemData;
    public int quantity;

    public Dictionary<ItemID, Inventory> inventory = new Dictionary<ItemID, Inventory>();


    public Inventory(ItemData item = null , int qty = 1)
    {
        itemData = item;
        quantity = qty;
    }

    public void GetItem(ItemData item)
    {
        if (inventory.TryGetValue(item.id, out Inventory existingItem))
        {
            existingItem.quantity += item.getAmount;
        }
        else
        {
            inventory[item.id] = new Inventory(item, item.getAmount);
        }
    }

    public void RemoveItem(ItemData item, int quantity)
    {
        Inventory existItem = ItemManager.Instance.inventory.FindItem(item.id);
        if (existItem != null) {
            if (existItem.quantity >= quantity)
            {
                existItem.quantity -= quantity;
                if (existItem.quantity <= 0)
                    inventory.Remove(item.id);
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

    public void RemoveItem(ItemID item, int quantity = 1)
    {
        Inventory existItem = ItemManager.Instance.inventory.FindItem(item);
        if (existItem != null)
        {
            if (existItem.quantity >= quantity)
            {
                existItem.quantity -= quantity;
                if (existItem.quantity <= 0)
                    inventory.Remove(item);
            }
            else
            {
                Debug.LogError("item의 갯수가 버릴 갯수보다 적습니다.");
            }
        }
        else
        {
            Debug.LogError($"Not found item {item} in inventory");
        }
    }

    public Inventory FindItem(ItemID id)
    {
        Inventory findItem;
        return inventory.TryGetValue(id, out findItem) ? findItem : null;
    }
}