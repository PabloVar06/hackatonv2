using System;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    // Hacemos que sea un Singleton para que los Jugadores puedan acceder a él fácilmente
    public static RhythmManager Instance;

    [Header("Configuración de Audio")]
    public AudioSource musicSource;
    public float bpm = 120f; // Los Beats por Minuto de tu canción
    private float secPerBeat;
    private double dspSongTime;

    [Header("Seguimiento del Ritmo")]
    public float songPosition;
    public float songPositionInBeats;
    private int completedBeats = 0;

    // Estados públicos para que el script del Player sepa qué está pasando
    public enum RhythmState { Warning, Active }
    public RhythmState currentState;
    public int currentQuadrant = 1;

    // EVENTOS: Otros scripts se suscribirán a estos
    public static event Action<int> OnQuadrantWarning;
    public static event Action<int> OnQuadrantActive;

    void Awake()
    {
        // Configuración básica del Singleton
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        secPerBeat = 60f / bpm; // Calcula cuántos segundos dura un beat
        dspSongTime = AudioSettings.dspTime; // Guarda el tiempo exacto en que inicia la música
        musicSource.Play();
    }

    void Update()
    {
        // Calcular la posición de la canción usando el reloj de audio de alta precisión
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);
        songPositionInBeats = songPosition / secPerBeat;

        // Si hemos cruzado la línea de un nuevo beat entero
        if (songPositionInBeats >= completedBeats + 1)
        {
            completedBeats++;
            ProcessBeat();
        }
    }

    void ProcessBeat()
    {
        // Un ciclo completo son 8 beats (Warning y Active para 4 cuadrantes)
        // El módulo (%) 8 nos dará números del 0 al 7 infinitamente.
        int stepInCycle = completedBeats % 8;

        switch (stepInCycle)
        {
            case 1: // Beat 1
                currentQuadrant = 1;
                currentState = RhythmState.Warning;
                OnQuadrantWarning?.Invoke(1); // Grita al vacío: "¡Aviso Cuadrante 1!"
                break;
            case 2: // Beat 2
                currentState = RhythmState.Active;
                OnQuadrantActive?.Invoke(1);  // Grita al vacío: "¡Activo Cuadrante 1!"
                break;
            case 3: // Beat 3
                currentQuadrant = 2;
                currentState = RhythmState.Warning;
                OnQuadrantWarning?.Invoke(2);
                break;
            case 4: // Beat 4
                currentState = RhythmState.Active;
                OnQuadrantActive?.Invoke(2);
                break;
            case 5: // Beat 5
                currentQuadrant = 3;
                currentState = RhythmState.Warning;
                OnQuadrantWarning?.Invoke(3);
                break;
            case 6: // Beat 6
                currentState = RhythmState.Active;
                OnQuadrantActive?.Invoke(3);
                break;
            case 7: // Beat 7
                currentQuadrant = 4;
                currentState = RhythmState.Warning;
                OnQuadrantWarning?.Invoke(4);
                break;
            case 0: // Beat 8 (módulo 8 da 0)
                currentState = RhythmState.Active;
                OnQuadrantActive?.Invoke(4);
                break;
        }
    }
}