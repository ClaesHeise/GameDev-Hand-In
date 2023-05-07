using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DockShip : MonoBehaviour
{
  [SerializeField]
  private Vector3 dockPosition;
  private MovementInput controls;

  private MeshRenderer meshRenderer;


  private void Awake()
  {
    controls = new MovementInput();
  }

  private void Start()
  {
    meshRenderer = GetComponent<MeshRenderer>();
  }
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Ship")
    {
      meshRenderer.enabled = true;
    }
  }

  private void OnTriggerStay(Collider other)
  {
    if (other.gameObject.tag == "Ship" && controls.Ship_Move.Dock.triggered)
    {
      Dock(other.gameObject.GetComponent<Ship>());
    }

  }

  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject.tag == "Ship")
    {
      meshRenderer.enabled = false;
    }
  }

  private void Dock(Ship ship)
  {
    Debug.Log("Docking");
    controls.Ship_Move.Disable();

    ship.transform.rotation = Quaternion.Euler(0, 0, 0);
    var coroutine = StartCoroutine(Move(ship.transform, ship.transform.position, dockPosition, 5f));

    ship.Dock();

    controls.Player_Move.Enable();
  }

  IEnumerator Move(Transform ship, Vector3 beginPos, Vector3 endPos, float duration)
  {
    float t = 0f;
    while (t < duration)
    {
      t += Time.deltaTime;
      ship.position = Vector3.Lerp(beginPos, endPos, t / duration);
      yield return null;
    }
    ship.position = endPos;
  }

  private void OnEnable()
  {
    controls.Ship_Move.Enable();
  }

  private void OnDisable()
  {
    controls.Ship_Move.Disable();
  }
}
