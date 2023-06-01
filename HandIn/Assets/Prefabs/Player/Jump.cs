using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField]
    private float _jumpHeight;
    [SerializeField]
    private float _gravityScale;
    private Rigidbody _rbody;
    private Vector3 _jumpInput;
    bool isGrounded;

    private MovementInput moveInp;

    Animator animator;

    void Awake()
    {
        moveInp = new MovementInput();

        animator = this.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        _rbody = GetComponent<Rigidbody>();
        if(_rbody is null){
            Debug.Log("Rigidbody is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded == false){
            _rbody.AddForce(Physics.gravity * (_gravityScale - 1) * _rbody.mass);
        }
        var keyboard = Keyboard.current;
        if(keyboard == null){
            return;
        }
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     animator.SetInteger("state", 3);
        // }
        // print(animator.GetInteger());
        if(isGrounded && moveInp.Player_Move.Jump.triggered){
            _rbody.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
            // animator.SetInteger("state", 3);
        }
    }

    private void FixedUpdate() {
        isGrounded = false;    
    }

    void OnEnable() {
        moveInp.Player_Move.Enable();
    }

    void OnDisable() {
        moveInp.Player_Move.Disable();
    }

    private void OnCollisionStay(Collision other) {
        if(other.contacts[0].normal.y >= 0.6f){
        isGrounded = true;
    }
    }
}
