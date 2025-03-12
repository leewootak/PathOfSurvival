using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    private void Start()
    {
        Debug.Log(data.getAmount);
    }
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        if(data.getAmount > 0)
        {
            str += $"\n{data.getAmount}개";
        }
        return str;
    }

    public void OnInteract()
    {
        //Player 스크립트 먼저 수정
 //       CharacterManager.Instance.Player.itemData = data;
 //       CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
}