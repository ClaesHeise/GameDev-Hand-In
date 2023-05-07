using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class OrbitCamera : MonoBehaviour
{
  [SerializeField]
  private Transform focus;

  [SerializeField, Range(1f, 100f)]
  private float distance = 30f;

  [SerializeField, Min(0f)]
  private int zoomSensitivity = 5;

  [SerializeField, Min(0f)]
  private float focusRadius = 1f;

  [SerializeField, Range(1f, 360f)]
  private float rotationSpeed = 90f;

  [SerializeField, Range(0f, 1f)]
  private float focusCentering = 0.5f;

  [SerializeField, Range(-89f, 89f)]
  private float minVerticalAngle = -30f, maxVerticalAngle = 60f;

  private MovementInput moveInp;
  private Vector2 input;
  private Vector3 focusPoint;
  private Vector2 orbitAngles = new Vector2(45f, 0f);

  void Awake()
  {
    moveInp = new MovementInput();
    moveInp.Orbit_Camera.RotateHorizontal.performed += rotating => input.y = rotating.ReadValue<float>();
    moveInp.Orbit_Camera.RotateVertical.performed += rotating => input.x = rotating.ReadValue<float>();
    moveInp.Orbit_Camera.Zoom.performed += zooming => distance = Mathf.Clamp(distance - (zooming.ReadValue<float>() * zoomSensitivity), 30f, 100f);
    focusPoint = focus.position;
    transform.localRotation = Quaternion.Euler(orbitAngles);
    OnEnable();
  }

  void LateUpdate()
  {
    UpdateFocusPoint();
    Quaternion lookRotation;
    if (DoManualRotation())
    {
      ConstrainAngles();
      lookRotation = Quaternion.Euler(orbitAngles);
    }
    else
    {
      lookRotation = transform.localRotation;
    }
    Vector3 lookDirection = lookRotation * Vector3.forward;
    Vector3 lookPosition = focusPoint - lookDirection * distance;
    transform.SetPositionAndRotation(lookPosition, lookRotation);
  }
  void UpdateFocusPoint()
  {
    Vector3 targetPoint = focus.position;
    if (focusRadius > 0f)
    {
      float distance = Vector3.Distance(targetPoint, focusPoint);
      float time = 1f;
      if (distance > 0.01f && focusCentering > 0f)
      {
        time = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
      }
      if (distance > focusRadius)
      {
        time = Mathf.Min(time, focusRadius / distance);
      }
      focusPoint = Vector3.Lerp(targetPoint, focusPoint, time);
    }
    else
    {
      focusPoint = targetPoint;
    }
  }

  void OnValidate()
  {
    if (maxVerticalAngle < minVerticalAngle)
    {
      maxVerticalAngle = minVerticalAngle;
    }
  }

  void ConstrainAngles()
  {
    orbitAngles.x =
      Mathf.Clamp(orbitAngles.x, minVerticalAngle, maxVerticalAngle);

    if (orbitAngles.y < 0f)
    {
      orbitAngles.y += 360f;
    }
    else if (orbitAngles.y >= 360f)
    {
      orbitAngles.y -= 360f;
    }
  }
  bool DoManualRotation()
  {
    if (!moveInp.Orbit_Camera.Adjust.IsPressed()) return false;

    return ManualRotation();
  }

  bool ManualRotation()
  {
    const float e = 0.001f;
    if (input.x < -e || input.x > e || input.y < -e || input.y > e)
    {
      orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
      return true;
    }

    return false;
  }

  void OnEnable()
  {
    moveInp.Orbit_Camera.Enable();
  }

  void OnDisable()
  {
    moveInp.Orbit_Camera.Disable();
  }
}
