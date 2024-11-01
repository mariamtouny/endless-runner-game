using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Play");
        Time.timeScale = 1;
    }

    public void Main()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
