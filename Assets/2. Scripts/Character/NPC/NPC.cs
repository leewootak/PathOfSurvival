using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject Clear;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Clear.SetActive(true);
        }
    }


}
