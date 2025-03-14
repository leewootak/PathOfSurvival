using UnityEngine;

public enum ItemType
{
    Resource,
    Equipable,
    Consumable,
    Object
}

public enum ConsumableType
{
    Health,
    Hungry,
    Thirst,
    Disinfection
}

//아이템 데이터 정의
[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public ItemID id;
    public string displayName;
    public string description;
    public ItemType type;
    public GameObject dropPrefab;
    public Sprite icon;
    public float weight;

    [Header("Stack")]
    public bool canStack;
    public int maxStackAmount;

    [Header("GetAmount")]
    public int getAmount;
    
    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Equip")]
    public GameObject equipPrefab;
    public int attackDamage;

    [Header("Object")]
    public bool isSetUP;
}

