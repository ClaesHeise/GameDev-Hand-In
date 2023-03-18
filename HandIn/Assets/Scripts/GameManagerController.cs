using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Game States
// for now we are only using these two
public enum GameState { MAIN_MENU, PAUSED, GAME, }

public delegate void OnStateChangeHandler();

public class GameManagerController : MonoBehaviour
{
  public event OnStateChangeHandler OnStateChange;
  public GameState gameState { get; private set; }

  public static GameManagerController Instance { get; private set; }

  void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(this);
    }
    else
    {
      Instance = this;
      Instance.gameState = GameState.MAIN_MENU;
      DontDestroyOnLoad(Instance);
    }
  }

  public void SetGameState(GameState state)
  {
    this.gameState = state;
    OnStateChange();
  }

  public void OnApplicationQuit()
  {
    Instance = null;
  }
}
