using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject resumeButton;
    private Player played;
    private float count;
    public bool gameIsPause;

    // Start is called before the first frame update
    void Start()
    {
        gameIsPause = false;
        count = 0;
        pauseMenuUI.SetActive(false);
        resumeButton.SetActive(false);
        played = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //Cursor.visible = false;
        if (Input.GetKeyDown(KeyCode.Escape) && !played.GetGameCompleted())
        {
            resumeButton.SetActive(true);
            //Cursor.visible = true;
            if (gameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (count % 2 == 0)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }

    public void Pause()
    {
        count++;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = true;
    }

    public void Resume()
    {
        if (!ZombieAI1.deadHuman)
        {
            count++;
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPause = false;

        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuu");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}