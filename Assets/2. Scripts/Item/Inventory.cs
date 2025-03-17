using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Inventory:MonoBehaviour
{
    public ItemData itemData;
    public int quantity;
    public Transform dropPosition;
    private bool equiped;
    private ItemID curEquipIndex;
    public PlayerCondition playerCondition;

    [Header("Select Item")]
    private ItemData selectedItem;
    private ItemID selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedStatName;
    public TextMeshProUGUI selectedStatValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;


    public Dictionary<ItemID, Inventory> inventory = new Dictionary<ItemID, Inventory>();

    private PlayerController controller;
    private PlayerCondition condition;

    [Header("UI Management")]
    private Dictionary<ItemID,ItemSlot> slots;
    public Transform slotParent; // Vertical Layout Group이 붙어있는 부모
    public GameObject slotPrefab; // 슬롯 프리팹


    public Inventory(ItemData item = null , int qty = 1)
    {
        itemData = item;
        quantity = qty;
    }

    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        slots = new Dictionary<ItemID, ItemSlot>();
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
            CreateItemSlot(item);
        }
        UpdateUI();
    }

    void CreateItemSlot(ItemData item)  
    {
        GameObject newSlot = Instantiate(slotPrefab, slotParent);
        ItemSlot itemSlot = newSlot.GetComponent<ItemSlot>();
        itemSlot.inventory = this;
        itemSlot.index = item.id;
        itemSlot.item = item;
        itemSlot.Set();
        slots.Add(item.id, itemSlot);
    }

    void RemoveItemSlot(ItemID itemId)
    {
         slots[itemId].DeleteSlot();
         slots.Remove(itemId);
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
                {
                    inventory.Remove(item);
                    RemoveItemSlot(item);
                    selectedItemIndex = ItemID.None;
                    selectedItem = null;
                    ClearSelectedItemWindow();
                    UpdateUI();
                }
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

    void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    public void SelectItem(ItemID index)
    {
        if (inventory[index].itemData == null) return;
        selectedItem = inventory[index].itemData;
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.displayName;
        selectedItemDescription.text = selectedItem.description;

        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;
        for (int i = 0; i < selectedItem.consumables.Length; i++)
        {
            selectedStatName.text += selectedItem.consumables[i].type.ToString() + "\n";
            selectedStatValue.text += selectedItem.consumables[i].value.ToString() + "\n";
        }

        useButton.SetActive(selectedItem.type == ItemType.Consumable);
        equipButton.SetActive(selectedItem.type == ItemType.Equipable && !inventory[index].equiped);
        unEquipButton.SetActive(selectedItem.type == ItemType.Equipable && inventory[index].equiped);
        dropButton.SetActive(true);
    }


    void ClearSelectedItemWindow()
    {
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void OnUseButton()
    {
        if (selectedItem.type == ItemType.Consumable)
        {
            for (int i = 0; i < selectedItem.consumables.Length; i++)
            {
                switch (selectedItem.consumables[i].type)
                {
                    case ConsumableType.Health:
                        playerCondition.Heal(selectedItem.consumables[i].value);
                        break;
                    case ConsumableType.Thirst:
                        playerCondition.Eat(selectedItem.consumables[i].value);
                        break;
                    case ConsumableType.Disinfection:
                        playerCondition.vaccine(selectedItem.consumables[i].value);
                        break;
                }
            }
            RemoveItem(selectedItemIndex);
        }
    }

    public void OnDropButton()
    {
        ThrowItem(selectedItem);
        RemoveItem(selectedItemIndex);
    }


    public void OnEquipButton()
    {
        if (inventory[curEquipIndex].equiped)
        {
            UnEquip(curEquipIndex);
        }
        inventory[selectedItemIndex].equiped = true;
        curEquipIndex = selectedItemIndex;
        CharacterManager.Instance.Player.equip.EquipNew(selectedItem);
        UpdateUI();
        SelectItem(selectedItemIndex);
    }

    void UnEquip(ItemID index)
    {
        inventory[index].equiped = false;
        CharacterManager.Instance.Player.equip.UnEquip();
        UpdateUI();

        if (selectedItemIndex == index)
        {
            SelectItem(selectedItemIndex);
        }
    }

    public void OnUnEquipButton()
    {
        UnEquip(selectedItemIndex);
    }

    public bool GetEquiped()
    {
        return equiped;
    }

    public float GetQuantity()
    {
        return quantity;
    }

    void UpdateUI()
    {
        foreach (KeyValuePair<ItemID, Inventory> slot in inventory)
        {
            if (slots.TryGetValue(slot.Key, out ItemSlot itemSlot))
            {
                itemSlot.Set();
            }
            else
            {
                itemSlot.Clear();
            }
        }
    }
}