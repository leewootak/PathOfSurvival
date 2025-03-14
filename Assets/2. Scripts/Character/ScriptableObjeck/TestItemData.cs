using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TestItemDataConsumable
{
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class TestItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    //public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    //[Header("Stacking")]
    //public bool canStack;
    //public int maxStackAmount;

    [Header("Consumable")]
    public TestItemDataConsumable[] consumables;
}