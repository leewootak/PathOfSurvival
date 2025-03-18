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


    private void Awake()
    {
        outline = GetComponent<Outline>();
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
        inventory.SelectItem(index);
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
