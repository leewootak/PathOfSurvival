using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    normal,
    Other
}


[CreateAssetMenu(fileName = "Enemy", menuName ="WWH/Zombie")]
public class EnemyInfo : ScriptableObject
{
    public float Health;
    public float Speed;
    public float Attack;
    public float Sight;
    public float AttackCoolTime;
    public EnemyType type;
    public bool IsHaveSpecialSkill;
    
}
