using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
  [SerializeField] private float argoRange = 10.0f;
  [SerializeField] private float forgetScale = 2.0f;
  private EnemyMovement movement;

  private NavMeshAgent navMeshAgent;

  private GameObject player;
  private bool isChasing = false;


  // Start is called before the first frame update
  void Start()
  {
    movement = GetComponent<EnemyMovement>();
    navMeshAgent = GetComponent<NavMeshAgent>();
  }

  // Update is called once per frame
  void Update()
  {
    if (player == null)
    {
      player = GameObject.FindGameObjectWithTag("Player");
    }

    shouldChase();

    if (isChasing)
    {
      movement.Chase(player.transform.position);
    }
    else
    {
      movement.Wander();
    }

  }

  private void shouldChase()
  {
    float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    if (distanceToPlayer < argoRange)
    {
      isChasing = true;
    }
    else if (distanceToPlayer > argoRange * forgetScale)
    {
      isChasing = false;
    }
  }
}
