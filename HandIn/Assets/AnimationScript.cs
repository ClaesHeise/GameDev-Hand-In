using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            animator.SetInteger("state", 3);
        }
        else if(Input.GetKeyDown(KeyCode.M)){
            animator.SetInteger("state", 4);
        }
        else if(Input.GetKey(KeyCode.W)) {
            animator.SetInteger("state", 1);
        }
        else if(Input.GetKey(KeyCode.S)){
            animator.SetInteger("state", 2);
        }
        else {
            animator.SetInteger("state", 0);
        }
    }
}
