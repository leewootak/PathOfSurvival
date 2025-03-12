using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    [SerializeField] private EnemyInfo EnemyInfo;


    public EnemyInfo GetEnemyInfo()
    {
        return EnemyInfo;
    }
    
}
