using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    
    public float offsetMultiplier = 0.1f;
    
    public float smoothTime = 0.3f;

    private Vector2 startPosition;
    private Vector2 velocity = Vector2.zero;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        
        float xOffset = (mousePosition.x / Screen.width) - 0.5f;
        float yOffset = (mousePosition.y / Screen.height) - 0.5f;

        Vector2 targetOffset = new Vector2(xOffset, yOffset) * offsetMultiplier;
        Vector2 targetPosition = startPosition + targetOffset;

        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}