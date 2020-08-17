using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenController : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    private bool paused;

    public void Pause()
    {
        paused = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        paused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            if (Input.anyKeyDown)
            {
                Resume();
            }
        }
        else
        {
            if (Input.GetButtonDown("Pause"))
            {
                Pause();
            }
        }
    }
}
