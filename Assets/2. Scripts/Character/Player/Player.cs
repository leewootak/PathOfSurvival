using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this; //CharacterManager에 있는 Player 에 Player 를 넣는다
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}
