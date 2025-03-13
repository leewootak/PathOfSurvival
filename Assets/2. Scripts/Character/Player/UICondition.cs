using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health; // Condition 이 부착된 오브젝트
    public Condition hunger;
    public Condition Stamina;
    public Condition Thirst;
    public Condition Infection;


    void Start()
    {
        CharacterManager.Instance.Player.condition.uICondition = this;
    }
}
