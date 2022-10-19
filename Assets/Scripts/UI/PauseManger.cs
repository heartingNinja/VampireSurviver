using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManger : MonoBehaviour
{
    public void Start()
    {
        UnPauseGame();
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
    }
}
