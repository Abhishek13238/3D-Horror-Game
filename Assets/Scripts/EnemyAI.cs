using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform[] patrolPoints;
    public Animator anim;

    [Header("Vision Settings")]
    public float sightDistance = 250f;
    public float visionAngle = 280f;

    [Header("Patrol Settings")]
    public float waitTime = 2f;

    [Header("Speed Settings")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 8f;

    enum EnemyState
    {
        Patrol,
        Chase
    }

    EnemyState currentState;

    private NavMeshAgent agent;
    private int currentPoint = 0;

    private float timer = 0f;
    public Sounds sound;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        currentState = EnemyState.Patrol;
        agent.speed = patrolSpeed;

        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPoint].position);
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();

                if (CanSeePlayer())
                {
                    currentState = EnemyState.Chase;
                    sound.StartChase();
                }
                break;

            case EnemyState.Chase:
                agent.speed = chaseSpeed;
                agent.SetDestination(player.position);

                if (!CanSeePlayer())
                {
                    agent.speed = patrolSpeed;
                    currentState = EnemyState.Patrol;
                }
                break;
        }

        // Animation
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    bool CanSeePlayer()
    {
        Vector3 dir = (player.position - transform.position);
        float distance = dir.magnitude;

        if (distance < sightDistance)
        {
            float angle = Vector3.Angle(transform.forward, dir);

            if (angle < visionAngle / 2f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position + Vector3.up, dir.normalized, out hit, distance))
                {
                    if (hit.transform == player)
                    {
                        return true;
                    }
                }
            }
            else
            {
                sound.StopChase();
            }
        }

        return false;
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        agent.speed = patrolSpeed;

        if (!agent.pathPending && agent.remainingDistance <= 0.5f)
        {
            timer += Time.deltaTime;

            if (timer >= waitTime)
            {
                timer = 0f;

                currentPoint = (currentPoint + 1) % patrolPoints.Length;
                agent.SetDestination(patrolPoints[currentPoint].position);
            }
        }

        // safety (agar atak jaye)
        if (!agent.pathPending && agent.velocity.magnitude < 0.1f)
        {
            agent.SetDestination(patrolPoints[currentPoint].position);
        }
    }
}