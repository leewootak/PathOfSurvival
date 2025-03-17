using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("UIManager").AddComponent<UIManager>();
            }
            return _instance;
        }
    }

    private MenuUI _menuUI;
    public MenuUI MenuUI
    {
        get { return _menuUI; }
        set { _menuUI = value; }
    }

    private InventoryUI _inventoryUI;
    public InventoryUI InventoryUI
    {
        get { return _inventoryUI; }
        set { _inventoryUI = value; }
    }

    private StoryUI _storyUI;
    public StoryUI StoryUI
    {
        get { return _storyUI; }
        set { _storyUI = value; }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
