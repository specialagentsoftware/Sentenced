using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputMonitor : MonoBehaviour
{
    SceneManager sceneManager;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SceneManager.GetActiveScene().name == "Start")
            {
                SceneManager.LoadScene("Controls_Instructions");
            } else
            {
                SceneManager.LoadScene("GameScene");
            }

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("PauseScreen");
        }
    }
}
