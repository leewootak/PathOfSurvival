using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostItHave : MonoBehaviour
{
    public PostIt postit;

    private void Start()
    {
        UIManager.Instance.StoryUI.AllPostit.Add(postit);
    }

    public void OnInteraction()
    {
        UIManager.Instance.StoryUI.AllPostit.Remove(postit);
        postit.isPlayerHave = true;
        UIManager.Instance.StoryUI.AllPostit.Add(postit);
        Destroy(gameObject);
    }
}
