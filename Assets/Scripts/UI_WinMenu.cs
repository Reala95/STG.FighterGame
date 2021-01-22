using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_WinMenu : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0.0f;
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = false;
    }

    public void btnOnClick_ReturnToMenu()
    {
        Application.Quit();
    }

    public void btnOnClick_Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void btnOnClick_NextLevel()
    {

    }
}
