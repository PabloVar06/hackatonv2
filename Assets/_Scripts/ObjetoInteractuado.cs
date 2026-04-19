using UnityEngine;
using UnityEngine.Events;

public class ObjetoInteractuado : MonoBehaviour
{
    [Header("Filtro de Player")]
    public PlayerID[] escucharA; // Seleccionas en Inspector cuáles escuchar

    [Header("Eventos públicos")]
    public UnityEvent OnAccion;
    public UnityEvent OnNoAccion;

    private void Start()
    {
        EventManager.Instance.EventPlayerInteractua += OnInteractua;
        EventManager.Instance.EventPlayerDejoInteractuar += OnDejoInteractuar;
        EventManager.Instance.EventPlayerMuere += OnMuere;
    }

    private void OnDestroy()
    {
        EventManager.Instance.EventPlayerInteractua -= OnInteractua;
        EventManager.Instance.EventPlayerDejoInteractuar -= OnDejoInteractuar;
        EventManager.Instance.EventPlayerMuere -= OnMuere;
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

    private void OnMuere(PlayerID id)
    {
        if (!DebeEscuchar(id)) return;
        print($"Player {id} murió");
    }
}