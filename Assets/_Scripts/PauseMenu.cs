using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; 
    public static bool isPaused = false;

    void Update()
    {
        // Detecta si presionas la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Esconde el menú
        Time.timeScale = 1f;          // Reanuda el tiempo del juego
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);  // Muestra el menú
        Time.timeScale = 0f;          // Congela el tiempo del juego
        isPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;          // Reanuda el tiempo antes de cambiar
        SceneManager.LoadScene("MainMenu"); // Toca ver como se llama la escena
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo...");
        Application.Quit();
    }
}