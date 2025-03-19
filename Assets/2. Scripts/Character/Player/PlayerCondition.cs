using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uICondition;

    Condition health { get { return uICondition.health; } }
    Condition hunger { get { return uICondition.hunger; } }
    Condition stamina { get { return uICondition.Stamina; } }
    Condition Thirst { get { return uICondition.Thirst; } }
    Condition Infection { get { return uICondition.Infection; } }

    public float noHungerHealthDecay; // 등가 교환
    public float noThirstStaminaDecay;
    // internal float curValue;

    public event Action onTakeDamage;

    void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime); // 시간에 따라 줄어듦
        stamina.Add(stamina.passiveValue * Time.deltaTime); // 행동시 줄어듦
        Thirst.Subtract(Thirst.passiveValue * Time.deltaTime); // 시간에 따라 줄어듦
        Infection.Subtract(Infection.passiveValue * Time.deltaTime); // 시간에 따라 줄어듦

        if (hunger.curValue <= 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue == 0f)
        {
            Die();
        }

        //if (Thirst.curValue <= 0f)
        //{
        //    Stamina.Subtract(noThirstStaminaDecay * Time.deltaTime);
        //}

        if (Infection.curValue == 0f)
        {
            Die();
        }
    }

    public void Heal(float amont)
    {
        health.Add(amont);
    }
    public void Eat(float amont)
    {
        hunger.Add(amont);
    }
    public void Water(float amont)
    {
        Thirst.Add(amont);
    }
    public void vaccine(float amont)
    {
        Infection.Add(amont);
    }
    public void Die()
    {
        GameManager.Instance.ChangeScene(NowSceneEnum.End);
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }
    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }
}

