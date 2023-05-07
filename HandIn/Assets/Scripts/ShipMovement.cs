using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{


  [SerializeField]
  private float speedMultiplier = 5f;

  [SerializeField]
  private float rotationSpeed = 2;

  [SerializeField]
  private float maxSpeed = 100f;

  [SerializeField]
  private float smootherRotation = 0.2f;

  [SerializeField]
  private float smootherMovement = 0.2f;

  [SerializeField]
  private float maxBackwardSpeed = 10f;

  private float speed { get; set; }
  private float shipMove;
  private float shipRotate;

  public event Action OnShipMove;

  private MovementInput moveInp;
  private Rigidbody rb;

  void Awake()
  {
    moveInp = new MovementInput();
    moveInp.Ship_Move.Movement.performed += moving => shipMove = moving.ReadValue<float>();
    moveInp.Ship_Move.Rotation.performed += rotating => shipRotate = rotating.ReadValue<float>();

    moveInp.Player_Move.Disable();
    moveInp.Ship_Move.Enable();
  }
  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  void FixedUpdate()
  {
    Move();
    Rotate();
  }
  void Move()
  {
    if (moveInp.Ship_Move.Movement.IsPressed())
      speed += shipMove * smootherMovement * speedMultiplier;

    speed = Mathf.Clamp(speed, -maxBackwardSpeed, maxSpeed);

    if (!(speed < 0.1f && speed > -0.1f))
      OnShipMove?.Invoke();

    Vector3 movement = transform.forward * speed;
    if (rb.velocity.magnitude < maxSpeed || rb.velocity.magnitude > -maxBackwardSpeed)
      rb.AddForce(movement);
  }

  void Rotate()
  {
    if (!moveInp.Ship_Move.Rotation.IsPressed())
      shipRotate -= shipRotate * smootherRotation;

    rb.AddTorque(transform.up * shipRotate * rotationSpeed);
  }

  void OnEnable()
  {
    moveInp.Ship_Move.Enable();
  }

  void OnDisable()
  {
    moveInp.Ship_Move.Disable();
  }
}
