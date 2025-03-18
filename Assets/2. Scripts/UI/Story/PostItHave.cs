using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostItHave : MonoBehaviour
{
    public PostIt postit;

    private void Start()
    {
        postit.isPlayerHave = false;
        postit.isShow = false;
        UIManager.Instance.StoryUI.AllPostit.Add(postit);
    }

    public void OnInteraction()
    {
        postit.isPlayerHave = true;
        Destroy(gameObject);
    }
}
