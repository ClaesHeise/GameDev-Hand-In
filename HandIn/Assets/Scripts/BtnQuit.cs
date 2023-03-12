using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnQuit : MonoBehaviour
{
  public void QuitGame()
  {
    GameManagerController.Instance.OnApplicationQuit();
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
  }
}