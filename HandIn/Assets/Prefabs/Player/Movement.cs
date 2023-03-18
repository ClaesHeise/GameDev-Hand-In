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
    // Start is called before the first frame update
    void Awake()
    {
        moveInp = new MovementInput();

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
        Run();
        RotatePlayer();
    }

    private void RotatePlayer(){
        float rotateDirection = moveInp.Player_Move.Rotate.ReadValue<float>();
        transform.Rotate(Vector3.up * Time.deltaTime * _rotateSpeed * rotateDirection);
    }

    private void Run(){
        _moveInput = moveInp.Player_Move.Movement.ReadValue<Vector3>();
        _rbody.velocity = transform.forward * _moveInput.x * _speed;
    }

    void OnEnable() {
        moveInp.Player_Move.Enable();
    }

    void OnDisable() {
        moveInp.Player_Move.Disable();
    }
}
