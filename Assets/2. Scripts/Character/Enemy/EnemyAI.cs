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

public abstract class EnemyAI : MonoBehaviour
{

    protected AIState state;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected NavMeshSurface surface;
    public EnemyObject enemyObject;
    protected float lastAttackTime;


    [Header("AI")]
    [Range(0f, 100f)][SerializeField] protected float DetectingDistance;
    [Range(0f, 100f)][SerializeField] protected float AttackDistance;
    protected float playerDistance;
    protected Player Player; //플레이어로 바꿀꺼임


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
        if(surface == null)
        {
            Debug.Log("Null");
        }
        state = AIState.Detect;
        Debug.Log(state);
    }

    public virtual void Update()
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

    public abstract void Attacking();
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

    protected void SetState(AIState WonderState)
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

    protected void FindNewLocation()
    {
        NavMeshHit hit;
        if (surface != null)
        {
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
    protected bool Sight()
    {
        Vector3 Dir = (Player.transform.position - transform.position).normalized;
        float dot = Vector3.Dot(Dir, transform.forward);
        float Sightcos  = Mathf.Cos(enemyObject.GetEnemyInfo().Sight * 0.5f * Mathf.Deg2Rad);
        return Sightcos < dot;
    } 

}
