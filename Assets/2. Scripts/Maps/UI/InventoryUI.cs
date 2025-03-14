using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryUIButton
{
    State,
    Craft
}

public class InventoryUI : MonoBehaviour
{
    public GameObject Slot;
    public Transform Contents;

    InventoryUIButton nowUI;

    public GameObject StateUI;
    public GameObject StateButton;
    public GameObject StateButtonIsOn;

    public GameObject CraftUI;
    public GameObject CraftButton;
    public GameObject CraftButtonIsOn;


    private void Awake()
    {
        UIManager.Instance.InventoryUI = this;
    }

    private void OnEnable()
    {
        //가지고 있는 만큼 소환
        //GameObject go = Instantiate(Slot);
        //go.transform.SetParent(Contents);

        nowUI = InventoryUIButton.State;
        ChangeStateAndUI();
    }

    void ChangeStateAndUI()
    {
        switch (nowUI)
        {
            case InventoryUIButton.State:
                StateUI.SetActive(true);
                StateButtonIsOn.SetActive(true);
                CraftUI.SetActive(false);
                CraftButtonIsOn.SetActive(false);
                break;
            case InventoryUIButton.Craft:
                StateUI.SetActive(false);
                StateButtonIsOn.SetActive(false);
                CraftUI.SetActive(true);
                CraftButtonIsOn.SetActive(true);
                break;
        }
    }
}
