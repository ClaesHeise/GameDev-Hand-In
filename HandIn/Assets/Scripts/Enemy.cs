using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  private GameObject player;
  private EnemyMovement movement;
  [SerializeField] private float targetRange = 10.0f;
  [SerializeField] private float forgetScale = 2.0f;
  private bool isChasing = false;

  // Start is called before the first frame update
  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
    movement = GetComponent<EnemyMovement>();
  }

  // Update is called once per frame
  void Update()
  {
    float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    if (distanceToPlayer < targetRange)
    {
      isChasing = true;
      movement.Chase(player.transform.position);
    }
    else
    {
      movement.Wander(isChasing);
      isChasing = false;
    }
  }

  private bool shouldChase()
  {
    float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    if (distanceToPlayer < targetRange)
    {
      isChasing = true;
    }
    else if (distanceToPlayer > targetRange * forgetScale)
    {
      isChasing = false;
    }
    return isChasing;
  }
}
