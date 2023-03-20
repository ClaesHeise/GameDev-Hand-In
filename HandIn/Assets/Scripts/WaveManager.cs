using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
  public static WaveManager Instance { get; private set; }

  public float amplitude = 1f;
  public float length = 2f;
  public float speed = 1f;

  public float offset = 0f;


  private void Awake()
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

  private void Update()
  {
    offset += Time.deltaTime * speed;
  }

  public float GetWaveHeight(Vector3 position)
  {
    return Mathf.Sin(position.x / length + offset) * amplitude;
  }
}
