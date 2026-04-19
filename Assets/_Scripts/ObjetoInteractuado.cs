using UnityEngine;
using UnityEngine.Events;

public class ObjetoInteractuado : MonoBehaviour
{
    [Header("Filtro de Player")]
    public PlayerID[] escucharA; 

    [Header("Eventos publicos")]
    public UnityEvent OnAccion;
    public UnityEvent OnNoAccion;

    private void Start()
    {
        // Pequeña alarma visual por si olvidas poner el EventManager en el nivel
        if (EventManager.Instance == null)
        {
            Debug.LogError("<color=red>ERROR FATAL:</color> No hay ningún EventManager en la escena. Pon el script en un GameObject vacío.", this);
        }
        else
        {
            // Nos suscribimos de forma segura
            EventManager.Instance.EventPlayerInteractua += OnInteractua;
            EventManager.Instance.EventPlayerDejoInteractuar += OnDejoInteractuar;
        }
    }

    private void OnDestroy()
    {
        // Siempre verificamos que exista antes de intentar desuscribirnos
        if (EventManager.Instance != null)
        {
            EventManager.Instance.EventPlayerInteractua -= OnInteractua;
            EventManager.Instance.EventPlayerDejoInteractuar -= OnDejoInteractuar;
        }
    }

    private bool DebeEscuchar(PlayerID id)
    {
        if (escucharA == null || escucharA.Length == 0) return true; 
        foreach (var p in escucharA)
            if (p == id) return true;
        return false;
    }

    private void OnInteractua(PlayerID id)
    {
        if (!DebeEscuchar(id)) return;
        OnAccion?.Invoke();
    }

    private void OnDejoInteractuar(PlayerID id)
    {
        if (!DebeEscuchar(id)) return;
        OnNoAccion?.Invoke();
    }
}