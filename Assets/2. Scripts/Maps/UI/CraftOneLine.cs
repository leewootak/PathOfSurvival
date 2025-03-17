using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftOneLine : MonoBehaviour
{
    Button CraftButton;
    public Slot slot;

    public ItemData ItemData;

    public GameObject SlotIcon;
    public Transform SpawnPositionSlotIcon;

    private void Start()
    {
        CraftButton = GetComponent<Button>();

        if (CraftButton != null)
        {
            CraftButton.onClick.AddListener(OnCraftButton);
        }
    }

    public void SetItem()
    {
        //slot.changeImage(ItemData.Image);
        //ItemData가 가지고 있는 재료 수 만큼
        //foreach
    }

    void OnCraftButton()
    {
        //ItemData가 가지고 있는 재료를
        //플레이어 아이템이 존재할 시
        //플레이어 아이템에서 -하고
        //재작템을 플레이어 아이템에 + 한다
    }
}
