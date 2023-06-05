using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    Animator animator;

    private NavMeshAgent m_navMeshAgent;

    [SerializeField] private float m_wanderRange = 15.0f;
    [SerializeField] private Vector3 m_targetPosition;

    private void Awake()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
    }

    public void MoveTo(Vector3 targetPosition) => m_navMeshAgent.SetDestination(targetPosition);

    public void Wander()
    {
        if (m_navMeshAgent.isStopped)
        {
            return;
        }

        GameObject player = GameObject.FindWithTag("Player");
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        float distanceToTargetPosition = Vector3.Distance(transform.position, m_targetPosition);
        if (distanceToPlayer > 50)// && distanceToTargetPosition == 0)
        {
            animator.SetInteger("state", 0);
            m_targetPosition = transform.position;
            // m_targetPosition = GetRandomPosition();
            // StartCoroutine(Idle(Random.Range(1F, 4F)));
        }
        // else if(distanceToTargetPosition < GetStoppingDistance()){

        //     m_targetPosition = GetRandomPosition();
        //     StartCoroutine(Idle(Random.Range(1F, 4F)));
        // }
        else if(distanceToPlayer < 50 && distanceToPlayer > 5) {
            animator.SetInteger("state", 1);
            m_targetPosition = player.transform.position;
        }
        else if(distanceToPlayer < 5){
            print("Dealt 20 damage");
        }
        MoveTo(m_targetPosition);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = Random.insideUnitSphere * (m_wanderRange * 1.25f) + transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPosition, out hit, (m_wanderRange * 1.25f), NavMesh.AllAreas);
        return hit.position;
    }

    public float GetStoppingDistance() => m_navMeshAgent.stoppingDistance;

    private IEnumerator Idle(float idleTime)
    {
        m_navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(idleTime);
        m_navMeshAgent.isStopped = false;
    }
}
