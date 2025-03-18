using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryOneLine : MonoBehaviour
{
    public PostIt postIt;
    public TextMeshProUGUI Name;

    private void Start()
    {
        Name.text = postIt.NameobStory;

        gameObject.GetComponent<Button>().onClick.AddListener(OnClicked);
    }

    void OnClicked()
    {
        UIManager.Instance.StoryUI.ShowStory(postIt);
    }
}
