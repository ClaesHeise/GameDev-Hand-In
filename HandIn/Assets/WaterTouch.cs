using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Area
{
  Tropic,
  Desert
}


public class WaterTouch : MonoBehaviour
{
  public Area area;
  private Dictionary<Area, List<int>> areas;
  private Dictionary<Area, Vector3> spawnAreas;
  // Start is called before the first frame update
  void Start()
  {
    areas = new Dictionary<Area, List<int>>();
    spawnAreas = new Dictionary<Area, Vector3>();
    areas.Add(Area.Tropic, new List<int> { 735, 1260, -1215, -690 });
    spawnAreas.Add(Area.Tropic, new Vector3(1137, 6, -990));
    areas.Add(Area.Desert, new List<int> { -410, 0, 35, 455});
    spawnAreas.Add(Area.Desert, new Vector3(-205, 10, 210));
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    var currentField = areas[area];
    if (transform.position.x < currentField[0] || transform.position.x > currentField[1] || transform.position.z < currentField[2] || transform.position.z > currentField[3])
    {
      transform.position = spawnAreas[area];
    }
  }
}

    // private void touchesTropic(){

    //     if(transform.position.x > 1260 || transform.position.x < 735 || transform.position.z < -1215 || transform.position.z > -690){
    //         print("Touched the water on tropic island");
    //         transform.position = new Vector3(1137, 6, -990);
    //     }
    // }

    // private void touchesDesert(){
    //     if(transform.position.x < -410 || transform.position.x > 0 || transform.position.z > 455 || transform.position.z < 35){
    //         print("Touched the water on desert island");
    //         transform.position = new Vector3(0, 0, 0);
    //     }
    // }
// }
