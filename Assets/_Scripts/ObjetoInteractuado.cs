using UnityEngine;
using UnityEngine.Events;

public class ObjetoInteractuado : MonoBehaviour
{
    [Header("Filtro de Player")]
    public PlayerID[] escucharA; // Seleccionas en Inspector cu�les escuchar

    [Header("Eventos publicos")]
    public UnityEvent OnAccion;
    public UnityEvent OnNoAccion;

    private void Start()
    {
        EventManager.Instance.EventPlayerInteractua += OnInteractua;
        EventManager.Instance.EventPlayerDejoInteractuar += OnDejoInteractuar;
    }

    private void OnDestroy()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.EventPlayerInteractua -= OnInteractua;
            EventManager.Instance.EventPlayerDejoInteractuar -= OnDejoInteractuar;
        }
    }

    // Verifica si debe escuchar a ese player
    private bool DebeEscuchar(PlayerID id)
    {
        if (escucharA == null || escucharA.Length == 0) return true; // Si no filtra, escucha a todos
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