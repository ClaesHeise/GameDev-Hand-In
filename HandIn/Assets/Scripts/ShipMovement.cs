using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{

  public float speed = 10f;
  public float rotationSpeed = 2;

  public float smootherRotation = 0.2f;
  public float smootherMovement = 0.2f;

  private float shipMove;
  private float shipRotate;

  private MovementInput moveInp;
  private Rigidbody rb;

  void Awake()
  {
    moveInp = new MovementInput();
    moveInp.Ship_Move.Movement.performed += moving => shipMove = moving.ReadValue<float>();
    moveInp.Ship_Move.Rotation.performed += rotating => shipRotate = rotating.ReadValue<float>();

    moveInp.Player_Move.Disable();
    moveInp.Ship_Move.Enable();
    Debug.Log("Ship Movement Enabled");
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
    Vector3 movement = transform.forward * shipMove * speed * Time.deltaTime;
    rb.AddForce(movement, ForceMode.VelocityChange);
    if (!moveInp.Ship_Move.Movement.IsPressed())
      shipMove -= shipMove * smootherMovement * Time.deltaTime;
  }

  void Rotate()
  {
    Debug.Log("Rotating" + shipRotate);
    rb.AddTorque(transform.up * shipRotate * rotationSpeed * Time.deltaTime, ForceMode.Impulse);
    if (!moveInp.Ship_Move.Rotation.IsPressed())
      shipRotate -= shipRotate * smootherRotation * Time.deltaTime;

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
