using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        if (data.getAmount > 0)
        {
            str += $"\n X {data.getAmount}";
        }
        return str;
    }

    public void OnInteract()
    {
        if (ItemManager.Instance.CompareWeight(data.weight))
        {
            ItemManager.Instance.inventory.GetItem(data);
            Destroy(gameObject);
        }
    }
}