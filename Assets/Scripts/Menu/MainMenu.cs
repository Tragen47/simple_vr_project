using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool IsVR = false;

    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

    // Enable mouse cursor
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadScene(bool isVR)
    {
        IsVR = isVR;
        SceneManager.LoadScene("MainScene");
    }

    public void Quit() => Application.Quit();
}