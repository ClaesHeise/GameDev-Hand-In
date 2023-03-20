using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Menu : MonoBehaviour
{
  public string menuName;
  private GameObject menu;
  public bool isMainMenu;

  void Start()
  {
    menu = gameObject;
    Debug.Log(menuName);
    MenuManagerController.Instance.AddMenu(this);
  }


  public void Enable()
  {
    menu.SetActive(true);
  }

  public void Disable()
  {
    menu.SetActive(false);
  }
}
