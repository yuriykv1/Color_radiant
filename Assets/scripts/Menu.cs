using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopScene();
        }


    }
    public void StopScene()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;

    }
    public void NextLVL(int scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    public void ExitMainMenu(int scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1f;
    }

    public void PlayScene()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;

    }

}
