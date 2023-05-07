using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Floater : MonoBehaviour
{
  public Rigidbody rb;
  public float depthBeforeSubmerged = 1f;
  public float displacementAmount = 3f;
  public int floaterCount = 1;
  public float waterDrag = 0.99f;
  public float waterAngularDrag = 0.5f;

  private void Start()
  {
    rb.useGravity = false;
  }
  void FixedUpdate()
  {

    rb.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);
    float waterHeight = WaveManager.Instance.GetWaveHeight(transform.position);


    if (transform.position.y < waterHeight)
    {
      float displacementMultiplier = Mathf.Clamp01((waterHeight - transform.position.y) / depthBeforeSubmerged) * displacementAmount;
      rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
      rb.AddForce(displacementMultiplier * -rb.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
      rb.AddTorque(displacementMultiplier * -rb.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
  }
}
