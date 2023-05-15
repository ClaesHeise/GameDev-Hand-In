using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class DockShip : MonoBehaviour
{
  [SerializeField]
  private Transform dockPosition;

  [SerializeField]
  private Transform playerDropOff;

  private MeshRenderer meshRenderer;

  private void Start()
  {
    meshRenderer = GetComponent<MeshRenderer>();
    if (meshRenderer != null)
      meshRenderer.enabled = false;
  }
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Ship"))
    {
      meshRenderer.enabled = true;
      var ship = other.gameObject.GetComponent<Ship>();
      ship.dockPosition = dockPosition;
      ship.playerDropOff = playerDropOff;

    }
  }

  private void OnTriggerStay(Collider other)
  {
    if (other.CompareTag("Ship") && other.gameObject.GetComponent<Ship>().dockPosition == null)
    {
      var ship = other.gameObject.GetComponent<Ship>();
      ship.dockPosition = dockPosition;
      ship.playerDropOff = playerDropOff;
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Ship"))
    {
      meshRenderer.enabled = false;
      var ship = other.gameObject.GetComponent<Ship>();
      ship.dockPosition = null;
      ship.playerDropOff = null;
    }
  }
}
