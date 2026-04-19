using System;
using UnityEngine;

// I changed the class name here to exactly match your filename: EventManScript
public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    void Awake()
    {
        Instance = this;
    }

    // Declarations (No "1"s)
    public event Action<PlayerID> EventPlayerInteractua;
    public event Action<PlayerID> EventPlayerDejoInteractuar;
    public event Action EventPlayerMuere;
    public event Action<PlayerID> EventPlayerEstaEnMeta;

    public void PlayerInteractua(PlayerID id)
    {
        // Removed the "1" here so it matches the declaration above perfectly
        EventPlayerInteractua?.Invoke(id);
    }

    public void PlayerDejoInteractuar(PlayerID id)
    {
        // Removed the "1" here so it matches the declaration above perfectly
        EventPlayerDejoInteractuar?.Invoke(id);
    }

    public void PlayerMuere()
    {
        EventPlayerMuere?.Invoke();
    }

    public void PlayerEnMeta(PlayerID id)
    {
        EventPlayerEstaEnMeta?.Invoke(id);
    }
}