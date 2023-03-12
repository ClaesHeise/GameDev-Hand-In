using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeBtn : MonoBehaviour
{
      private static GameManagerController GM;

    // Start is called before the first frame update
    void Start()
    {
        if (GM == null) GM = GameManagerController.Instance;
    }

    // Update is called once per frame
    public void Resume()
    {
        GM.SetGameState(GameState.GAME);
        Time.timeScale = 1;
    }
}
