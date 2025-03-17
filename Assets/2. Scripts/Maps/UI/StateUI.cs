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
        HealthText.text = $"{100} / {100}";
        StaminaText.text = $"{100} / {100}";
        FoodText.text = $"{100} / {100}";
        WaterText.text = $"{100} / {100}";
        InfectionText.text = $"{100} / {100}";
    }
}
