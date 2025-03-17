using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private TextMeshProUGUI TalkUI;
    void Start()
    {
        TalkUI = GetComponentInChildren<TextMeshProUGUI>();
    }


    public void TalkUITurnDown()
    {
        TalkUI.gameObject.SetActive(false);
    }
}
