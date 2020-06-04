using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject pausePanel;
    public static bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                MenuContinue();
            }
            else
            { 
                MenuPause();
            }
        }
    }
    public void MenuPause()
    {
        pausePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
        isPaused = true;
    }
    public void MenuContinue()
    {
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        isPaused = false;
    }

    public void MenuRestart()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Time.timeScale = 1;
        SceneManager.LoadScene(currentScene);
    }

    public void ExitGame()
    {
        Destroy(GameObject.FindGameObjectWithTag("music"));
        SceneManager.LoadScene("MenuPrincipal");
    }
}
