using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyMovement m_movement;

    // Start is called before the first frame update
    void Start()
    {
        m_movement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        m_movement.Wander();
    }
}
