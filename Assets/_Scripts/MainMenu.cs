using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    [Header("Configuración de Sonidos (Efectos)")]
    public AudioSource effectsSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    [Header("Configuración de Música de Fondo")]
    public AudioSource musicSource;   

    void Start()
    {
      
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.loop = true; 
            musicSource.Play();
        }
    }

    public void PlayGame()
    {
        //Cambiar al nombre de la escena del juego 
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    public void PlayHoverSound()
    {
        if (effectsSource && hoverSound)
        {
            effectsSource.PlayOneShot(hoverSound);
        }
    }

    public void PlayClickSound()
    {
        if (effectsSource && clickSound)
        {
            effectsSource.PlayOneShot(clickSound);
        }
    }
}