using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Patrol : MonoBehaviour
{
    public Transform[] points;
    public float turnSpeed = 5f;   // ne kadar h�zl� d�nece�i

    private NavMeshAgent agent;
    private Animator animator;
    private int idx = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Agent d�n���n� biz halledelim:
        agent.updateRotation = false;
        agent.updatePosition = true;

        if (points == null || points.Length == 0)
        {
            Debug.LogWarning("Patrol: points dizisi bo�!");
            enabled = false;
            return;
        }
        agent.SetDestination(points[idx].position);
    }

    void Update()
    {
        // Hedefe var�nca s�radaki noktaya ge�
        if (!agent.pathPending && agent.remainingDistance < 0.2f)
        {
            idx = (idx + 1) % points.Length;
            agent.SetDestination(points[idx].position);
        }

        // Hareket y�n�n� ve h�z� al
        Vector3 vel = agent.velocity;
        vel.y = 0;

        bool isWalking = vel.sqrMagnitude > 0.01f;
        animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            // Hedef rotasyonu hesaplarken normalize ederek
            Quaternion targetRot = Quaternion.LookRotation(vel);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                turnSpeed * Time.deltaTime
            );
        }
    }
}
