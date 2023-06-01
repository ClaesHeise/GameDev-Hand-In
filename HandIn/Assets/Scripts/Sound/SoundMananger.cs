using UnityEngine;



public class SoundMananger : MonoBehaviour
{


  public static SoundMananger Instance { get; private set; }

  [Header("Audio Sources")]
  [SerializeField]
  private AudioSource seaWindSource;
  [SerializeField]
  private AudioSource sailingSource;

  // Start is called before the first frame update
  void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  // Update is called once per frame
  void Update()
  {
    var cols = Physics.OverlapSphere(transform.position, 1f);

    foreach (var col in cols)
    {
      var unit = col.gameObject;

    }
  }

  public void PlayWind()
  {

    seaWindSource.Play();
  }

  public void PlaySailing()
  {
    sailingSource.Play();
  }
}
