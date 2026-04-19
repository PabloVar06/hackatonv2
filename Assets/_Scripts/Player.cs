using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Configuración de Ritmo")]
    public int miCuadrante = 1; 
    private bool seMovioEsteCiclo = false;

    public PlayerID playerID;

    [Header("Movimiento y Físicas")]
    [SerializeField] Collider2D footCollider; 
    [SerializeField] LayerMask sueloLayer;    
    [SerializeField] LayerMask paredLayer;
    public float stepSize = 5;
    public float vel = 5;
    
    [Header("Sistema de Seguridad")]
    [Tooltip("Tiempo máximo en segundos que puede durar un paso antes de forzar su detención")]
    public float tiempoMaximoPaso = 1f;
    private float temporizadorPaso = 0f; // Llevará la cuenta del tiempo
    
    private Rigidbody2D rb2D;
    private bool moviendo;
    private bool cayendo;
    private Vector3 targetPosition;

    private void OnEnable()
    {
        RhythmManager.OnQuadrantActive += ResetearTurno;
    }

    private void OnDisable()
    {
        RhythmManager.OnQuadrantActive -= ResetearTurno;
    }

    private void ResetearTurno(int cuadranteActivo)
    {
        if (cuadranteActivo == miCuadrante)
        {
            seMovioEsteCiclo = false;
        }
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;
    }

    void Update()
    {
        cayendo = !footCollider.IsTouchingLayers(sueloLayer);
    }

    public void MoverIzquierda()
    {
        if (!PuedeMoverse()) return;

        // NUEVO: Si hay una pared a la izquierda, cancelamos el movimiento
        if (HayPared(Vector2.left)) return; 

        targetPosition = new Vector3(transform.position.x - stepSize, transform.position.y, 0);
        IniciarMovimiento();
    }

    public void MoverDerecha()
    {
        if (!PuedeMoverse()) return;

        // NUEVO: Si hay una pared a la derecha, cancelamos el movimiento
        if (HayPared(Vector2.right)) return; 

        targetPosition = new Vector3(transform.position.x + stepSize, transform.position.y, 0);
        IniciarMovimiento();
    }

    private bool PuedeMoverse()
    {
        if (moviendo || cayendo) return false;
        if (RhythmManager.Instance == null) return false;
        
        if (RhythmManager.Instance.currentQuadrant != miCuadrante || 
            RhythmManager.Instance.currentState != RhythmManager.RhythmState.Active)
        {
            return false; 
        }

        if (seMovioEsteCiclo) return false;

        return true;
    }

    private void IniciarMovimiento()
    {
        moviendo = true;
        seMovioEsteCiclo = true; 
        temporizadorPaso = 0f; // Reseteamos el cronómetro a 0 cada vez que empezamos un paso
    }

    // Función que verifica si hay una pared en la dirección en la que queremos ir
    private bool HayPared(Vector2 direccion)
    {
        // Physics2D.Raycast lanza un rayo. 
        // Parámetros: Origen, Dirección, Distancia, Filtro de Capas
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direccion, stepSize, paredLayer);
        
        if (hit.collider != null)
        {
            Debug.Log($"Alien {miCuadrante}: Movimiento bloqueado por {hit.collider.name}");
            return true; // Sí, hay una pared
        }
        
        return false; // No hay pared, el camino está libre
    }

    void FixedUpdate()
    {
        if (!moviendo) return;

        // 1. Aumentamos nuestro cronómetro de seguridad
        temporizadorPaso += Time.fixedDeltaTime;

        // 2. Intentamos movernos normalmente
        Vector2 newPosition = Vector2.MoveTowards(rb2D.position, targetPosition, vel * Time.fixedDeltaTime);
        rb2D.MovePosition(newPosition);

        // 3. Verificamos las 3 condiciones para detenernos:
        //    - Llegamos a la meta (con un margen más amplio de 0.05f)
        //    - Nos caímos por un precipicio
        //    - NUEVO: Se acabó el tiempo de seguridad (1 segundo)
        if (Vector2.Distance(rb2D.position, targetPosition) < 0.05f || cayendo || temporizadorPaso >= tiempoMaximoPaso)
        {
            if (temporizadorPaso >= tiempoMaximoPaso)
            {
                Debug.LogWarning($"Alien {miCuadrante}: Se activó el sistema de seguridad. Forzando detención.");
            }

            // Forzamos al alien a encajar perfectamente en su meta si no se está cayendo
            if (!cayendo) 
            {
                rb2D.position = targetPosition; 
            }
            
            rb2D.linearVelocity = new Vector2(0, rb2D.linearVelocity.y);
            moviendo = false;
        }
    }
}