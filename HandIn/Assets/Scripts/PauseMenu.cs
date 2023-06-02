using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject pauseMenuUI;
    public GameObject savingAndExitingPopUp;
    public GameObject savingAndQuittingPopUp;

    //public Player player;
    public GameObject player;

    public GameObject ship;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Freeze the game
        Paused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Unfreeze the game
        Paused = false;
    }

    public void LoadMenu()
    {
        // Display saving and exiting pop up for a couple seconds then continue
        pauseMenuUI.SetActive(false);
        waiter();
        savingAndExitingPopUp.SetActive(true);

        Debug.Log("Saving game and Loading menu...");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        // Save the player data
        SaveSystem.SavePlayer(player);

        // Save the ship data
        SaveSystem.SaveShip(ship);

        Time.timeScale = 1f; // Unfreeze the game
        Paused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        savingAndQuittingPopUp.SetActive(true);
        waiter();
        Application.Quit();
    }

    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(4);
    }
}
