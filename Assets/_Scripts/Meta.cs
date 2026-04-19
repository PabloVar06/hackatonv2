// Meta.cs - En el trigger de la meta
using UnityEngine;

public class Meta : MonoBehaviour
{
    public PlayerID playerID; // Asignas en el Inspector qué player es esta meta

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventManager.Instance.PlayerEnMeta(playerID);
        }
    }
}