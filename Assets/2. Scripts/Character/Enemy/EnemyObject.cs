using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    [SerializeField] private EnemyInfo EnemyInfo;

    private float health;
    private float attack;
    public void Set()
    {
        health = EnemyInfo.Health;
        attack = EnemyInfo.Attack;
    }
    public EnemyInfo GetEnemyInfo()
    {
        return EnemyInfo;
    }
    
}
