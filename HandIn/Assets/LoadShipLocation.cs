using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadShipLocation : MonoBehaviour
{
    void Awake() 
    {
        if(PlayerPrefs.HasKey("shipX") && PlayerPrefs.HasKey("shipY") && PlayerPrefs.HasKey("shipZ"))
        {
            Debug.Log("Loading ship location...");
            float x = PlayerPrefs.GetFloat("shipX");
            float y = PlayerPrefs.GetFloat("shipY");
            float z = PlayerPrefs.GetFloat("shipZ");

            Vector3 position = new Vector3(x, y, z);

            transform.position = position;
        }
    }
}
