using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    public static SceneFade Instance;
    public Image fadeImage;
    public float fadeDuration = 1f;
    public bool fadeAlIniciar = true; // Si está activo hace FadeOut al iniciar

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (fadeAlIniciar)
            StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        yield return Fade(1f, 0f);
    }

    public IEnumerator FadeIn()
    {
        yield return Fade(0f, 1f);
    }

    private IEnumerator Fade(float desde, float hasta)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(desde, hasta, elapsed / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = hasta;
        fadeImage.color = color;
    }
}