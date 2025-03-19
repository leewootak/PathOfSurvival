using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class CraftOneLine : MonoBehaviour
{
    Button CraftButton;
    public Slot slot;

    public ItemData ItemData;

    public GameObject SlotIcon;
    public Transform SpawnPositionSlotIcon;

    private CraftTable  craftTable;

    private void Start()
    {
        CraftButton = GetComponent<Button>();

        if (CraftButton != null)
        {
            CraftButton.onClick.AddListener(OnCraftButton);
        }
        craftTable = FindAnyObjectByType<CraftTable>();

    }

    public void SetItem()
    {
        slot.changeImage(ItemData.icon, 1);

        for (int i = 0; i < ItemData.CraftResourceData.Count; i++)
        {
            GameObject go = Instantiate(SlotIcon);
            go.transform.parent = SpawnPositionSlotIcon;
            go.GetComponent<Slot>().changeImage(ItemData.CraftResourceData[i].icon, ItemData.CraftResourceAmount[i]);
        }
    }

    void OnCraftButton()
    {
        //ItemData가 가지고 있는 재료를
        //플레이어 아이템이 존재할 시
        //플레이어 아이템에서 -하고
        //재작템을 플레이어 아이템에 + 한다

        if(ItemData.type == ItemType.Object)
        {
            //Equip하고 있다면 추가로 장착을 해제해야함
            craftTable.prefabs = ItemData.dropPrefab;
            craftTable.enabled = true;
        }
    }


}
