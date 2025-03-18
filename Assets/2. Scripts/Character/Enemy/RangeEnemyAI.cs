using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemyAI : EnemyAI
{
    [SerializeField] GameObject Prefabs;
    [SerializeField][Range(0, 100f)] float RunAwayRange;
    private bool ISRunAway = false;


    public override void Update()
    {
        base.Update();
        if (AIState.Attack == state && ISRunAway)
        {
            if (playerDistance >= RunAwayRange)
            {
                agent.isStopped = true;
            }
        }
    }
    public override void Attacking()
    {
        if(playerDistance < AttackDistance && Sight())
        {
            agent.isStopped = true;
            if (Time.time - lastAttackTime > enemyObject.GetEnemyInfo().AttackCoolTime)
            {
                lastAttackTime = Time.time;
              
                StartCoroutine(ProjectileShoot());
                RunAway();
            }
            else
            {
                RunAway();
            }
        }
        else
        {
            agent.isStopped = false;
            SetState(AIState.Detect);
            
        }

    }

    private void RunAway()
    {
        if (playerDistance < RunAwayRange)
        {
            agent.isStopped = false;
            Vector3 RandomPosition = new Vector3(Random.Range(surface.transform.position.x, surface.transform.position.x + surface.size.x),
      transform.position.y, Random.Range(surface.transform.position.z, surface.transform.position.z + surface.size.z));

            agent.SetDestination(RandomPosition);
            ISRunAway = true;
        }
        else
        {
            agent.isStopped = true;
        }
    }

    private IEnumerator ProjectileShoot()
    {
        animator.SetTrigger("IsAttack");
        yield return new WaitForSeconds(1f);
        //공격모션에 따라서 잘하면 코루틴 써야할듯
        Vector3 dir = CharacterManager.Instance.Player.transform.position - transform.position;
        GameObject Pre =  Instantiate(Prefabs,transform);
        Pre.transform.GetChild(0).GetComponent<Rigidbody>().AddForce(dir * 3f, ForceMode.Impulse);
    }
    
}
