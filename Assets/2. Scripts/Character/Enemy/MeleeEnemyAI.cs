using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyAI : EnemyAI
{
    public bool IsAttack;
    public override void Attacking()
    {
        if (AttackDistance > playerDistance && Sight())
        {
            agent.isStopped = true;
            if (Time.time - lastAttackTime > enemyObject.GetEnemyInfo().AttackCoolTime)
            {
                lastAttackTime = Time.time;
                animator.SetTrigger("IsAttack");
                AudioManager.Instance.FXOn(Random.Range(0, 3));
                IsAttack = true;
            }
            else
            {
                IsAttack = false;
                agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(Player.transform.position, path))
                {
                    agent.SetDestination(Player.transform.position);
                    animator.SetBool("IsRun", true);
                    animator.SetBool("IsMoving", false);
                }
                else
                {
                    agent.SetDestination(transform.position);
                    agent.isStopped = true;
                    SetState(AIState.Detect);
                    animator.SetBool("IsRun", false);
                    animator.SetBool("IsMoving", true);
                }
            }
        }
        else
        {
            IsAttack = false;
            if (DetectingDistance > playerDistance)
            {
                agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(Player.transform.position, path))
                {
                    agent.SetDestination(Player.transform.position);
                    animator.SetBool("IsRun", true);
                    animator.SetBool("IsMoving", false);
                }
                else
                {
                    agent.SetDestination(transform.position);
                    agent.isStopped = true;
                    SetState(AIState.Detect);
                    animator.SetBool("IsRun", false);
                    animator.SetBool("IsMoving", true);
                }
            }
            else
            {
                SetState(AIState.Detect);
                animator.SetBool("IsRun", false);
                animator.SetBool("IsMoving", true);
            }
        }
    }


 
}
