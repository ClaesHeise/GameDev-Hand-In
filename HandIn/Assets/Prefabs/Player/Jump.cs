using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Jump : MonoBehaviour
{
  [SerializeField]
  private float jumpHeight;

  [SerializeField]
  private float gravityScale;

  [SerializeField]
  public bool isGrounded { get; private set; } = true;

  [SerializeField]
  public float groundSphereRadius;

  private Rigidbody rb;

  private MovementInput moveInp;

  private bool jump;



  Animator animator;

  void Awake()
  {
    moveInp = new MovementInput();
    moveInp.Player_Move.Jump.performed += _ => jump = true;

    // animator = this.GetComponent<Animator>();
  }

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    if (rb is null)
    {
      Debug.Log("Rigidbody is NULL!");
    }
  }

  // Update is called once per frame
  void Update()
  {
    isGrounded = GroundCheck();
  }

  private void FixedUpdate()
  {
    // add gravity always
    rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
    if (isGrounded && jump)
    {
      float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics.gravity.y * gravityScale));
      // animator.SetInteger("state", 3);
      rb.AddForce(Vector3.up * jumpForce * rb.mass, ForceMode.Impulse);
    }
    jump = false;
  }

  void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawSphere(transform.position, groundSphereRadius);
  }
  bool GroundCheck()
  {
    return Physics.OverlapSphere(transform.position, groundSphereRadius).Length > 1;
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
