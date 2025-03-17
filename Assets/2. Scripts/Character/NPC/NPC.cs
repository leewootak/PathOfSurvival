using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private GameObject[] interactObject;

    private void Start()
    {
        interactObject = GetComponentsInChildren<GameObject>();
    }


    public void TalkToMeTextDown()
    {
        interactObject[1].gameObject.SetActive(false);
    }
    public  void DescriptionTextDown()
    {
        interactObject[0].gameObject.SetActive(false);
    }
    public void DescriptionTextOn()
    {
        interactObject[0].gameObject.SetActive(true);
    }
    public void FriendsAvataDown()
    {
        interactObject[2].gameObject.SetActive(false);
    }

}
