using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;



public enum AIState
{
    Idle,
    Detect,
    Attack
}

public class EnemyAI : MonoBehaviour
{

    private AIState state;
    private NavMeshAgent agent;
    private Animator animator;
    private NavMeshSurface surface;
    private EnemyObject enemyObject;
    private float lastAttackTime;


    [Header("AI")]
    [Range(0f, 100f)][SerializeField] private float DetectingDistance;
    [Range(0f, 100f)][SerializeField] private float AttackDistance;
    private float playerDistance;
    Player Player; //플레이어로 바꿀꺼임


    private void Awake()
    {
        Player = FindAnyObjectByType<Player>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyObject = GetComponent<EnemyObject>();
        

    }
    private void Start()
    {
        agent.speed = enemyObject.GetEnemyInfo().Speed;
        surface = GetComponentInParent<NavMeshSurface>();
        state = AIState.Detect;
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, Player.transform.position);
        animator.SetBool("IsMoving", state != AIState.Idle);
        
        StateAction();

    }


    private void StateAction()
    {
        switch (state) 
        {
            case AIState.Idle:
            case AIState.Detect:
                DetectingPlayer();
                break;
            case AIState.Attack:
                Attacking();
                break;
        }
    }
   

    private void DetectingPlayer()
    {
        if(playerDistance < DetectingDistance) 
        {
            SetState(AIState.Attack);
        }
        else
        {
            if(AIState.Detect == state &&agent.gameObject.activeInHierarchy && agent.remainingDistance<0.1f)
            {
                SetState(AIState.Idle);
                Invoke("StartDetect", 0f);
            }
        }
    }

    private void SetState(AIState WonderState)
    {
        state = WonderState;

        if (this.gameObject.activeInHierarchy)
        {
            switch (state)
            {
                case AIState.Idle:
                    agent.isStopped = true;
                    break;
                case AIState.Detect:
                    agent.isStopped = false;
                    break;
                case AIState.Attack:

                    agent.isStopped = false;
                    break;
            }
        }

        //animator.speed = agent.speed / 10;
    }


    private void StartDetect()
    {
        if(state != AIState.Idle)
        {
            return;
        }
        SetState(AIState.Detect);
        FindNewLocation();

    }

    private void FindNewLocation()
    {
        NavMeshHit hit;
        if (surface != null)
        {
            Debug.Log("Find");
            Vector3 RandomPosition = new Vector3(Random.Range(surface.transform.position.x, surface.transform.position.x + surface.size.x),
                transform.position.y, Random.Range(surface.transform.position.z, surface.transform.position.z + surface.size.z));
            NavMesh.SamplePosition(RandomPosition, out hit, 100f, NavMesh.AllAreas);
            //Debug.Log(hit.position);

            if(transform.gameObject.activeInHierarchy)
            agent.SetDestination(hit.position);
        }
        else
        {
            Debug.Log("Surface is null");
        }
    }

    private void Attacking()
    {
        Debug.Log("AttackIng");
        if (AttackDistance > playerDistance && Sight())
        {
            agent.isStopped = true;
            if( Time.time - lastAttackTime  > enemyObject.GetEnemyInfo().AttackCoolTime )
            {
                lastAttackTime = Time.time;
                animator.SetTrigger("IsAttack");
                Debug.Log("AttackComplete");
            }
            else
            {
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
            Debug.Log("Attack Fail");
            if (DetectingDistance > playerDistance) 
            {
                Debug.Log("In Range");
                agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();
                if(agent.CalculatePath(Player.transform.position, path))
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

    private bool Sight()
    {
        Vector3 Dir = (Player.transform.position - transform.position).normalized;
        float dot = Vector3.Dot(Dir, transform.forward);
        float Sightcos  = Mathf.Cos(enemyObject.GetEnemyInfo().Sight * 0.5f * Mathf.Deg2Rad);
        return Sightcos < dot;
    }

    private void Die()
    {
        if(enemyObject.SubHealth(1) <= 0f)
        {
            animator.SetTrigger("DIE");
        }
    }

}
