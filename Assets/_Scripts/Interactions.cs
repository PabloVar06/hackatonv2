
using UnityEngine;
using UnityEngine.Events;

public class Interactions : MonoBehaviour
{
    public UnityEvent OnPlayerEntra;
    public UnityEvent OnPlayerSale;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Obtener el ID del player que entró
            Player player = collision.GetComponent<Player>();
            if (player != null)
                EventManager.Instance.PlayerInteractua(player.playerID);

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