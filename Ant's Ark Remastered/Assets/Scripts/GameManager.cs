using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TimerManager timerManager;
    public UIManager UIManager;

    public bool paused;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void Win()
    {
        print("Win");
        //timerManager;

        timerManager.CheckHighScore();
        timerManager.UpdateHighScoreText();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (paused)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (!paused)
        {
            paused = true;
            UIManager.PauseUI.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        else
        {
            paused = false;
            UIManager.PauseUI.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }

    }


}
