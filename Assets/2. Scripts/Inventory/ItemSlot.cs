using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;
    public int quantity;
    public bool equiped;

    public Button button;
    public Image icon;
    public TextMeshProUGUI quantityText;
    private Outline outline;

    public Inventory inventory;
    public InventoryUI inventoryUI;

    public ItemID index;
    bool isClick;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        button = GetComponent<Button>();
    }

    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        quantityText.text = quantity > 1 ? quantity.ToString() : string.Empty;

        if (outline != null)
        {
            outline.enabled = equiped;
        }
    }

    public void Clear()
    {
        item = null;
        index = ItemID.None;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
        if (outline != null) outline.enabled = false;
    }

    public void DeleteSlot()
    {
        Destroy(gameObject);
    }

    public void OnClickButton()
    {
        Debug.Log("check");
        inventory.SelectItem(index);
        if (isClick)
        {
            switch (item.type) 
            {
                case ItemType.Consumable:
                    inventory.OnUseButton();
                    break;
                case ItemType.Equipable:
                    inventory.OnEquipButton();
                    break;
                case ItemType.Object:
                    break;
            }

        }
        else isClick = true;

    }

    public float GetQuantity()
    {
        return quantity;
    }
    public bool GetEquiped()
    {
        return equiped;
    }
}
