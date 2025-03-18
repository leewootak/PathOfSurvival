using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryUIGameObject;
    public Transform dropPosition;

    private ItemID curEquipIndex = ItemID.None;

    //public GameObject inventoryWindow;

    [Header("Select Item")]
    private ItemData selectedItem;
    private ItemID selectedItemIndex;
    private TextMeshProUGUI selectedItemName;
    private TextMeshProUGUI selectedItemDescription;
    private TextMeshProUGUI selectedStatName;
    private TextMeshProUGUI selectedStatValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;

    public Dictionary<ItemID, ItemSlot> inventory;

    private PlayerController controller;
    private PlayerCondition condition;

    [Header("UI Management")]
    public Transform slotParent; // Vertical Layout Group이 붙어있는 부모
    public GameObject slotPrefab; // 슬롯 프리팹


    private void Awake()
    {
        inventory = new Dictionary<ItemID, ItemSlot>();
    }

    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        dropPosition = CharacterManager.Instance.Player.dropPosition;

        InventoryUIGameObject = UIManager.Instance.InventoryUI.gameObject;
        InventoryUIGameObject.SetActive(false);

        controller.inventory += Toggle;

        ClearSelectedItemWindow();
    }

    public void GetItem(ItemData item)
    {
        if (!inventory.ContainsKey(item.id))
        {
            CreateItemSlot(item);
        }
        else
        {
            inventory[item.id].quantity += item.getAmount;
        }
        UpdateUI();
    }

    void CreateItemSlot(ItemData item)
    {
        GameObject newSlot = Object.Instantiate(slotPrefab, slotParent);
        ItemSlot itemSlot = newSlot.GetComponent<ItemSlot>();
        itemSlot.inventory = this;
        itemSlot.index = item.id;
        itemSlot.item = item;
        itemSlot.Set();
        if (!inventory.ContainsKey(item.id))
        {
            inventory[item.id] = itemSlot;
            inventory[item.id].quantity = item.getAmount;
        }
    }

    void RemoveItemSlot(ItemID itemId)
    {
        inventory[itemId].DeleteSlot();
        inventory.Remove(itemId);
    }

    public void RemoveItem(ItemID item, int quantity = 1)
    {
        ItemSlot existItem = ItemManager.Instance.inventory.FindItem(item);
        if (existItem != null)
        {
            if (existItem.quantity >= quantity)
            {
                existItem.quantity -= quantity;
                if (existItem.quantity <= 0)
                {
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

    public ItemSlot FindItem(ItemID id)
    {
        ItemSlot findItem;
        return inventory.TryGetValue(id, out findItem) ? findItem : null;
    }

    void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    public void SelectItem(ItemID index)
    {
        if (inventory[index].item == null) return;
        selectedItem = inventory[index].item;
        selectedItemIndex = index;
        /*
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
  */  }


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
                        condition.Heal(selectedItem.consumables[i].value);
                        break;
                    case ConsumableType.Hunger:
                        condition.Eat(selectedItem.consumables[i].value);
                        break;
                    case ConsumableType.Thirst:
                        condition.Water(selectedItem.consumables[i].value);
                        break;
                    case ConsumableType.Disinfection:
                        condition.vaccine(selectedItem.consumables[i].value);
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
        if (curEquipIndex != ItemID.None && inventory[curEquipIndex].equiped)
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

    void UpdateUI()
    {
        foreach (KeyValuePair<ItemID, ItemSlot> slot in inventory)
        {
            if (inventory.TryGetValue(slot.Key, out ItemSlot itemSlot))
            {
                itemSlot.Set();
            }
            else
            {
                itemSlot.Clear();
            }
        }
    }
    public void Toggle()
    {
        if (IsOpen())
        {
            InventoryUIGameObject.SetActive(false);
        }
        else
        {
            InventoryUIGameObject.SetActive(true);
        }
    }
    public bool IsOpen()
    {
        return InventoryUIGameObject.activeInHierarchy;
    }

}