using UnityEngine;

public class Quadrant : MonoBehaviour
{
    public int index;
    public Player player;           // El personaje de este cuadrante
    public GameObject visualIndicator; // Un borde/highlight opcional

    private bool isActive = false;

    public void SetActive(bool active)
    {
        isActive = active;
        //player.SetControllable(active); // Bloquear/desbloquear input del jugador

        // Mostrar indicador visual si tienes uno
        if (visualIndicator != null)
            visualIndicator.SetActive(active);
    }

    public bool IsActive() => isActive;
}