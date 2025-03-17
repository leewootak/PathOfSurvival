using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;

    public Button button;
    public Image icon;
    public TextMeshProUGUI quantityText;
    private Outline outline;

    public Inventory inventory;

    public ItemID index;
   

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        outline.enabled = inventory.GetEquiped();
    }

    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        quantityText.text = inventory.GetQuantity() > 1 ? inventory.GetQuantity().ToString() : string.Empty;

        if (outline != null)
        {
            outline.enabled = inventory.GetEquiped();
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
}
