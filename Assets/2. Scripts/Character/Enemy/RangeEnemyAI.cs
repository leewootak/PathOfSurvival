using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemyAI : EnemyAI
{
    [SerializeField] GameObject Prefabs;
    [SerializeField][Range(0, 100f)] float RunAwayRange;

    public override void Attacking()
    {
        if(playerDistance < AttackDistance && Sight())
        {
            agent.isStopped = true;
            if (Time.time - lastAttackTime > enemyObject.GetEnemyInfo().AttackCoolTime)
            {
                lastAttackTime = Time.time;
                animator.SetTrigger("IsAttack");
                Debug.Log("AttackComplete");
                RunAway();
            }
            else
            {
                RunAway();
            }
        }
        else
        {
            SetState(AIState.Detect);
        }

    }

    private void RunAway()
    {
        if (playerDistance < RunAwayRange)
        {
          
        }
        else
        {
            agent.isStopped = true;
        }
    }

    private void ProjectileShoot()
    {

    }
    
}
