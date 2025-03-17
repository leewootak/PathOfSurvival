using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateUI : MonoBehaviour
{
    public Slot slot;

    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI StaminaText;
    public TextMeshProUGUI FoodText;
    public TextMeshProUGUI WaterText;
    public TextMeshProUGUI InfectionText;

    UICondition uICondition;

    private void Start()
    {
        uICondition = CharacterManager.Instance.Player.condition.uICondition;
    }

    private void Update()
    {
        ChangeConditions();
    }

    public void CheckEquipItem()
    {
        //slot에 장착중인 아이템 이미지
    }

    void ChangeConditions()
    {
        HealthText.text = $"{Mathf.FloorToInt(uICondition.health.curValue)} / {uICondition.health.maxValue}";
        StaminaText.text = $"{Mathf.FloorToInt(uICondition.Stamina.curValue)} / {uICondition.Stamina.maxValue}";
        FoodText.text = $"{Mathf.FloorToInt(uICondition.hunger.curValue)} / {uICondition.hunger.maxValue}";
        WaterText.text = $"{Mathf.FloorToInt(uICondition.Thirst.curValue)} / {uICondition.Thirst.maxValue}";
        InfectionText.text = $"{Mathf.FloorToInt(uICondition.Infection.curValue)} / {uICondition.Infection.maxValue}";
    }
}
