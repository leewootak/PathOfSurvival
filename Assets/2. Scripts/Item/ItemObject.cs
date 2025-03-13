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
        if(data.getAmount > 0)
        {
            str += $"\n X {data.getAmount}";
        }
        return str;
    }

    public void OnInteract()
    {
 //       CharacterManager.Instance.Player.itemData = data;
 //       CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
}