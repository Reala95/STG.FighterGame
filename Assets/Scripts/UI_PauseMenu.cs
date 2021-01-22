using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Assets.Scripts.ClassLib._StaticData;

public class UI_PauseMenu : MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int X, int Y);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetCursorPos(out Point pos);

    Point mousePosOnEnable;

    private void Update()
    {
        if (Input.GetMouseButtonDown(MouseClick[RightClick]))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0.0f;
        Cursor.visible = true;
        GetCursorPos(out mousePosOnEnable);
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        SetCursorPos(mousePosOnEnable.X, mousePosOnEnable.Y);
    }

    public void btnOnClick_ReturnToMenu()
    {
        Application.Quit();
    }

    public void btnOnClick_Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void btnOnClick_Resume()
    {
        Cursor.visible = false;
        gameObject.SetActive(false);
    }
}
