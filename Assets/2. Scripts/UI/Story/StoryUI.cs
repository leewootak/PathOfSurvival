using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryUI : MonoBehaviour
{
    public GameObject StoryUIGameObject;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Story;

    public List<PostIt> AllPostit;

    private void Awake()
    {
        UIManager.Instance.StoryUI = this;
    }

    public void ShowStory(PostIt _postit)
    {
        Name.text = _postit.NameobStory;
        Story.text = _postit.Story;
        StoryUIGameObject.SetActive(true);
        StartCoroutine(ClickAnyToOff());
    }

    IEnumerator ClickAnyToOff()
    {
        bool isloop = true;

        while (isloop)
        {
            if (Input.anyKeyDown)
            {
                StoryUIGameObject.SetActive(false);
                isloop = false;
            }
            yield return null;
        }
    }
}
