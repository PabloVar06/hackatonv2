using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAdmin : MonoBehaviour
{
    public static SceneAdmin Instance;

    void Awake() { Instance = this; }

    public void CambiarEscena(string nombreEscena)
    {
        StartCoroutine(CambiarConFade(nombreEscena));
    }

    private IEnumerator CambiarConFade(string nombreEscena)
    {
        // Fade in (transparente → negro)
        yield return StartCoroutine(SceneFade.Instance.FadeIn());
        // Cambiar escena cuando termina
        SceneManager.LoadScene(nombreEscena);
    }
}