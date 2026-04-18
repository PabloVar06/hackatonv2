using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuadrantManager : MonoBehaviour
{
    public static QuadrantManager Instance;

    public Quadrant[] quadrants;
    public float activeTime = 4f;

    [Header("Overlays")]
    public Image[] overlays;          // Un panel UI por cuadrante
    public float overlayAlpha = 0.5f; // Qué tan oscuro se ve inactivo

    private int currentIndex = 0;
    public Player GetPlayerActivo()
    {
        return quadrants[currentIndex].player;
    }
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        foreach (var q in quadrants)
            q.SetActive(false);

        quadrants[0].SetActive(true);
        RefreshOverlays();
        StartCoroutine(CycleCuadrants());
    }

    IEnumerator CycleCuadrants()
    {
        while (true)
        {
            yield return new WaitForSeconds(activeTime);

            quadrants[currentIndex].SetActive(false);
            currentIndex = (currentIndex + 1) % quadrants.Length;
            quadrants[currentIndex].SetActive(true);

            RefreshOverlays(); // Actualizar overlays en cada cambio
        }
    }

    void RefreshOverlays()
    {
        for (int i = 0; i < overlays.Length; i++)
        {
            Color c = overlays[i].color;
            // Activo = transparente, inactivo = oscuro
            c.a = (i == currentIndex) ? 0f : overlayAlpha;
            overlays[i].color = c;
        }
    }

    public void SetActiveTime(float newTime)
    {
        activeTime = newTime;
    }
}