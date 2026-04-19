using UnityEngine;
using UnityEngine.Events;

public class Interactions : MonoBehaviour
{
    public UnityEvent OnPlayerEntra;
    public UnityEvent OnPlayerSale;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. Nos avisa si CUALQUIER cosa toca el botón
        Debug.Log($"<color=orange>BOTÓN:</color> Algo me tocó: {collision.gameObject.name}");

        if (collision.CompareTag("Player"))
        {
            // 2. Nos avisa si reconoció el Tag
            Debug.Log("<color=orange>BOTÓN:</color> ¡Reconocí que es un Player!");

            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                // 3. Nos avisa si logró leer el ID del Alien y enviar la señal
                Debug.Log($"<color=orange>BOTÓN:</color> Enviando señal al EventManager del Player ID: {player.playerID}");
                EventManager.Instance.PlayerInteractua(player.playerID);
            }
            else
            {
                Debug.LogError("<color=orange>BOTÓN:</color> El objeto tiene el Tag 'Player' pero NO tiene el script 'Player'.");
            }

            OnPlayerEntra?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
                EventManager.Instance.PlayerDejoInteractuar(player.playerID);

            OnPlayerSale?.Invoke();
        }
    }
}