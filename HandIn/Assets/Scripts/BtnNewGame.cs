using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnNewGame : MonoBehaviour
{
  GameManagerController GM;
  MenuManagerController MM;
  // Start is called before the first frame update
  void Start()
  {
    GM = GameManagerController.Instance;
    MM = MenuManagerController.Instance;
    GM.OnStateChange += HandleOnStateChange;
  }

  public void HandleOnStateChange()
  {
    MM.DisableAllMenus();
    Time.timeScale = 1;
  }
  public void StartNewGame()
  {
    GM.SetGameState(GameState.GAME);
  }
}
