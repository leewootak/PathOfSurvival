using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyAI : EnemyAI
{
    private bool IsAttack;
    public override void Attacking()
    {
        Debug.Log("AttackIng");
        if (AttackDistance > playerDistance && Sight())
        {
            agent.isStopped = true;
            if (Time.time - lastAttackTime > enemyObject.GetEnemyInfo().AttackCoolTime)
            {
                lastAttackTime = Time.time;
                animator.SetTrigger("IsAttack");
                AudioManager.Instance.FXOn(Random.Range(0, 3));
                IsAttack = true;
                Debug.Log("AttackComplete");
            }
            else
            {
                IsAttack = false;
                Debug.Log("AfterDelay");
                agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(Player.transform.position, path))
                {
                    agent.SetDestination(Player.transform.position);
                    animator.SetBool("IsRun", true);
                    animator.SetBool("IsMoving", false);
                    Debug.Log("AfterAttackRun");
                }
                else
                {
                    agent.SetDestination(transform.position);
                    agent.isStopped = true;
                    SetState(AIState.Detect);
                    animator.SetBool("IsRun", false);
                    animator.SetBool("IsMoving", true);
                    Debug.Log("AfterAttackIdle");
                }
            }
        }
        else
        {
            IsAttack = false;
            Debug.Log("Attack Fail");
            if (DetectingDistance > playerDistance)
            {
                Debug.Log("In Range");
                agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(Player.transform.position, path))
                {
                    Debug.Log("yes Way");
                    agent.SetDestination(Player.transform.position);
                    animator.SetBool("IsRun", true);
                    animator.SetBool("IsMoving", false);
                }
                else
                {
                    Debug.Log("No way");
                    agent.SetDestination(transform.position);
                    agent.isStopped = true;
                    SetState(AIState.Detect);
                    animator.SetBool("IsRun", false);
                    animator.SetBool("IsMoving", true);
                }
            }
            else
            {
                Debug.Log("Cant Find");
                SetState(AIState.Detect);
                animator.SetBool("IsRun", false);
                animator.SetBool("IsMoving", true);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && IsAttack)
        {
            StartCoroutine(TakeDamage());
        }
    }

    private IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(1);
        CharacterManager.Instance.Player.condition.TakePhysicalDamage((int)enemyObject.GetEnemyInfo().Attack);
    }
}
