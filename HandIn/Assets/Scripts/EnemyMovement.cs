using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
  private Animator animator;
  private NavMeshAgent agent;
  [SerializeField] private float wanderRange = 20.0f;
  [SerializeField] private float wanderSpeed;
  [SerializeField] private float chaseSpeed = 15.0f;
  [SerializeField] private Vector3 targetPosition;
  private Coroutine idleCoroutine;

  private void Start()
  {
    agent = GetComponent<NavMeshAgent>();
    animator = GetComponent<Animator>();
    targetPosition = GetRandomPosition();
    wanderSpeed = agent.speed;
  }

  public void MoveTo(Vector3 targetPosition) => agent.SetDestination(targetPosition);

  public void Wander(bool lostPlayer)
  {
    if (agent.isStopped) return;

    float distanceToTargetPosition = Vector3.Distance(transform.position, targetPosition);
    if (distanceToTargetPosition <= GetStoppingDistance() || lostPlayer)
    {
      animator.Play("Idle");
      idleCoroutine = StartCoroutine(Idle(Random.Range(1F, 4F)));
      return;
    }

    animator.Play("Walking");
    agent.speed = wanderSpeed;
    MoveTo(targetPosition);
  }

  public void Chase(Vector3 targetPosition)
  {
    if (agent.isStopped)
    {
      StopCoroutine(idleCoroutine);
      agent.isStopped = false;
    }

    animator.Play("Running");
    agent.speed = chaseSpeed;
    MoveTo(targetPosition);
  }

  private Vector3 GetRandomPosition()
  {
    Vector3 randomPosition = Random.insideUnitSphere * wanderRange + transform.position;
    NavMeshHit hit;
    NavMesh.SamplePosition(randomPosition, out hit, wanderRange, NavMesh.AllAreas);
    return hit.position;
  }

  public float GetStoppingDistance() => agent.stoppingDistance;

  private IEnumerator Idle(float idleTime)
  {
    agent.isStopped = true;
    yield return new WaitForSeconds(idleTime);

    targetPosition = GetRandomPosition();
    agent.isStopped = false;
  }
}
