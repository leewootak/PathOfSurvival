using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Resource, // 자원
    Equipable, // 장비
    Consumable // 음식
}

public enum ConsumableType
{
    Hunger,
    Health
}

[System.Serializable]
public class TestItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class TestItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    //[Header("Stacking")]
    //public bool canStack;
    //public int maxStackAmount;

    [Header("Consumable")]
    public TestItemDataConsumable[] consumables;
}