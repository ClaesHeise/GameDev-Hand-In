using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{

  public float speed = 100f;
  public float rotationSpeed = 100f;

  private Rigidbody rb;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update()
  {
    float translation = Input.GetAxis("Vertical") * speed;
    float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

    rb.AddForce(transform.forward * translation * Time.deltaTime, ForceMode.Impulse);
    rb.AddTorque(transform.up * rotation * Time.deltaTime, ForceMode.Impulse);
  }
}
