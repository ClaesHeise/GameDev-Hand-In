using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipData
{
    public float[] position;

    public ShipData(GameObject ship)
    {
        position = new float[3];
        position[0] = ship.transform.position.x;
        position[1] = ship.transform.position.y;
        position[2] = ship.transform.position.z;
    }
}
