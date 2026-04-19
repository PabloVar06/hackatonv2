using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Configuración de Ritmo")]
    [Tooltip("Asigna 1, 2, 3 o 4 dependiendo del cuadrante de este alien")]
    public int miCuadrante = 1; 
    private bool seMovioEsteCiclo = false;

    [Header("Movimiento y Físicas")]
    [SerializeField] Collider2D footCollider; 
    [SerializeField] LayerMask sueloLayer;    
    public float stepSize = 5;
    public float vel = 5;
    
    private Rigidbody2D rb2D;
    private bool moviendo;
    private bool cayendo;
    private Vector3 targetPosition;

    // --- SUSCRIPCIÓN A EVENTOS DEL RITMO ---
    // OnEnable y OnDisable son vitales cuando usamos Action/Eventos en Unity
    // para evitar errores si el personaje se destruye o desactiva.
    private void OnEnable()
    {
        RhythmManager.OnQuadrantActive += ResetearTurno;
    }

    private void OnDisable()
    {
        RhythmManager.OnQuadrantActive -= ResetearTurno;
    }

    // Esta función se llama automáticamente cuando el RhythmManager anuncia un nuevo cuadrante activo
    private void ResetearTurno(int cuadranteActivo)
    {
        // Si el cuadrante que se acaba de activar es el mío, recupero mi capacidad de moverme
        if (cuadranteActivo == miCuadrante)
        {
            seMovioEsteCiclo = false;
        }
    }
    // ---------------------------------------

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;
    }

    void Update()
    {
        cayendo = !footCollider.IsTouchingLayers(sueloLayer);
        if (cayendo) Debug.DrawRay(transform.position, Vector2.down * 0.5f, Color.red);
    }

    // --- LÓGICA DE BOTONES ---

    public void MoverIzquierda()
    {
        if (!PuedeMoverse()) return;

        targetPosition = new Vector3(transform.position.x - stepSize, transform.position.y, 0);
        IniciarMovimiento();
    }

    public void MoverDerecha()
    {
        if (!PuedeMoverse()) return;

        targetPosition = new Vector3(transform.position.x + stepSize, transform.position.y, 0);
        IniciarMovimiento();
    }

    // Función auxiliar para mantener el código limpio
    private bool PuedeMoverse()
    {
        // 1. Reglas físicas: ¿Ya se está moviendo o está en el aire?
        if (moviendo || cayendo) return false;

        // 2. Reglas de ritmo: ¿Existe el RhythmManager? (Por seguridad)
        if (RhythmManager.Instance == null) return false;

        // 3. Reglas de ritmo: ¿Es mi turno activo?
        if (RhythmManager.Instance.currentQuadrant != miCuadrante || 
            RhythmManager.Instance.currentState != RhythmManager.RhythmState.Active)
        {
            Debug.Log($"Alien {miCuadrante}: ¡Fallaste el timing!");
            // TIP: Aquí podrías llamar a una animación de "Error" en el futuro
            return false; 
        }

        // 4. Reglas de ritmo: ¿Ya gasté mi movimiento en este ciclo?
        if (seMovioEsteCiclo)
        {
            Debug.Log($"Alien {miCuadrante}: Ya te moviste este beat, espera al siguiente ciclo.");
            return false;
        }

        // Si pasa todas las pruebas, sí puede moverse
        return true;
    }

    private void IniciarMovimiento()
    {
        moviendo = true;
        seMovioEsteCiclo = true; // Gastamos el turno de este ciclo
    }

    // ---------------------------------------

    void FixedUpdate()
    {
        if (!moviendo) return;

        Vector2 newPosition = Vector2.MoveTowards(rb2D.position, targetPosition, vel * Time.fixedDeltaTime);
        rb2D.MovePosition(newPosition);

        if (Vector2.Distance(rb2D.position, targetPosition) < 0.01f || cayendo)
        {
            if(!cayendo) rb2D.MovePosition(targetPosition);
            
            rb2D.linearVelocity = new Vector2(0, rb2D.linearVelocity.y);
            moviendo = false;
        }
    }
}