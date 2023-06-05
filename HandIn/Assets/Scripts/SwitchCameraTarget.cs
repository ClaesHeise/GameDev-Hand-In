using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OffsetType
{
  World,
  Local,

}

public class SwitchCameraTarget : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  public void SwitchTarget(GameObject target, Vector3 offset, OffsetType offsetType = OffsetType.World)
  {
    if (target.CompareTag("Player"))
    {
      transform.parent = target.transform;
    }
    else
    {
      transform.parent = null;
    }

    Vector3 targetPos = offsetType switch
    {
      OffsetType.World => target.transform.position + offset,
      OffsetType.Local => target.transform.position + target.transform.TransformVector(offset),
      _ => target.transform.position + offset,
    };
    Quaternion targetRot = target.CompareTag("Player") ? target.transform.rotation * Quaternion.Euler(10, 0, 0) : target.transform.rotation * Quaternion.identity;
    transform.localScale = Vector3.one;
    StartCoroutine(Switch(targetPos, targetRot));
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
