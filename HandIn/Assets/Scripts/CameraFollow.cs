using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  public Transform target;
  public Vector3 offset;
  // Start is called before the first frame update
  void Start()
  {
    offset = transform.position - target.position;
  }

  // Update is called once per frame
  void Update()
  {
    // follow the target position with offset
    transform.position = target.position + offset;


  }
}
