using UnityEngine;
using UnityEngine.EventSystems; 

public class JuicyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Ajustes de Escala")]
    public float scaleFactor = 1.1f; 
    public float speed = 10f;      

    private Vector3 initialScale;
    private Vector3 targetScale;

    void Start()
    {
        initialScale = transform.localScale;
        targetScale = initialScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.unscaledDeltaTime * speed);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = initialScale * scaleFactor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = initialScale;
    }
}