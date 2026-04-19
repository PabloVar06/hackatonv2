using UnityEngine;

public class PlataformaRitmica : MonoBehaviour
{
    [Header("Configuración")]
    public float stepSize = 5f;
    public float vel = 5f;
    public int limitePasos = 3; 
    
    private bool estaActiva = false;
    private bool moviendo = false;
    private int pasoActual = 0;
    private Vector3 posicionInicial;
    private Vector3 targetPosition;
    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.bodyType = RigidbodyType2D.Kinematic; 
        posicionInicial = transform.position;
        targetPosition = posicionInicial;
    }

    private void OnEnable() { RhythmManager.OnQuadrantActive += ProcesarMovimiento; }
    private void OnDisable() { RhythmManager.OnQuadrantActive -= ProcesarMovimiento; }

    public void ActivarPlataforma()
    {
        Debug.Log("<color=green>PLATAFORMA:</color> Señal de ACTIVAR recibida del botón.");
        estaActiva = true;
    }

    public void DesactivarPlataforma()
    {
        Debug.Log("<color=red>PLATAFORMA:</color> Señal de DESACTIVAR recibida.");
        estaActiva = false;
    }

    private void ProcesarMovimiento(int cuadranteActivo)
    {
        // Este log te dirá si el ritmo está llegando a la plataforma
        // Debug.Log("Ritmo detectado por plataforma. ¿Está activa?: " + estaActiva);

        if (moviendo || !estaActiva) return;

        if (pasoActual < limitePasos)
        {
            pasoActual++;
            targetPosition = posicionInicial + (Vector3.up * stepSize * pasoActual);
            moviendo = true;
            Debug.Log("Iniciando paso hacia arriba n°: " + pasoActual);
        }
    }

    private void FixedUpdate()
    {
        if (!moviendo) return;

        Vector2 newPosition = Vector2.MoveTowards(rb2D.position, targetPosition, vel * Time.fixedDeltaTime);
        rb2D.MovePosition(newPosition);

        if (Vector2.Distance(rb2D.position, targetPosition) < 0.05f)
        {
            rb2D.position = targetPosition;
            moviendo = false;
        }
    }
}