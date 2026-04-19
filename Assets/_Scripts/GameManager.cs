using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string escenaGanar;
    public Animator animatorGO;
   

    private HashSet<PlayerID> playersEnMeta = new HashSet<PlayerID>();
    public int totalPlayers = 4;
    public float tiempoAnimacionCerrar = 1f;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        EventManager.Instance.EventPlayerEstaEnMeta += OnPlayerEnMeta;
        EventManager.Instance.EventPlayerMuere += OnPlayerMuere;
    }

    private void OnDestroy()
    {
        EventManager.Instance.EventPlayerEstaEnMeta -= OnPlayerEnMeta;
        EventManager.Instance.EventPlayerMuere -= OnPlayerMuere;
    }

    private void OnPlayerEnMeta(PlayerID id)
    {
        playersEnMeta.Add(id); // HashSet ignora duplicados automáticamente
        print($"Player {id} llegó a la meta. {playersEnMeta.Count}/{totalPlayers}");

        if (playersEnMeta.Count >= totalPlayers)
            Ganar();
    }

    private void OnPlayerMuere()
    {
        print("Un player a muerto");
        GameOver();
    }

    private void Ganar()
    {
        print("¡Todos en meta!");
        SceneAdmin.Instance.CambiarEscena(escenaGanar);
    }

    // Llamado desde el botón reiniciar del panel GO
    public void Reiniciar()
    {
        StartCoroutine(ReiniciarConAnimacion());
    }

    private void GameOver()
    {
        animatorGO.SetTrigger("Abrir");
    }

    private IEnumerator ReiniciarConAnimacion()
    {
        // Cerrar panel GO con animación
        animatorGO.SetTrigger("Cerrar");

        // Esperar que termine la animación
        yield return new WaitForSeconds(tiempoAnimacionCerrar);

        // Reiniciar la escena actual
        SceneAdmin.Instance.CambiarEscena(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}
