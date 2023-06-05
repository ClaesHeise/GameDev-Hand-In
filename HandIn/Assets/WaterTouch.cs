using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        touchesTropic();
    }

    private void touchesTropic(){
        if(transform.position.x > 1260 || transform.position.x < 735 || transform.position.z < -1215 || transform.position.z > -690){
            print("XZ");
            // print(transform.position.x);
            // print(transform.position.z);
            transform.position = new Vector3(1137, 6, -990);
             
        }
    }
}
