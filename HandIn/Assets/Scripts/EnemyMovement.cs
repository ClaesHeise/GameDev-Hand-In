using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

  Animator animator;

  private NavMeshAgent m_navMeshAgent;

  [SerializeField] private float chaseSpeed = 15.0f;
  [SerializeField] private float m_wanderRange = 15.0f;

  [SerializeField] private Vector3 m_targetPosition;

  private float initialSpeed;

  private void Awake()
  {
    m_navMeshAgent = GetComponent<NavMeshAgent>();
    animator = this.GetComponent<Animator>();
    m_targetPosition = GetRandomPosition();
    initialSpeed = m_navMeshAgent.speed;
  }

  public void MoveTo(Vector3 targetPosition) => m_navMeshAgent.SetDestination(targetPosition);

  public void Wander()
  {
    m_navMeshAgent.speed = initialSpeed;
    if (m_navMeshAgent.isStopped)
    {
      animator.Play("Idle");
      return;
    }

    float distanceToTargetPosition = Vector3.Distance(transform.position, m_targetPosition);
    if (distanceToTargetPosition < GetStoppingDistance())
    {
      m_targetPosition = GetRandomPosition();
      StartCoroutine(Idle(Random.Range(1F, 4F)));
    }

    animator.Play("Walking");
    MoveTo(m_targetPosition);
  }

  public void Chase(Vector3 targetPosition)
  {
    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Running"))
    {
      animator.Play("Base Layer.Running");
    }

    m_navMeshAgent.speed = chaseSpeed;
    MoveTo(targetPosition);
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
