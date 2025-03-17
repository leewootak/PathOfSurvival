using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "StoryTailing")]
public class PostIt : ScriptableObject
{
    public string NameobStory;
    public string Story;
    public bool isPlayerHave = false;
    public bool isShow = false;
}
