using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatObject : MonoBehaviour
{
  private Rigidbody rb;
  public float displacementAmount = 3f;
  public float density = 0.125f;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }


  // Update is called once per frame
  void FixedUpdate()
  {
    rb.AddForceAtPosition(Physics.gravity, transform.position, ForceMode.Acceleration);
    float waveHight = WaveManager.Instance.GetWaveHeight(transform.position);

    if (transform.position.y < waveHight)
    {
      Vector3 uplift = Vector3.up * displacementAmount * density * Mathf.Abs(Physics.gravity.y);

      rb.AddForceAtPosition(uplift, transform.position, ForceMode.Force);
    }

  }
}
