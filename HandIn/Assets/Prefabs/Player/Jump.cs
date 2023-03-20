using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField]
    private float _jumpHeight;
    [SerializeField]
    private float _gravityScale;
    private Rigidbody _rbody;
    private Vector3 _jumpInput;
    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
        _rbody = GetComponent<Rigidbody>();
        if(_rbody is null){
            Debug.Log("Rigidbody is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        _rbody.AddForce(Physics.gravity * (_gravityScale - 1) * _rbody.mass);
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            _rbody.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Map"){
            isGrounded = true;
        }    
    }
}
