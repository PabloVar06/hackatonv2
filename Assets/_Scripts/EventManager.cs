using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public event Action<PlayerID> EventPlayerInteractua;
    public event Action<PlayerID> EventPlayerDejoInteractuar;
    public event Action<PlayerID> EventPlayerMuere;

    public void Player1Interactua(PlayerID id)
    {
        EventPlayer1Interactua?.Invoke(id);
    }

    public void Player1DejoInteractuar(PlayerID id)
    {
        EventPlayer1DejoInteractuar?.Invoke(id);
    }

    public void PlayerMuere(PlayerID id)
    {
        EventPlayerMuere?.Invoke(id);
    }
}