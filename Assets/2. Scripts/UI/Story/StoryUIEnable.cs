using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryUIEnable : MonoBehaviour
{
    public GameObject StoryOneLine;
    public Transform SpawnPosition;
    public RectTransform SpawnStoryHeight;

    private void OnEnable()
    {
        foreach (PostIt it in UIManager.Instance.StoryUI.AllPostit)
        {
            if (it.isPlayerHave && !it.isShow)
            {
                SpawnStoryHeight.sizeDelta = new Vector2(SpawnStoryHeight.sizeDelta.x, SpawnStoryHeight.sizeDelta.y + 140);

                GameObject go = Instantiate(StoryOneLine);
                go.transform.parent = SpawnPosition;
                go.GetComponent<StoryOneLine>().postIt = it;
                it.isShow = true;
            }
        }
    }
}
