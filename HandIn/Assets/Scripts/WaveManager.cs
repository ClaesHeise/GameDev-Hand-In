using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
  public static WaveManager Instance { get; private set; }

  public float waveHeight = 0.5f;
  public float waveFrequency = 0.5f;
  public float waveLength = 0.75f;

  //Position where the waves originate from
  public Vector3 waveOriginPosition = new Vector3(0.0f, 0.0f, 0.0f);
  public float offset = 0f;
  public float speed = 0f;


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
    //Get the distance between wave origin position and the current vertex
    float distance = Vector3.Distance(position, waveOriginPosition);
    distance = (distance % waveLength) / waveLength;
    return waveHeight * Mathf.Sin(Time.time * Mathf.PI * 2.0f * waveFrequency
            + (Mathf.PI * 2.0f * distance)) + Mathf.Sin(Time.time * Mathf.PI * 4.0f * waveFrequency
            + (Mathf.PI * 1.5f * distance));
  }
}
