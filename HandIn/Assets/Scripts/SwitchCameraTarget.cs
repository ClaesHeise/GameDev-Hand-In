using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraTarget : MonoBehaviour
{

  // Start is called before the first frame update
  void Start()
  {

  }

 public void SwitchTarget(GameObject target, Vector3 offset)
  {
    if (target.CompareTag("Player"))
    {
      transform.parent = target.transform;
    }
    else
    {
      transform.parent = null;
    }
    Vector3 worldOffset  = target.transform.localToWorldMatrix.MultiplyPoint(offset);
    Vector3 targetPos = worldOffset;
    StartCoroutine(Switch(targetPos, target.transform.rotation));
  }

  IEnumerator Switch(Vector3 targetPos, Quaternion targetRot)
  {
    Vector3 currentPos = transform.position;
    Quaternion currentRot = transform.rotation;
    float t = 0;
    while (t < 1)
    {
      t += Time.deltaTime;
      transform.position = Vector3.Lerp(currentPos, targetPos, t);
      transform.rotation = Quaternion.Lerp(currentRot, targetRot, t);
      yield return null;
    }

  }


}
