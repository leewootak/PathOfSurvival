using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    [SerializeField] private EnemyInfo EnemyInfo;

    private Animator animator;
    private float health;

    public void Set()
    {
        health = EnemyInfo.Health;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            animator.SetTrigger("DIE");
            gameObject.SetActive(false);
            EnemyPool.Instance.ReQueue(gameObject);
        }
    }
    public EnemyInfo GetEnemyInfo()
    {
        return EnemyInfo;
    }

    public float SubHealth(float Damage)
    {
        return health -= Damage;
    }
    
}
