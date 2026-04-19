using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuadrantUI : MonoBehaviour
{
    [Header("Configuración")]
    public int miCuadrante = 1;
    
    [Header("Sistema de Focos (Sprites)")]
    public GameObject focoOn;  // El objeto con el sprite "encendido"
    public GameObject focoOff; // El objeto con el sprite "apagado" (siempre activo)

    [Header("Fondo de Activación")]
    public Image overlayFondo;
    public Color colorActivacion = new Color(1f, 1f, 1f, 0.4f); 
    public float tiempoFade = 0.5f;

    private void OnEnable()
    {
        // IMPORTANTE: Si el objeto está desactivado en la jerarquía al inicio,
        // esto no se ejecutará. Asegúrate de que los 4 paneles estén activos.
        RhythmManager.OnQuadrantWarning += ActivarAdvertencia;
        RhythmManager.OnQuadrantActive += IniciarActivacion;
    }

    private void OnDisable()
    {
        RhythmManager.OnQuadrantWarning -= ActivarAdvertencia;
        RhythmManager.OnQuadrantActive -= IniciarActivacion;
    }

    private void Start()
    {
        // Estado inicial
        focoOn.SetActive(false);
        focoOff.SetActive(true);
        
        Color c = overlayFondo.color;
        c.a = 0f;
        overlayFondo.color = c;
    }

    private void ActivarAdvertencia(int cuadranteActivo)
    {
        if (cuadranteActivo == miCuadrante)
        {
            Debug.Log($"Panel {miCuadrante}: Recibida Advertencia");
            focoOn.SetActive(true); // Se superpone al Off
        }
        else
        {
            focoOn.SetActive(false);
        }
    }

    private void IniciarActivacion(int cuadranteActivo)
    {
        if (cuadranteActivo == miCuadrante)
        {
            Debug.Log($"Panel {miCuadrante}: Recibida Activación");
            focoOn.SetActive(false); // Se apaga el foco On
            
            StopAllCoroutines();
            StartCoroutine(RutinaFadeout());
        }
    }

    private IEnumerator RutinaFadeout()
    {
        float t = 0;
        while (t < tiempoFade)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(colorActivacion.a, 0f, t / tiempoFade);
            overlayFondo.color = new Color(colorActivacion.r, colorActivacion.g, colorActivacion.b, alpha);
            yield return null;
        }
    }
}