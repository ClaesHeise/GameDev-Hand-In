using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Floater))]
public class BoatController : MonoBehaviour
{
  public Vector3 COM;

  [Space(10)]
  public float speed = 1f;
  public float steerSpeed = 1f;
  public float movementThreshold = 10f;

  private Transform m_COM;
  private Rigidbody rb;

  private float movementFactor;

  private float steerFactor;
  private float verticalInput;
  private float horizontalInput;


  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update()
  {
    Balance();
    Movement();
    Steer();
  }

  void Balance()
  {
    if (!m_COM)
    {
      m_COM = new GameObject("COM").transform;
      m_COM.SetParent(transform);
    }

    m_COM.position = COM;
    rb.centerOfMass = m_COM.position;
  }

  void Movement()
  {
    verticalInput = Input.GetAxis("Vertical");
    movementFactor = Mathf.Lerp(movementFactor, verticalInput, Time.deltaTime / movementThreshold);
    transform.Translate(0, 0, movementFactor * speed);
  }

  void Steer()
  {
    horizontalInput = Input.GetAxis("Horizontal");
    steerFactor = Mathf.Lerp(steerFactor, horizontalInput, Time.deltaTime / movementThreshold);
    transform.Rotate(0, steerFactor * steerSpeed, 0);
    // rb.AddTorque(transform.up * horizontalInput * steerSpeed, ForceMode.Impulse);

  }
}
