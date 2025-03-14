using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InventoryUIButton
{
    State,
    Craft
}

public class InventoryUI : MonoBehaviour
{
    public GameObject InventoryUIGameObject;

    public GameObject Slot;
    public Transform Contents;

    InventoryUIButton nowUI;

    public GameObject StateUI;
    public Button StateButton;
    public GameObject StateButtonIsOn;

    public GameObject CraftUI;
    public Button CraftButton;
    public GameObject CraftButtonIsOn;

    public Button ExitButton;

    private void Awake()
    {
        UIManager.Instance.InventoryUI = this;
    }

    private void Start()
    {
        if (StateButton != null)
        {
            StateButton.onClick.AddListener(OnClickStateButton);
        }
        if (CraftButton != null)
        {
            CraftButton.onClick.AddListener(OnClickCraftButton);
        }
        if (ExitButton != null)
        {
            ExitButton.onClick.AddListener(OnClickExitButton);
        }
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

    void OnClickStateButton()
    {
        nowUI = InventoryUIButton.State;
        ChangeStateAndUI();
    }

    void OnClickCraftButton()
    {
        nowUI = InventoryUIButton.Craft;
        ChangeStateAndUI();
    }

    public void OnOffInventoryUI()
    {
        InventoryUIGameObject.SetActive(!InventoryUIGameObject.activeSelf);
    }

    void OnClickExitButton()
    {
        OnOffInventoryUI();
    }
}
