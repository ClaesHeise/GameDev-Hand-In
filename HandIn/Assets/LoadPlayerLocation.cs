using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerLocation : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if(PlayerPrefs.HasKey("playerX") && PlayerPrefs.HasKey("playerY") && PlayerPrefs.HasKey("playerZ"))
        {
            Debug.Log("Loading player location...");
            float x = PlayerPrefs.GetFloat("playerX");
            float y = PlayerPrefs.GetFloat("playerY");
            float z = PlayerPrefs.GetFloat("playerZ");

            Vector3 position = new Vector3(x, y, z);

            transform.position = position;

        }
    }

}
