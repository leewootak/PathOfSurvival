using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryUIEnable : MonoBehaviour
{
    public GameObject StoryOneLine;
    public Transform SpawnPosition;

    GameObject[] AllLine;

    private void OnEnable()
    {
        foreach (PostIt it in UIManager.Instance.StoryUI.AllPostit)
        {
            if (it.isPlayerHave)
            {
                GameObject go = Instantiate(StoryOneLine);
                go.transform.parent = SpawnPosition;
                go.GetComponent<StoryOneLine>().postIt = it;
            }
        }
    }
}
