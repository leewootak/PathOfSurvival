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
        // 무게 비교가 통과되면 템을 먹고, 그렇지 않으면 템 먹지 않음
        if (ItemManager.Instance.CompareWeight(data.weight))
        {
            // 아이템을 플레이어에게 추가
            CharacterManager.Instance.Player.itemData = data;
            CharacterManager.Instance.Player.addItem?.Invoke();
            Destroy(gameObject);
        }
        else
        {
            // 무게 초과 시, 알림 메시지
            Debug.Log("아이템 무게가 너무 많아 먹을 수 없습니다.");
        }
    }
}