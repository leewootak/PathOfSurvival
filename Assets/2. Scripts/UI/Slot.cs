using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image Itemicon;
    public TextMeshProUGUI NumObItems;

    public void changeImage(Sprite image)
    {
        Itemicon.sprite = image;
        //NumObItems.text = image.quantity;
    }

    public void InventorySlot()
    {
        Button button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(Inventorybutton);
    }

    void Inventorybutton()
    {
        //아이템 데이터 타입에 따른 효과
        //consumable면 그에 맞는 스탯 변경
        //장착,설치 가능시 장착하고 있는 물건 제거 및 장착
        //재료면 return
    }

    public void EquipSlot()
    {
        Button button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(Equipbutton);
    }

    void Equipbutton()
    {
        //장착하고 있는 물건을 제거
    }
}
