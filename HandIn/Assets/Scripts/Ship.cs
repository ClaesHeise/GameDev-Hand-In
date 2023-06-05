using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ship : MonoBehaviour
{
  [SerializeField]
  private AudioSource dockSound;

  [SerializeField]
  private AudioSource sailingSound;

  private ShipMovement shipMovement;
  private Rigidbody rb;

  [SerializeField]
  private GameObject Player;

  public bool isDocked { get; private set; } = false;
  public event Action OnShipDock;

  public Transform dockPosition { get; set; }
  public Transform playerDropOff { get; set; }

  private MovementInput controls;
  private void Awake()
  {
    controls = new MovementInput();
    controls.Ship_Move.Disable();
  }

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    shipMovement = GetComponent<ShipMovement>();
    OnShipDock += PlayDockSound;
    shipMovement.OnShipMove += PlaySailingSound;
    isDocked = true;
    OnShipDock.Invoke();
  }

  private void PlayDockSound()
  {
    if (!dockSound.isPlaying)
    {
      dockSound.Play();
    }
  }

  private void PlaySailingSound()
  {
    if (!sailingSound.isPlaying)
    {
      sailingSound.Play();
    }
  }
  // Update is called once per frame
  void FixedUpdate()
  {
    if (isDocked && dockPosition != null)
    {
      rb.angularVelocity = Vector3.zero;
      rb.velocity = new Vector3(0, rb.velocity.y, 0);
      transform.rotation = Quaternion.identity * dockPosition.rotation;
    }
  }

  private void Interaction()
  {
    if (dockPosition == null)
    {
      return;
    }

    if (!isDocked)
    {
      Dock();
    }
  }

  private void Dock()
  {
    StartCoroutine(Move(transform, dockPosition, 5f));
  }

  public void OnHasDock()
  {
    OnShipDock.Invoke();
    rb.velocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;
    isDocked = true;
    shipMovement.enabled = false;
    controls.Ship_Move.Disable();
    controls.Player_Move.Enable();
    Player.transform.parent = null;
    Player.transform.position = playerDropOff.position;
    Player.transform.rotation = playerDropOff.rotation;
    Player.SetActive(true);
    Camera.main.GetComponent<SwitchCameraTarget>().SwitchTarget(Player, new Vector3(0, 4, -6), OffsetType.Local);
    Camera.main.GetComponent<OrbitCamera>().enabled = false;
    controls.Interaction.Use.performed += ctx => { };
  }

  public void Undock()
  {
    Player.transform.parent = transform;
    Camera.main.GetComponent<SwitchCameraTarget>().SwitchTarget(gameObject, new Vector3(30, 40, 0), OffsetType.World);
    Camera.main.GetComponent<OrbitCamera>().enabled = true;
    isDocked = false;
    shipMovement.enabled = true;
    controls.Player_Move.Disable();
    controls.Ship_Move.Enable();
    controls.Interaction.Enable();
    controls.Interaction.Height.Disable();
    controls.Interaction.Drop.Disable();
    controls.Interaction.Use.performed += ctx => Interaction();
    Player.SetActive(false);
  }

  IEnumerator Move(Transform beginPos, Transform endPos, float duration)
  {
    float t = 0f;
    while (t < duration)
    {
      t += Time.deltaTime;
      transform.position = Vector3.Lerp(beginPos.position, endPos.position, t / duration);
      transform.rotation = Quaternion.Lerp(beginPos.rotation, endPos.rotation, t / duration);
      yield return null;
    }
    transform.position = endPos.position;
    transform.rotation = endPos.rotation;
    OnHasDock();
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
