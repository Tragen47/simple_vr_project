using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyInputHandler : MonoBehaviour
{
    void Update()
    {
        // Return to the main menu when the "Escape" button is pressed
        if (Input.GetKeyUp(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
    }
}
