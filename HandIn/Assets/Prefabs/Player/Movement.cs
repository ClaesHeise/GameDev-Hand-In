using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotateSpeed;

    private Rigidbody _rbody;
    private Vector3 _moveInput;

    private MovementInput moveInp;
    private bool keyIsHeld;
    private bool animationIsPlaying;

    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        moveInp = new MovementInput();

        // moveInp.Player_Move.triggered += _ => AnimatePlayer();
        
        animator = this.GetComponent<Animator>();

        _rbody = GetComponent<Rigidbody>();
        if(_rbody is null){
            Debug.Log("Rigidbody is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        if(keyboard == null){
            return;
        }
        // if(moveInp.Player_Move.Movement.triggered){
        //     keyIsHeld = true;
        // }
        // else {
        //     keyIsHeld = false;
        // }
        // if(keyIsHeld){
        //     if(!animationIsPlaying){
                if(Input.GetKeyDown(KeyCode.Space)){
                    animator.SetInteger("state", 3);
                }
                else if(Input.GetKey(KeyCode.W)) {
                    animator.SetInteger("state", 1);
                }
                else if(Input.GetKey(KeyCode.S)){
                    animator.SetInteger("state", 2);
                }
        //         animationIsPlaying = true;
        //     }
        // }
        else {
            animator.SetInteger("state", 0);
            // animationIsPlaying = false;
        }
        // if(Input.GetKey(KeyCode.W)){
        //     animator.SetInteger("state", 1);
        // }
        // else{
        //     animator.SetInteger("state", 0);
        // }
        Run();
        RotatePlayer();
        // animator.SetInteger("state", 0);
    }

    // private void AnimatePlayer(){
        
    //     else {
    //         animator.SetInteger("state", 0);
    //     }
    // }

    private void RotatePlayer(){
        float rotateDirection = moveInp.Player_Move.Rotate.ReadValue<float>();
        transform.Rotate(Vector3.up * Time.deltaTime * _rotateSpeed * rotateDirection);
    }

    private void Run(){
        // animator.SetInteger("state", 1);
        _moveInput = moveInp.Player_Move.Movement.ReadValue<Vector3>();
        _rbody.velocity = transform.forward * _moveInput.x * _speed;
        // animator.SetInteger("state", 0);
    }

    void OnEnable() {
        moveInp.Player_Move.Enable();
    }

    void OnDisable() {
        moveInp.Player_Move.Disable();
    }
}
