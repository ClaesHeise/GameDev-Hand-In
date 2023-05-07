using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

  [SerializeField]
  private AudioSource dockSound;

  [SerializeField]
  private AudioSource sailingSound;

  private ShipMovement shipMovement;



  // Start is called before the first frame update
  void Start()
  {

    shipMovement = GetComponent<ShipMovement>();
    shipMovement.OnShipDock += PlayDockSound;
    shipMovement.OnShipMove += PlaySailingSound;
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
  void Update()
  {

  }
}
