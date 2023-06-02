using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
  [SerializeField]
  private float speed;
  [SerializeField]
  private float rotateSpeed;

  private Rigidbody rb;
  private Vector3 moveInput;

  private MovementInput moveInp;
  private bool keyIsHeld;
  private bool animationIsPlaying;

  Animator animator;
  // Start is called before the first frame update
  void Awake()
  {
    moveInp = new MovementInput();
    moveInp.Player_Move.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector3>();

    // moveInp.Player_Move.triggered += _ => AnimatePlayer();

    // animator = this.GetComponent<Animator>();

    rb = GetComponent<Rigidbody>();
    if (rb is null)
    {
      Debug.Log("Rigidbody is NULL!");
    }
  }

  // Update is called once per frame
  void Update()
  {
    var keyboard = Keyboard.current;
    if (keyboard == null)
    {
      return;
    }

    RotatePlayer();

  }

  private void FixedUpdate()
  {
    Move();
  }

  // private void AnimatePlayer(){

  //     else {
  //         animator.SetInteger("state", 0);
  //     }
  // }

  private void RotatePlayer()
  {
    float rotateDirection = moveInp.Player_Move.Rotate.ReadValue<float>();
    transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed * rotateDirection);
  }

  private void Move()
  {
    // Movement
    var movement = transform.TransformDirection(moveInput.normalized);
    if (movement.x > 0 || movement.z > 0)
    {
      // animator.SetInteger("state", 1);
    }
    else if (movement.x < 0 || movement.z < 0)
    {
      // animator.SetInteger("state", 2);
    }
    else
    {
      // animator.SetInteger("state", 0);
    }
    rb.AddForce(movement * speed);
  }

  void OnEnable()
  {
    moveInp.Player_Move.Enable();
  }

  void OnDisable()
  {
    moveInp.Player_Move.Disable();
  }
}
